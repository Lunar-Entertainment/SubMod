using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using UnityEngine;

namespace CallOfTheAbyss.Items;

public static class StreamlinedFins
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType(
        "StreamlinedFins",
        "Hydrodynamic Streamlined Fins",
        "Lightweight hydrodynamic fins reinforced with Creepvine fiber threading and flexible titanium ribs. Increases unassisted swim velocity by 15% before advanced modification benches are accessible."
    ).WithIcon(SpriteManager.Get(TechType.Fins));

    public static void Register()
    {
        CustomPrefab prefab = new CustomPrefab(Info);

        CloneTemplate clone = new CloneTemplate(Info, TechType.Fins)
        {
            ModifyPrefab = obj =>
            {
                var renderers = obj.GetComponentsInChildren<Renderer>();
                foreach (var renderer in renderers)
                {
                    foreach (var mat in renderer.materials)
                    {
                        mat.color = new Color(0.1f, 0.7f, 0.75f);
                    }
                }
            }
        };

        prefab.SetGameObject(clone);

        prefab.SetEquipment(EquipmentType.Foots);

        prefab.SetRecipe(new RecipeData(
            new Ingredient(TechType.Fins, 1),
            new Ingredient(TechType.FiberMesh, 1),
            new Ingredient(TechType.Titanium, 1)
        ))
        .WithFabricatorType(CraftTree.Type.Fabricator)
        .WithStepsToFabricatorTab("Personal", "Equipment")
        .WithCraftingTime(3.0f);

        prefab.SetPdaGroupCategory(TechGroup.Personal, TechCategory.Equipment);

        prefab.Register();
    }
}
