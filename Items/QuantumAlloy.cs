using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using UnityEngine;

namespace CallOfTheAbyss.Items;

public static class QuantumAlloy
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType(
        "QuantumAlloy",
        "Quantum Alloy Ingot",
        "An advanced composite ingot forged by compressing titanium, diamond matrices, and resonated abyssal crystals. Yields extraordinary tensile strength."
    ).WithIcon(SpriteManager.Get(TechType.PlasteelIngot));

    public static void Register()
    {
        CustomPrefab prefab = new CustomPrefab(Info);

        CloneTemplate clone = new CloneTemplate(Info, TechType.PlasteelIngot)
        {
            ModifyPrefab = obj =>
            {
                var renderers = obj.GetComponentsInChildren<Renderer>();
                foreach (var renderer in renderers)
                {
                    foreach (var mat in renderer.materials)
                    {
                        mat.color = new Color(0.12f, 0.22f, 0.35f);
                    }
                }
            }
        };

        prefab.SetGameObject(clone);

        prefab.SetRecipe(new RecipeData(
            new Ingredient(TechType.PlasteelIngot, 1),
            new Ingredient(AbyssalCrystal.Info.TechType, 2),
            new Ingredient(TechType.Diamond, 1),
            new Ingredient(TechType.Lead, 1)
        ))
        .WithFabricatorType(CraftTree.Type.Fabricator)
        .WithStepsToFabricatorTab("Resources", "AdvancedMaterials")
        .WithCraftingTime(3.5f);

        prefab.SetPdaGroupCategory(TechGroup.Resources, TechCategory.AdvancedMaterials);

        prefab.Register();
    }
}
