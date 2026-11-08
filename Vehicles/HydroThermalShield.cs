using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using CallOfTheAbyss.Items;
using UnityEngine;

namespace CallOfTheAbyss.Vehicles;

public static class HydroThermalShield
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType(
        "HydroThermalShield",
        "Hydro-Thermal Pulse Shield",
        "Discharges a thermal-kinetic shockwave around the vehicle that repels aggressive deep trench predators and disrupts attacking fauna."
    ).WithIcon(SpriteManager.Get(TechType.SeamothElectricalDefense));

    public static void Register()
    {
        CustomPrefab prefab = new CustomPrefab(Info);

        CloneTemplate clone = new CloneTemplate(Info, TechType.SeamothElectricalDefense)
        {
            ModifyPrefab = obj =>
            {
                var renderers = obj.GetComponentsInChildren<Renderer>();
                foreach (var renderer in renderers)
                {
                    foreach (var mat in renderer.materials)
                    {
                        mat.color = new Color(0.9f, 0.4f, 0.1f);
                    }
                }
            }
        };

        prefab.SetGameObject(clone);

        prefab.SetRecipe(new RecipeData(
            new Ingredient(TechType.SeamothElectricalDefense, 1),
            new Ingredient(QuantumAlloy.Info.TechType, 1),
            new Ingredient(AbyssalCrystal.Info.TechType, 2),
            new Ingredient(TechType.ComputerChip, 1)
        ))
        .WithFabricatorType(CraftTree.Type.Workbench)
        .WithCraftingTime(4.0f);

        prefab.SetVehicleUpgradeModule(EquipmentType.SeamothModule, QuickSlotType.SelectableChargeable)
            .WithEnergyCost(15f)
            .WithCooldown(5.0f)
            .WithMaxCharge(10.0f)
            .WithOnModuleUsed((Vehicle vehicle, int slotId, float charge, float chargeScalar) =>
            {
                if (vehicle == null) return;

                // Repel surrounding creatures within 30 meters
                Collider[] hits = Physics.OverlapSphere(vehicle.transform.position, 30f);
                foreach (var hit in hits)
                {
                    var creature = hit.GetComponentInParent<Creature>();
                    if (creature != null)
                    {
                        var liveMixin = creature.GetComponent<LiveMixin>();
                        if (liveMixin != null)
                        {
                            liveMixin.TakeDamage(25f * Mathf.Max(0.5f, chargeScalar), vehicle.transform.position, DamageType.Electrical, vehicle.gameObject);
                        }

                        var creatureRb = creature.GetComponent<Rigidbody>();
                        if (creatureRb != null)
                        {
                            Vector3 blastDir = (creature.transform.position - vehicle.transform.position).normalized;
                            creatureRb.AddForce(blastDir * 25f, ForceMode.Impulse);
                        }
                    }
                }

                ErrorMessage.AddMessage("Hydro-Thermal Pulse Discharged! Nearby fauna repelled.");
            });

        prefab.SetPdaGroupCategory(TechGroup.VehicleUpgrades, TechCategory.VehicleUpgrades);

        prefab.Register();
    }
}
