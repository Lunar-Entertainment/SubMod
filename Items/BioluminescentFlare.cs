using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using UnityEngine;

namespace CallOfTheAbyss.Items;

public static class BioluminescentFlare
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType(
        "AbyssalFlare",
        "Abyssal Luminescent Flare",
        "Long-duration chemical lighting flare infused with deep trench bioluminescent minerals. Burns with intense blue-green light, piercing murky depths."
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
                    light.color = new Color(0.1f, 0.9f, 0.8f);
                    light.range = 35f;
                    light.intensity = 2.0f;
                }

                var renderers = obj.GetComponentsInChildren<Renderer>();
                foreach (var r in renderers)
                {
                    foreach (var mat in r.materials)
                    {
                        mat.color = new Color(0.2f, 0.85f, 0.75f);
                    }
                }
            }
        };

        prefab.SetGameObject(clone);

        prefab.SetRecipe(new RecipeData(
            new Ingredient(TechType.Flare, 1),
            new Ingredient(AbyssalCrystal.Info.TechType, 1)
        ))
        .WithFabricatorType(CraftTree.Type.Fabricator)
        .WithStepsToFabricatorTab("Personal", "Tools")
        .WithCraftingTime(2.0f);

        prefab.SetPdaGroupCategory(TechGroup.Personal, TechCategory.Tools);

        prefab.Register();
    }
}
