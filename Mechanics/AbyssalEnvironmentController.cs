using CallOfTheAbyss.Biomes;
using CallOfTheAbyss.Items;
using UnityEngine;

namespace CallOfTheAbyss.Mechanics;

public class AbyssalEnvironmentController : MonoBehaviour
{
    public static AbyssalEnvironmentController Instance { get; private set; }

    public static bool IsPlayerFreezing { get; private set; }
    public static float CurrentBodyTemperature { get; private set; } = 37.0f;

    private float _nextWarningTime;
    private float _nextDamageTime;
    private Player _player;

    private void Awake()
    {
        Instance = this;
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        if (_player == null || CallOfTheAbyssPlugin.ModSettings == null)
            return;

        // Player is safe when inside a submarine or base structure
        bool isSheltered = _player.IsInSub() || _player.IsInBase();
        float currentDepth = _player.GetDepth();

        // Check if player has the Abyssal Thermal Dive Suit or Insulated Wetsuit equipped
        bool hasThermalSuit = false;
        bool hasInsulatedWetsuit = false;
        if (Inventory.main != null && Inventory.main.equipment != null)
        {
            var bodyTech = Inventory.main.equipment.GetTechTypeInSlot("Body");
            if (bodyTech == ThermalDiveSuit.Info.TechType)
            {
                hasThermalSuit = true;
            }
            else if (bodyTech == InsulatedWetsuit.Info.TechType)
            {
                hasInsulatedWetsuit = true;
            }
        }

        // ==========================================
        // 1. HYPOTHERMIA MECHANIC
        // ==========================================
        if (CallOfTheAbyssPlugin.ModSettings.EnableHypothermia)
        {
            // Deep water or within the Abyssal Trench
            bool inFrigidWater = !isSheltered && (currentDepth > 450f || Player.main.GetBiomeString() == AbyssalTrenchBiome.BiomeName);

            if (inFrigidWater && !hasThermalSuit)
            {
                if (hasInsulatedWetsuit)
                {
                    // Mid-game insulated wetsuit cushions cold loss significantly
                    CurrentBodyTemperature = Mathf.Max(20.0f, CurrentBodyTemperature - Time.deltaTime * 0.25f);
                }
                else
                {
                    // Uninsulated temperature drops rapidly in icy ocean depths
                    CurrentBodyTemperature = Mathf.Max(12.0f, CurrentBodyTemperature - Time.deltaTime * 0.75f);
                }
            }
            else
            {
                // Body temperature warms back to normal
                CurrentBodyTemperature = Mathf.Min(37.0f, CurrentBodyTemperature + Time.deltaTime * 2.5f);
            }

            float freezingThreshold = hasInsulatedWetsuit ? 22.0f : 28.0f;
            IsPlayerFreezing = CurrentBodyTemperature < freezingThreshold;

            // Freezing effects: periodic camera jitter and hypothermic shock damage
            if (IsPlayerFreezing && !isSheltered)
            {
                // Shiver camera jitter
                if (MainCamera.camera != null)
                {
                    MainCamera.camera.transform.localPosition += Random.insideUnitSphere * 0.008f;
                }

                if (Time.time > _nextWarningTime)
                {
                    ErrorMessage.AddMessage($"WARNING: Hypothermia Imminent! Body Temp: {CurrentBodyTemperature:F1}°C");
                    _nextWarningTime = Time.time + 12f;
                }

                // If severely hypothermic (< 20C), take continuous cold damage
                if (CurrentBodyTemperature < 20.0f && Time.time > _nextDamageTime)
                {
                    _nextDamageTime = Time.time + 3.0f;
                    if (_player.liveMixin != null)
                    {
                        _player.liveMixin.TakeDamage(8f, _player.transform.position, DamageType.Cold, gameObject);
                    }
                }
            }
        }
        else
        {
            IsPlayerFreezing = false;
            CurrentBodyTemperature = 37.0f;
        }

        // ==========================================
        // 2. EXTREME BAROMETRIC PRESSURE MECHANIC
        // ==========================================
        if (CallOfTheAbyssPlugin.ModSettings.EnablePressureHazard)
        {
            // Depth exceeds 1200m without thermal suit or vehicle protection
            if (!isSheltered && currentDepth > 1200f && !hasThermalSuit)
            {
                if (Time.time > _nextDamageTime)
                {
                    _nextDamageTime = Time.time + 2.0f;
                    ErrorMessage.AddMessage("WARNING: Extreme Barometric Pressure! Crush depth exceeded!");
                    if (_player.liveMixin != null)
                    {
                        _player.liveMixin.TakeDamage(12f, _player.transform.position, DamageType.Pressure, gameObject);
                    }
                }
            }
        }

        // ==========================================
        // 3. CARTOGRAPHIC HUD CHIP TELEMETRY
        // ==========================================
        bool hasCartographyChip = false;
        if (Inventory.main != null && Inventory.main.equipment != null)
        {
            var chip1 = Inventory.main.equipment.GetTechTypeInSlot("Chip1");
            var chip2 = Inventory.main.equipment.GetTechTypeInSlot("Chip2");
            if (chip1 == CartographicHUDChip.Info.TechType || chip2 == CartographicHUDChip.Info.TechType)
            {
                hasCartographyChip = true;
            }
        }

        if (hasCartographyChip)
        {
            _nextHudTimer += Time.deltaTime;
            if (_nextHudTimer >= 0.25f)
            {
                _nextHudTimer = 0f;
                Vector3 pos = _player.transform.position;
                string biome = _player.GetBiomeString();
                bool hit = Physics.Raycast(pos, Vector3.down, out RaycastHit downHit, 200f);
                string seabed = hit ? $"{downHit.distance:F0}m" : ">200m";
                _cachedNavHud = $"🧭 [NAV] {biome} | ({pos.x:F0}, {pos.y:F0}, {pos.z:F0}) | Depth: {currentDepth:F0}m | Seabed: {seabed}";
            }

            if (!string.IsNullOrEmpty(_cachedNavHud))
            {
                HandReticle.main.SetText(HandReticle.TextType.Use, _cachedNavHud, false);
            }
        }
    }

    private float _nextHudTimer;
    private string _cachedNavHud = "";
}
