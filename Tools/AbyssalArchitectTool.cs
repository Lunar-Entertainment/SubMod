using System;
using System.Collections.Generic;
using CallOfTheAbyss.Buildables.InWall;
using UnityEngine;

namespace CallOfTheAbyss.Tools;

public class AbyssalArchitectTool : PlayerTool
{
    public override string animToolName => "builder";

    public enum ToolMode
    {
        InWallBuild,
        WallDeconstruct
    }

    public class CustomPartOption
    {
        public string Name;
        public TechType TechType;
        public List<TechType> Cost;
        public Func<Vector3, Quaternion, Transform, GameObject> Factory;
        public Color ThemeColor;
    }

    private static readonly List<CustomPartOption> Parts = new List<CustomPartOption>
    {
        new CustomPartOption
        {
            Name = "In-Wall Storage Niche",
            TechType = InWallStorageNiche.Info.TechType,
            Cost = new List<TechType> { TechType.Titanium, TechType.Titanium, TechType.Quartz },
            Factory = InWallStorageNiche.CreateInWallPart,
            ThemeColor = new Color(0.1f, 0.6f, 0.9f)
        },
        new CustomPartOption
        {
            Name = "In-Wall Thermal Core",
            TechType = InWallThermalCore.Info.TechType,
            Cost = new List<TechType> { TechType.Titanium, TechType.Magnetite, TechType.CopperWire },
            Factory = InWallThermalCore.CreateInWallPart,
            ThemeColor = new Color(1.0f, 0.45f, 0.1f)
        },
        new CustomPartOption
        {
            Name = "In-Wall Bio-Luminescent Light",
            TechType = InWallBioLight.Info.TechType,
            Cost = new List<TechType> { TechType.Glass, TechType.Titanium, TechType.CreepvinePiece },
            Factory = InWallBioLight.CreateInWallPart,
            ThemeColor = new Color(0.1f, 0.9f, 0.7f)
        },
        new CustomPartOption
        {
            Name = "In-Wall Micro-Capacitor Conduit",
            TechType = InWallPowerConduit.Info.TechType,
            Cost = new List<TechType> { TechType.Titanium, TechType.Gold, TechType.CopperWire },
            Factory = InWallPowerConduit.CreateInWallPart,
            ThemeColor = new Color(0.2f, 0.5f, 1.0f)
        },
        new CustomPartOption
        {
            Name = "In-Wall Reinforced Bulkhead",
            TechType = InWallReinforcedBulkhead.Info.TechType,
            Cost = new List<TechType> { TechType.Titanium, TechType.Titanium, TechType.Lithium, TechType.Lithium },
            Factory = InWallReinforcedBulkhead.CreateInWallPart,
            ThemeColor = new Color(0.4f, 0.5f, 0.6f)
        }
    };

    private ToolMode _currentMode = ToolMode.InWallBuild;
    private int _selectedPartIndex = 0;
    private GameObject _ghostObject;
    private Material _ghostMaterial;

    public override void Awake()
    {
        base.Awake();
        InitGhost();
    }

    private void InitGhost()
    {
        if (_ghostObject != null) return;

        _ghostObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        _ghostObject.name = "ArchitectGhostPreview";
        var col = _ghostObject.GetComponent<Collider>();
        if (col != null) DestroyImmediate(col);

        var r = _ghostObject.GetComponent<Renderer>();
        if (r != null)
        {
            Shader sh = Shader.Find("Particles/Standard Unlit") ?? Shader.Find("Legacy Shaders/Transparent/Diffuse");
            if (sh != null)
            {
                _ghostMaterial = new Material(sh);
                _ghostMaterial.color = new Color(0.2f, 0.8f, 1.0f, 0.45f);
                r.material = _ghostMaterial;
            }
        }
        _ghostObject.transform.localScale = new Vector3(0.7f, 1.1f, 0.12f);
        _ghostObject.SetActive(false);
    }

    public override void OnDraw(Player p)
    {
        base.OnDraw(p);
        if (_ghostObject == null) InitGhost();
    }

    public override void OnHolster()
    {
        base.OnHolster();
        if (_ghostObject != null) _ghostObject.SetActive(false);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        if (_ghostObject != null) Destroy(_ghostObject);
    }

    public override bool OnReloadDown()
    {
        ToggleMode();
        return true;
    }

    public override bool OnAltDown()
    {
        ToggleMode();
        return true;
    }

    private void ToggleMode()
    {
        _currentMode = (_currentMode == ToolMode.InWallBuild) ? ToolMode.WallDeconstruct : ToolMode.InWallBuild;
        string modeName = (_currentMode == ToolMode.InWallBuild) ? "IN-WALL BUILD MODE" : "WALL DEMOLITION / DECONSTRUCT MODE";
        ErrorMessage.AddMessage($"[Architect Tool] Mode: {modeName}");
    }

    public override bool OnRightHandDown()
    {
        if (_currentMode == ToolMode.InWallBuild)
        {
            _selectedPartIndex = (_selectedPartIndex + 1) % Parts.Count;
            ErrorMessage.AddMessage($"[Architect] Selected: {Parts[_selectedPartIndex].Name}");
            return true;
        }
        else
        {
            ToggleMode();
            return true;
        }
    }

    public override bool OnLeftHandDown()
    {
        if (MainCamera.camera == null) return false;

        Ray ray = new Ray(MainCamera.camera.transform.position, MainCamera.camera.transform.forward);
        if (!Physics.Raycast(ray, out RaycastHit hit, 5.0f, -1, QueryTriggerInteraction.Ignore))
            return false;

        if (_currentMode == ToolMode.InWallBuild)
        {
            var part = Parts[_selectedPartIndex];

            // Verify materials
            if (!HasRequiredMaterials(part.Cost))
            {
                ErrorMessage.AddMessage($"Cannot install {part.Name}: Missing materials!");
                return false;
            }

            // Deduct materials
            DeductMaterials(part.Cost);

            // Install in-wall part flush into wall
            Quaternion wallRot = Quaternion.LookRotation(hit.normal);
            GameObject installed = part.Factory(hit.point, wallRot, hit.collider.transform);

            // Align parent to base if inside base
            var subRoot = hit.collider.GetComponentInParent<SubRoot>();
            if (subRoot != null)
            {
                installed.transform.SetParent(subRoot.transform, true);
            }

            ErrorMessage.AddMessage($"[Architect] Installed {part.Name} flush into wall!");
            return true;
        }
        else
        {
            // Wall Deconstruct / Demolition Mode
            // 1. Check for custom InWallModule
            var inWall = hit.collider.GetComponentInParent<InWallModule>();
            if (inWall != null)
            {
                inWall.Demolish();
                return true;
            }

            // 2. Check for BaseDeconstructable (Subnautica base wall piece)
            var baseDeconstructable = hit.collider.GetComponentInParent<BaseDeconstructable>();
            if (baseDeconstructable != null)
            {
                baseDeconstructable.Deconstruct();
                ErrorMessage.AddMessage("Base wall section demolished.");
                return true;
            }

            // 3. Check for standard Constructable wall fixture
            var constructable = hit.collider.GetComponentInParent<Constructable>();
            if (constructable != null)
            {
                var tech = constructable.techType;
                Destroy(constructable.gameObject);
                CraftData.AddToInventory(tech, 1, false, false);
                ErrorMessage.AddMessage($"Wall fixture ({Language.main.Get(tech)}) deconstructed.");
                return true;
            }

            ErrorMessage.AddMessage("[Architect] Target is not a removable wall part or in-wall module.");
            return false;
        }
    }

    private void Update()
    {
        if (!isDrawn || MainCamera.camera == null)
        {
            if (_ghostObject != null && _ghostObject.activeSelf) _ghostObject.SetActive(false);
            return;
        }

        Ray ray = new Ray(MainCamera.camera.transform.position, MainCamera.camera.transform.forward);
        bool hasHit = Physics.Raycast(ray, out RaycastHit hit, 5.0f, -1, QueryTriggerInteraction.Ignore);

        if (_currentMode == ToolMode.InWallBuild)
        {
            var part = Parts[_selectedPartIndex];
            bool hasMaterials = HasRequiredMaterials(part.Cost);

            if (hasHit)
            {
                if (_ghostObject != null)
                {
                    _ghostObject.SetActive(true);
                    _ghostObject.transform.position = hit.point + hit.normal * 0.02f;
                    _ghostObject.transform.rotation = Quaternion.LookRotation(hit.normal);

                    if (_ghostMaterial != null)
                    {
                        _ghostMaterial.color = hasMaterials 
                            ? new Color(part.ThemeColor.r, part.ThemeColor.g, part.ThemeColor.b, 0.5f)
                            : new Color(1.0f, 0.2f, 0.2f, 0.5f);
                    }
                }

                string costString = FormatCost(part.Cost);
                HandReticle.main.SetText(HandReticle.TextType.Hand, $"[L-Click] Install {part.Name} ({costString})\n[R-Click] Cycle Part | [R / Alt] Demolish Mode", false);
            }
            else
            {
                if (_ghostObject != null) _ghostObject.SetActive(false);
                HandReticle.main.SetText(HandReticle.TextType.Hand, $"[Architect Build] Aim at wall | Part: {part.Name} | [R-Click] Cycle | [R] Demolish Mode", false);
            }
        }
        else
        {
            // Wall Deconstruction / Removal Mode
            if (_ghostObject != null && _ghostObject.activeSelf) _ghostObject.SetActive(false);

            if (hasHit)
            {
                var inWall = hit.collider.GetComponentInParent<InWallModule>();
                if (inWall != null)
                {
                    HandReticle.main.SetText(HandReticle.TextType.Hand, $"[L-Click] Demolish {inWall.PartName} (Full Materials Refunded)\n[R / Alt] Switch to Build Mode", false);
                    return;
                }

                var baseDeconstructable = hit.collider.GetComponentInParent<BaseDeconstructable>();
                if (baseDeconstructable != null)
                {
                    HandReticle.main.SetText(HandReticle.TextType.Hand, $"[L-Click] Demolish Base Wall Section\n[R / Alt] Switch to Build Mode", false);
                    return;
                }

                var constructable = hit.collider.GetComponentInParent<Constructable>();
                if (constructable != null)
                {
                    HandReticle.main.SetText(HandReticle.TextType.Hand, $"[L-Click] Deconstruct Wall Fixture\n[R / Alt] Switch to Build Mode", false);
                    return;
                }

                HandReticle.main.SetText(HandReticle.TextType.Hand, "[Architect Demolition] Aim at an in-wall module or wall section | [R] Build Mode", false);
            }
            else
            {
                HandReticle.main.SetText(HandReticle.TextType.Hand, "[Architect Demolition Mode] Aim at wall to remove | [R / Alt] Build Mode", false);
            }
        }
    }

    private static bool HasRequiredMaterials(List<TechType> cost)
    {
        if (Inventory.main == null || Inventory.main.container == null) return false;

        // Count required items
        Dictionary<TechType, int> counts = new Dictionary<TechType, int>();
        foreach (var item in cost)
        {
            if (counts.ContainsKey(item)) counts[item]++;
            else counts[item] = 1;
        }

        foreach (var kvp in counts)
        {
            if (Inventory.main.container.GetCount(kvp.Key) < kvp.Value)
                return false;
        }

        return true;
    }

    private static void DeductMaterials(List<TechType> cost)
    {
        if (Inventory.main == null || Inventory.main.container == null) return;

        foreach (var item in cost)
        {
            Inventory.main.container.DestroyItem(item);
        }
    }

    private static string FormatCost(List<TechType> cost)
    {
        Dictionary<TechType, int> counts = new Dictionary<TechType, int>();
        foreach (var item in cost)
        {
            if (counts.ContainsKey(item)) counts[item]++;
            else counts[item] = 1;
        }

        List<string> parts = new List<string>();
        foreach (var kvp in counts)
        {
            parts.Add($"{kvp.Value}x {Language.main.Get(kvp.Key)}");
        }
        return string.Join(", ", parts);
    }
}
