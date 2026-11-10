using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using UnityEngine;

namespace CallOfTheAbyss.Items.Seaglide;

public static class SeaglideMapModule
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType(
        "SeaglideMapModule",
        "Seaglide Holographic Cartography Module",
        "Advanced wide-spectrum cartographic imaging array for the Modular Seaglide. Quadruples holographic terrain scanning range to 120m, clears forward visual obstructions, and projects high-contrast bioluminescent topography."
    ).WithIcon(SpriteManager.Get(TechType.MapRoomUpgradeScanRange));

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
                        mat.color = new Color(0.1f, 0.9f, 0.95f);
                    }
                }
            }
        };

        prefab.SetGameObject(clone);

        prefab.SetRecipe(new RecipeData(
            new Ingredient(EmptySeaglideModule.Info.TechType, 1),
            new Ingredient(TechType.ComputerChip, 1),
            new Ingredient(TechType.Magnetite, 1),
            new Ingredient(TechType.Glass, 1)
        ))
        .WithFabricatorType(CraftTree.Type.Fabricator)
        .WithStepsToFabricatorTab("Personal", "Equipment")
        .WithCraftingTime(3.5f);

        prefab.SetPdaGroupCategory(TechGroup.Personal, TechCategory.Equipment);

        prefab.Register();
    }
}
