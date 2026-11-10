using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using UnityEngine;

namespace CallOfTheAbyss.Items.Seaglide;

public static class SeaglideSpeedModule
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType(
        "SeaglideSpeedModule",
        "Seaglide Speed Overdrive Module",
        "High-torque electromagnetic thruster module for the Modular Seaglide. Increases underwater propulsion speed by +35%."
    ).WithIcon(SpriteManager.Get(TechType.VehicleHullModule1));

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
                        mat.color = new Color(0.1f, 0.85f, 0.4f);
                    }
                }
            }
        };

        prefab.SetGameObject(clone);

        prefab.SetRecipe(new RecipeData(
            new Ingredient(EmptySeaglideModule.Info.TechType, 1),
            new Ingredient(TechType.Magnetite, 1),
            new Ingredient(TechType.Lubricant, 1)
        ))
        .WithFabricatorType(CraftTree.Type.Fabricator)
        .WithStepsToFabricatorTab("Personal", "Equipment")
        .WithCraftingTime(3.0f);

        prefab.SetPdaGroupCategory(TechGroup.Personal, TechCategory.Equipment);

        prefab.Register();
    }
}
