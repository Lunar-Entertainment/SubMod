using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using UnityEngine;

namespace CallOfTheAbyss.Items.Seaglide;

public static class SeaglideOxygenModule
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType(
        "SeaglideOxygenModule",
        "Seaglide Rebreather & O2 Injection Module",
        "Miniaturized ocean water electrolysis and rebreather cartridge for the Modular Seaglide. Continuously injects fresh oxygen into the diver's supply while gliding."
    ).WithIcon(SpriteManager.Get(TechType.Rebreather));

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
                        mat.color = new Color(0.1f, 0.75f, 0.95f);
                    }
                }
            }
        };

        prefab.SetGameObject(clone);

        prefab.SetRecipe(new RecipeData(
            new Ingredient(EmptySeaglideModule.Info.TechType, 1),
            new Ingredient(TechType.FiberMesh, 1),
            new Ingredient(TechType.AirBladder, 1)
        ))
        .WithFabricatorType(CraftTree.Type.Fabricator)
        .WithStepsToFabricatorTab("Personal", "Equipment")
        .WithCraftingTime(3.0f);

        prefab.SetPdaGroupCategory(TechGroup.Personal, TechCategory.Equipment);

        prefab.Register();
    }
}
