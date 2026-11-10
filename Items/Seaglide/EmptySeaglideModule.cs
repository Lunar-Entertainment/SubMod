using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using UnityEngine;

namespace CallOfTheAbyss.Items.Seaglide;

public static class EmptySeaglideModule
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType(
        "EmptySeaglideModule",
        "Empty Seaglide Module Chassis",
        "An unprogrammed modular chassis for the Modular Seaglide. Combined with a standard Seaglide to construct a Modular Seaglide, or upgraded with specialized circuitry into functional modules."
    ).WithIcon(SpriteManager.Get(TechType.ComputerChip));

    public static void Register()
    {
        CustomPrefab prefab = new CustomPrefab(Info);

        CloneTemplate clone = new CloneTemplate(Info, TechType.ComputerChip)
        {
            ModifyPrefab = obj =>
            {
                var renderers = obj.GetComponentsInChildren<Renderer>();
                foreach (var r in renderers)
                {
                    foreach (var mat in r.materials)
                    {
                        mat.color = new Color(0.2f, 0.7f, 0.85f);
                    }
                }
            }
        };

        prefab.SetGameObject(clone);

        prefab.SetRecipe(new RecipeData(
            new Ingredient(TechType.Titanium, 1),
            new Ingredient(TechType.WiringKit, 1),
            new Ingredient(TechType.ComputerChip, 1)
        ))
        .WithFabricatorType(CraftTree.Type.Fabricator)
        .WithStepsToFabricatorTab("Personal", "Equipment")
        .WithCraftingTime(2.5f);

        prefab.SetPdaGroupCategory(TechGroup.Personal, TechCategory.Equipment);

        prefab.Register();
    }
}
