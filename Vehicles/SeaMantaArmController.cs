using UnityEngine;

namespace CallOfTheAbyss.Vehicles;

public class SeaMantaArmController : MonoBehaviour
{
    private SeaMoth _seamoth;
    private GameObject _leftArmModel;
    private GameObject _rightArmModel;
    private float _nextDrillTime;
    private float _nextGrappleTime;

    private const float DrillCooldown = 0.5f;
    private const float GrappleCooldown = 3.5f;
    private const float ArmReachDistance = 7.0f;

    private void Start()
    {
        _seamoth = GetComponent<SeaMoth>();
        BuildVisualArmHardpoints();
    }

    private void BuildVisualArmHardpoints()
    {
        // Port (Left) Hydraulic Drill Arm hardpoint
        if (_leftArmModel == null)
        {
            _leftArmModel = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            _leftArmModel.name = "SeaManta_PortDrillArm";
            var col = _leftArmModel.GetComponent<Collider>();
            if (col != null) DestroyImmediate(col);

            _leftArmModel.transform.SetParent(transform, false);
            _leftArmModel.transform.localPosition = new Vector3(-1.15f, -0.15f, 1.25f);
            _leftArmModel.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
            _leftArmModel.transform.localScale = new Vector3(0.22f, 0.65f, 0.22f);

            var r = _leftArmModel.GetComponent<Renderer>();
            if (r != null)
            {
                r.material.color = new Color(0.2f, 0.75f, 0.85f);
            }

            // Drill bit tip
            var drillBit = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            var bitCol = drillBit.GetComponent<Collider>();
            if (bitCol != null) DestroyImmediate(bitCol);
            drillBit.transform.SetParent(_leftArmModel.transform, false);
            drillBit.transform.localPosition = new Vector3(0f, 1.0f, 0f);
            drillBit.transform.localScale = new Vector3(1.2f, 0.5f, 1.2f);
            var bitR = drillBit.GetComponent<Renderer>();
            if (bitR != null) bitR.material.color = new Color(0.95f, 0.65f, 0.15f);
        }

        // Starboard (Right) Kinetic Grappling Arm hardpoint
        if (_rightArmModel == null)
        {
            _rightArmModel = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            _rightArmModel.name = "SeaManta_StarboardGrappleArm";
            var col = _rightArmModel.GetComponent<Collider>();
            if (col != null) DestroyImmediate(col);

            _rightArmModel.transform.SetParent(transform, false);
            _rightArmModel.transform.localPosition = new Vector3(1.15f, -0.15f, 1.25f);
            _rightArmModel.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
            _rightArmModel.transform.localScale = new Vector3(0.22f, 0.65f, 0.22f);

            var r = _rightArmModel.GetComponent<Renderer>();
            if (r != null)
            {
                r.material.color = new Color(0.2f, 0.75f, 0.85f);
            }

            // Grappling claw tip
            var claw = GameObject.CreatePrimitive(PrimitiveType.Cube);
            var clawCol = claw.GetComponent<Collider>();
            if (clawCol != null) DestroyImmediate(clawCol);
            claw.transform.SetParent(_rightArmModel.transform, false);
            claw.transform.localPosition = new Vector3(0f, 1.0f, 0f);
            claw.transform.localScale = new Vector3(0.8f, 0.6f, 1.4f);
            var clawR = claw.GetComponent<Renderer>();
            if (clawR != null) clawR.material.color = new Color(0.2f, 0.5f, 1.0f);
        }
    }

    private void Update()
    {
        if (_seamoth == null || Player.main == null) return;

        bool isPiloting = _seamoth.GetPilotingMode();
        if (!isPiloting) return;

        // Display Arm Controls on HUD Reticle
        string grappleStatus = (Time.time >= _nextGrappleTime) ? "Ready" : $"{(_nextGrappleTime - Time.time):F1}s";
        HandReticle.main.SetText(
            HandReticle.TextType.Hand,
            $"SeaManta Heavy Submersible\n[L-Click] Port Drill Arm | [R-Click] Grapple Boost ({grappleStatus})",
            false
        );

        // 1. Port Drill Arm (Left Click)
        if (GameInput.GetButtonHeld(GameInput.Button.LeftHand) || Input.GetMouseButton(0))
        {
            if (Time.time >= _nextDrillTime)
            {
                _nextDrillTime = Time.time + DrillCooldown;
                PerformDrillAction();
            }
        }

        // 2. Starboard Grappling / Kinetic Arm (Right Click)
        if (GameInput.GetButtonDown(GameInput.Button.RightHand) || Input.GetMouseButtonDown(1))
        {
            if (Time.time >= _nextGrappleTime)
            {
                _nextGrappleTime = Time.time + GrappleCooldown;
                PerformGrappleAction();
            }
        }
    }

    private void PerformDrillAction()
    {
        if (MainCamera.camera == null) return;

        Ray ray = new Ray(MainCamera.camera.transform.position, MainCamera.camera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, ArmReachDistance, -1, QueryTriggerInteraction.Ignore))
        {
            // A. Large Resource Deposit
            var drillable = hit.collider.GetComponentInParent<Drillable>();
            if (drillable != null)
            {
                drillable.OnDrill(hit.point, null, out GameObject drop);
                ErrorMessage.AddMessage("[SeaManta Drill] Excavating resource deposit...");
                return;
            }

            // B. Breakable Resource Outcrop (Limestone, Sandstone, Shale, etc.)
            var breakable = hit.collider.GetComponentInParent<BreakableResource>();
            if (breakable != null)
            {
                breakable.BreakIntoResources();
                ErrorMessage.AddMessage("[SeaManta Drill] Mineral outcrop fractured!");
                return;
            }

            // C. Hostile Creature / Predator Hydraulic Strike
            var liveMixin = hit.collider.GetComponentInParent<LiveMixin>();
            if (liveMixin != null && liveMixin.gameObject != gameObject)
            {
                liveMixin.TakeDamage(50f, hit.point, DamageType.Normal, gameObject);

                var creatureRb = hit.collider.GetComponentInParent<Rigidbody>();
                if (creatureRb != null)
                {
                    Vector3 pushDir = (hit.point - transform.position).normalized;
                    creatureRb.AddForce(pushDir * 25f, ForceMode.Impulse);
                }

                ErrorMessage.AddMessage("[SeaManta Arm] Hydraulic strike delivered (50 Damage)!");
                return;
            }
        }
    }

    private void PerformGrappleAction()
    {
        if (MainCamera.camera == null || _seamoth == null || _seamoth.useRigidbody == null) return;

        // Discharges a powerful kinetic tether impulse pulling the heavy submersible forward
        Vector3 forwardBoost = MainCamera.camera.transform.forward * 30f;
        _seamoth.useRigidbody.AddForce(forwardBoost, ForceMode.VelocityChange);

        ErrorMessage.AddMessage("[SeaManta Grapple] Kinetic Tether Burst Discharged!");
    }
}
