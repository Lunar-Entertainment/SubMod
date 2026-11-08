using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using UnityEngine;

namespace CallOfTheAbyss.Items;

public static class AbyssalStasisHarpoon
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType(
        "AbyssalStasisHarpoon",
        "Abyssal Kinetic Harpoon",
        "Personal defense tool utilizing concentrated electromagnetic stasis fields to halt deep trench predators and leviathans."
    ).WithIcon(SpriteManager.Get(TechType.StasisRifle));

    public static void Register()
    {
        CustomPrefab prefab = new CustomPrefab(Info);

        CloneTemplate clone = new CloneTemplate(Info, TechType.StasisRifle)
        {
            ModifyPrefab = obj =>
            {
                var renderers = obj.GetComponentsInChildren<Renderer>();
                foreach (var renderer in renderers)
                {
                    foreach (var mat in renderer.materials)
                    {
                        mat.color = new Color(0.18f, 0.45f, 0.65f);
                    }
                }
            }
        };

        prefab.SetGameObject(clone);

        prefab.SetEquipment(EquipmentType.Hand);

        prefab.SetRecipe(new RecipeData(
            new Ingredient(TechType.StasisRifle, 1),
            new Ingredient(QuantumAlloy.Info.TechType, 1),
            new Ingredient(AbyssalCrystal.Info.TechType, 2),
            new Ingredient(TechType.PrecursorIonBattery, 1)
        ))
        .WithFabricatorType(CraftTree.Type.Workbench)
        .WithCraftingTime(4.0f);

        prefab.SetPdaGroupCategory(TechGroup.Personal, TechCategory.Tools);

        prefab.Register();
    }
}
