using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using UnityEngine;

namespace CallOfTheAbyss.Items;

public static class BioluminescentChemStick
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType(
        "BioluminescentChemStick",
        "Bioluminescent Chem-Stick",
        "A chemical glowstick charged with reactive Acid Mushroom luminescence. Radiates a soothing emerald glow to illuminate tight cave pockets and attract inquisitive small fish."
    ).WithIcon(SpriteManager.Get(TechType.Flare));

    public static void Register()
    {
        CustomPrefab prefab = new CustomPrefab(Info);

        CloneTemplate clone = new CloneTemplate(Info, TechType.Flare)
        {
            ModifyPrefab = obj =>
            {
                var lights = obj.GetComponentsInChildren<Light>();
                foreach (var light in lights)
                {
                    light.color = new Color(0.1f, 0.95f, 0.55f);
                    light.range = 22f;
                    light.intensity = 1.6f;
                }

                var renderers = obj.GetComponentsInChildren<Renderer>();
                foreach (var r in renderers)
                {
                    foreach (var mat in r.materials)
                    {
                        mat.color = new Color(0.15f, 0.9f, 0.45f);
                    }
                }
            }
        };

        prefab.SetGameObject(clone);

        prefab.SetRecipe(new RecipeData(
            new Ingredient(TechType.AcidMushroom, 2),
            new Ingredient(TechType.Titanium, 1)
        ))
        .WithFabricatorType(CraftTree.Type.Fabricator)
        .WithStepsToFabricatorTab("Personal", "Tools")
        .WithCraftingTime(1.5f);

        prefab.SetPdaGroupCategory(TechGroup.Personal, TechCategory.Tools);

        prefab.Register();
    }
}
