using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using UnityEngine;

namespace CallOfTheAbyss.Items;

public static class InsulatedWetsuit
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType(
        "InsulatedWetsuit",
        "Insulated Neoprene Wetsuit",
        "Thermal-insulated dive suit synthesized from treated silicone rubber and creepvine mesh. Traps body warmth, drastically slowing hypothermia onset in waters down to 450 meters."
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
                        mat.color = new Color(0.18f, 0.38f, 0.42f);
                    }
                }
            }
        };

        prefab.SetGameObject(clone);

        prefab.SetEquipment(EquipmentType.Body);

        prefab.SetRecipe(new RecipeData(
            new Ingredient(TechType.FiberMesh, 2),
            new Ingredient(TechType.Silicone, 2),
            new Ingredient(TechType.StalkerTooth, 1)
        ))
        .WithFabricatorType(CraftTree.Type.Fabricator)
        .WithStepsToFabricatorTab("Personal", "Equipment")
        .WithCraftingTime(4.0f);

        prefab.SetPdaGroupCategory(TechGroup.Personal, TechCategory.Equipment);

        prefab.Register();
    }
}
