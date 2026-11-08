using System.Collections.Generic;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using CallOfTheAbyss.Items;
using UnityEngine;

namespace CallOfTheAbyss.Vehicles;

public static class VoidCrushMK4Upgrade
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType(
        "VoidCrushMK4Upgrade",
        "Void Depth Module MK4",
        "Advanced pressure-nullification matrix utilizing Quantum Alloy plating. Extends maximum vehicle operational crush depth to 3500 meters."
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
                        mat.color = new Color(0.1f, 0.35f, 0.6f);
                    }
                }
            }
        };

        prefab.SetGameObject(clone);

        prefab.SetRecipe(new RecipeData(
            new Ingredient(TechType.VehicleHullModule3, 1),
            new Ingredient(QuantumAlloy.Info.TechType, 2),
            new Ingredient(AbyssalCrystal.Info.TechType, 2),
            new Ingredient(TechType.Kyanite, 2)
        ))
        .WithFabricatorType(CraftTree.Type.Workbench)
        .WithCraftingTime(5.0f);

        prefab.SetVehicleUpgradeModule(EquipmentType.SeamothModule, QuickSlotType.Passive)
            .WithDepthUpgrade(3500f)
            .WithOnModuleAdded((Vehicle vehicle, int slotId) =>
            {
                ErrorMessage.AddMessage("Abyssal Pressure Compensator Online. Maximum crush depth extended to 3,500m.");
            })
            .WithOnModuleRemoved((Vehicle vehicle, int slotId) =>
            {
                ErrorMessage.AddMessage("Warning: Abyssal Pressure Compensator Removed!");
            });

        prefab.SetPdaGroupCategory(TechGroup.VehicleUpgrades, TechCategory.VehicleUpgrades);

        prefab.Register();
    }
}
