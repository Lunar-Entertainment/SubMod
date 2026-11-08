using Nautilus.Json;
using Nautilus.Options.Attributes;

namespace CallOfTheAbyss.Configuration;

[Menu("Call of the Abyss", LoadOn = MenuAttribute.LoadEvents.MenuRegistered)]
public class ModConfig : ConfigFile
{
    [Toggle("Enable Deep Trench Hypothermia", Tooltip = "Freezing temperatures at extreme depths cause shivering and double oxygen consumption unless equipped with the Thermal Dive Suit.")]
    public bool EnableHypothermia = true;

    [Toggle("Enable Barometric Pressure Hazard", Tooltip = "Extreme water pressure below 1200m causes continuous crush damage to the player if exposed outside a vehicle without high-tier dive suits.")]
    public bool EnablePressureHazard = true;

    [Slider("Phantom Predator Aggression", Min = 0.5f, Max = 3.0f, DefaultValue = 1.0f, Step = 0.1f, Format = "{0:F1}x", Tooltip = "Scales the attack damage and detection radius of Abyssal Phantoms.")]
    public float AggressionMultiplier = 1.0f;

    [Slider("Quantum Propulsion Overdrive Boost", Min = 1.5f, Max = 5.0f, DefaultValue = 2.5f, Step = 0.5f, Format = "{0:F1}x", Tooltip = "Forward velocity impulse multiplier when activating the Seamoth Quantum Overdrive module.")]
    public float OverdriveSpeedMultiplier = 2.5f;

    [Toggle("Atmospheric Abyssal Darkness", Tooltip = "Enable dense light-scattering and deep midnight fog effects inside the Abyssal Trench biome.")]
    public bool DenseAtmosphericDarkness = true;
}
