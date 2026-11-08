using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using UnityEngine;

namespace CallOfTheAbyss.Items;

public static class ThermalDiveSuit
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType(
        "AbyssalThermalSuit",
        "Abyssal Thermal Dive Suit",
        "Deep-dive atmospheric diving suit insulated with aerogel and quantum micro-composites. Shields the wearer from sub-zero ocean temperatures and withstands extreme pressure up to 3500 meters."
    ).WithIcon(SpriteManager.Get(TechType.ReinforcedDiveSuit));

    public static void Register()
    {
        CustomPrefab prefab = new CustomPrefab(Info);

        CloneTemplate clone = new CloneTemplate(Info, TechType.ReinforcedDiveSuit)
        {
            ModifyPrefab = obj =>
            {
                var renderers = obj.GetComponentsInChildren<Renderer>();
                foreach (var renderer in renderers)
                {
                    foreach (var mat in renderer.materials)
                    {
                        mat.color = new Color(0.15f, 0.25f, 0.4f);
                    }
                }
            }
        };

        prefab.SetGameObject(clone);

        // Body equipment slot
        prefab.SetEquipment(EquipmentType.Body);

        prefab.SetRecipe(new RecipeData(
            new Ingredient(TechType.ReinforcedDiveSuit, 1),
            new Ingredient(QuantumAlloy.Info.TechType, 2),
            new Ingredient(AbyssalCrystal.Info.TechType, 2),
            new Ingredient(TechType.Aerogel, 2),
            new Ingredient(TechType.FiberMesh, 1)
        ))
        .WithFabricatorType(CraftTree.Type.Workbench)
        .WithCraftingTime(5.0f);

        prefab.SetPdaGroupCategory(TechGroup.Personal, TechCategory.Equipment);

        prefab.Register();
    }
}
