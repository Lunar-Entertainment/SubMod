using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using UnityEngine;

namespace CallOfTheAbyss.Items;

public static class CartographicHUDChip
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType(
        "CartographicHUDChip",
        "Abyssal Cartographic HUD Chip",
        "Personal navigational HUD coprocessor. Continuously renders real-time topographic coordinates (X, Y, Z), current biome telemetry, and distance to seabed onto the diving mask visor."
    ).WithIcon(SpriteManager.Get(TechType.Compass));

    public static void Register()
    {
        CustomPrefab prefab = new CustomPrefab(Info);

        CloneTemplate clone = new CloneTemplate(Info, TechType.Compass)
        {
            ModifyPrefab = obj =>
            {
                var renderers = obj.GetComponentsInChildren<Renderer>();
                foreach (var r in renderers)
                {
                    foreach (var mat in r.materials)
                    {
                        mat.color = new Color(0.1f, 0.85f, 0.95f);
                    }
                }
            }
        };

        prefab.SetGameObject(clone);

        prefab.SetEquipment(EquipmentType.Chip);

        prefab.SetRecipe(new RecipeData(
            new Ingredient(TechType.Compass, 1),
            new Ingredient(TechType.ComputerChip, 1),
            new Ingredient(TechType.CopperWire, 1)
        ))
        .WithFabricatorType(CraftTree.Type.Fabricator)
        .WithStepsToFabricatorTab("Personal", "Equipment")
        .WithCraftingTime(3.0f);

        prefab.SetPdaGroupCategory(TechGroup.Personal, TechCategory.Equipment);

        prefab.Register();
    }
}
