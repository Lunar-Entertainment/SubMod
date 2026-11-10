using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using UnityEngine;

namespace CallOfTheAbyss.Items.Seaglide;

public static class SeaglideBatteryModule
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType(
        "SeaglideBatteryModule",
        "Seaglide Efficiency & Capacitor Module",
        "Superconducting micro-capacitor matrix for the Modular Seaglide. Reduces motor power drain by 65%, more than doubling battery operational runtime."
    ).WithIcon(SpriteManager.Get(TechType.Battery));

    public static void Register()
    {
        CustomPrefab prefab = new CustomPrefab(Info);

        CloneTemplate clone = new CloneTemplate(Info, TechType.VehicleHullModule1)
        {
            ModifyPrefab = obj =>
            {
                var renderers = obj.GetComponentsInChildren<Renderer>();
                foreach (var r in renderers)
                {
                    foreach (var mat in r.materials)
                    {
                        mat.color = new Color(0.95f, 0.75f, 0.1f);
                    }
                }
            }
        };

        prefab.SetGameObject(clone);

        prefab.SetRecipe(new RecipeData(
            new Ingredient(EmptySeaglideModule.Info.TechType, 1),
            new Ingredient(TechType.Battery, 1),
            new Ingredient(TechType.CopperWire, 1)
        ))
        .WithFabricatorType(CraftTree.Type.Fabricator)
        .WithStepsToFabricatorTab("Personal", "Equipment")
        .WithCraftingTime(3.0f);

        prefab.SetPdaGroupCategory(TechGroup.Personal, TechCategory.Equipment);

        prefab.Register();
    }
}
