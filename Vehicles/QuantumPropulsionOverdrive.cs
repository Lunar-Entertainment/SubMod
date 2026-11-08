using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using CallOfTheAbyss.Items;
using UnityEngine;

namespace CallOfTheAbyss.Vehicles;

public static class QuantumPropulsionOverdrive
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType(
        "QuantumPropulsionOverdrive",
        "Quantum Overdrive Module",
        "Supercharges vehicle propulsion thrusters with condensed abyssal plasma, delivering an immense surge of forward velocity on activation."
    ).WithIcon(SpriteManager.Get(TechType.SeamothSolarCharge));

    public static void Register()
    {
        CustomPrefab prefab = new CustomPrefab(Info);

        CloneTemplate clone = new CloneTemplate(Info, TechType.SeamothSolarCharge)
        {
            ModifyPrefab = obj =>
            {
                var renderers = obj.GetComponentsInChildren<Renderer>();
                foreach (var renderer in renderers)
                {
                    foreach (var mat in renderer.materials)
                    {
                        mat.color = new Color(0.1f, 0.8f, 0.95f);
                    }
                }
            }
        };

        prefab.SetGameObject(clone);

        prefab.SetRecipe(new RecipeData(
            new Ingredient(QuantumAlloy.Info.TechType, 2),
            new Ingredient(AbyssalCrystal.Info.TechType, 2),
            new Ingredient(TechType.WiringKit, 1),
            new Ingredient(TechType.PowerCell, 1)
        ))
        .WithFabricatorType(CraftTree.Type.Workbench)
        .WithCraftingTime(4.0f);

        prefab.SetVehicleUpgradeModule(EquipmentType.SeamothModule, QuickSlotType.SelectableChargeable)
            .WithEnergyCost(12f)
            .WithCooldown(4.0f)
            .WithMaxCharge(5.0f)
            .WithOnModuleUsed((Vehicle vehicle, int slotId, float charge, float chargeScalar) =>
            {
                if (vehicle == null) return;

                var rb = vehicle.useRigidbody;
                if (rb != null)
                {
                    float multiplier = CallOfTheAbyssPlugin.ModSettings != null 
                        ? CallOfTheAbyssPlugin.ModSettings.OverdriveSpeedMultiplier 
                        : 2.5f;

                    Vector3 forwardForce = vehicle.transform.forward * (35f * multiplier * Mathf.Max(0.5f, chargeScalar));
                    rb.AddForce(forwardForce, ForceMode.VelocityChange);
                    
                    ErrorMessage.AddMessage("Quantum Overdrive: Thrusters at 250%!");
                }
            });

        prefab.SetPdaGroupCategory(TechGroup.VehicleUpgrades, TechCategory.VehicleUpgrades);

        prefab.Register();
    }
}
