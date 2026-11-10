using System.Collections.Generic;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using CallOfTheAbyss.Items;
using UnityEngine;

namespace CallOfTheAbyss.Vehicles;

public static class HadalCrushMK5Upgrade
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType(
        "HadalCrushMK5Upgrade",
        "Void Depth Module MK5",
        "Hyper-compressed singularity matrix reinforced with hadal crystal lattice. Extends maximum vehicle operational crush depth to an extraordinary 8500 meters, unlocking exploration of the Hadal Chasm beyond the Outer Edge."
    ).WithIcon(SpriteManager.Get(TechType.VehicleHullModule3));

    public static void Register()
    {
        CustomPrefab prefab = new CustomPrefab(Info);

        CloneTemplate clone = new CloneTemplate(Info, TechType.VehicleHullModule3)
        {
            ModifyPrefab = obj =>
            {
                var renderers = obj.GetComponentsInChildren<Renderer>();
                foreach (var renderer in renderers)
                {
                    foreach (var mat in renderer.materials)
                    {
                        mat.color = new Color(0.35f, 0.05f, 0.6f);
                    }
                }
            }
        };

        prefab.SetGameObject(clone);

        prefab.SetRecipe(new RecipeData(
            new Ingredient(VoidCrushMK4Upgrade.Info.TechType, 1),
            new Ingredient(QuantumAlloy.Info.TechType, 2),
            new Ingredient(AbyssalCrystal.Info.TechType, 3),
            new Ingredient(TechType.Kyanite, 3),
            new Ingredient(TechType.Polyaniline, 2)
        ))
        .WithFabricatorType(CraftTree.Type.Workbench)
        .WithCraftingTime(7.0f);

        prefab.SetVehicleUpgradeModule(EquipmentType.SeamothModule, QuickSlotType.Passive)
            .WithDepthUpgrade(8500f, true)
            .WithOnModuleAdded((Vehicle vehicle, int slotId) =>
            {
                ErrorMessage.AddMessage("Hadal Pressure Nullifier Online. Maximum crush depth extended to 8,500m.");
            })
            .WithOnModuleRemoved((Vehicle vehicle, int slotId) =>
            {
                ErrorMessage.AddMessage("Warning: Hadal Pressure Nullifier Disconnected!");
            });

        prefab.SetPdaGroupCategory(TechGroup.VehicleUpgrades, TechCategory.VehicleUpgrades);

        prefab.Register();
    }
}
