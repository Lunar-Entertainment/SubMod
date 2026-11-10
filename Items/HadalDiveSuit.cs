using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using UnityEngine;

namespace CallOfTheAbyss.Items;

public static class HadalDiveSuit
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType(
        "HadalHeavyDiveSuit",
        "Hadal Heavy Dive Suit",
        "Experimental hyperbaric atmospheric dive suit lined with quantum mesh and liquid fluorocarbon pressure dampers. Protects the diver against crushing barometric pressures up to 8,500 meters and frigid hadal waters in the deep chasm."
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
                        mat.color = new Color(0.2f, 0.1f, 0.35f);
                    }
                }
            }
        };

        prefab.SetGameObject(clone);

        // Body equipment slot
        prefab.SetEquipment(EquipmentType.Body);

        prefab.SetRecipe(new RecipeData(
            new Ingredient(ThermalDiveSuit.Info.TechType, 1),
            new Ingredient(QuantumAlloy.Info.TechType, 2),
            new Ingredient(AbyssalCrystal.Info.TechType, 3),
            new Ingredient(TechType.Kyanite, 2),
            new Ingredient(TechType.Polyaniline, 2)
        ))
        .WithFabricatorType(CraftTree.Type.Workbench)
        .WithCraftingTime(6.0f);

        prefab.SetPdaGroupCategory(TechGroup.Personal, TechCategory.Equipment);

        prefab.Register();
    }
}
