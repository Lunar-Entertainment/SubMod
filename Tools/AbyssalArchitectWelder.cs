using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using UnityEngine;

namespace CallOfTheAbyss.Tools;

public static class AbyssalArchitectWelder
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType(
        "AbyssalArchitectWelder",
        "Abyssal Architectural Welder",
        "Advanced quantum structural welder engineered for deep seabase interior fabrication. Interfaces with internal bulkhead walls to install recessed in-wall modules and dematerialize existing internal wall sections."
    ).WithIcon(SpriteManager.Get(TechType.Builder));

    public static void Register()
    {
        CustomPrefab prefab = new CustomPrefab(Info);

        CloneTemplate clone = new CloneTemplate(Info, TechType.Builder)
        {
            ModifyPrefab = obj =>
            {
                // Remove standard BuilderTool and attach our custom AbyssalArchitectTool
                var standardBuilder = obj.GetComponent<BuilderTool>();
                if (standardBuilder != null)
                {
                    Object.DestroyImmediate(standardBuilder);
                }

                if (obj.GetComponent<AbyssalArchitectTool>() == null)
                {
                    obj.AddComponent<AbyssalArchitectTool>();
                }

                // Apply sleek deep-void indigo styling with cyan glow
                var renderers = obj.GetComponentsInChildren<Renderer>();
                foreach (var renderer in renderers)
                {
                    foreach (var mat in renderer.materials)
                    {
                        mat.color = new Color(0.16f, 0.24f, 0.42f);
                        if (mat.HasProperty("_SpecColor"))
                            mat.SetColor("_SpecColor", new Color(0.2f, 0.8f, 1.0f));
                    }
                }
            }
        };

        prefab.SetGameObject(clone);

        // Middle-late game recipe
        prefab.SetRecipe(new RecipeData(
            new Ingredient(TechType.Builder, 1),
            new Ingredient(TechType.AdvancedWiringKit, 1),
            new Ingredient(TechType.Magnetite, 2),
            new Ingredient(TechType.Diamond, 1)
        ))
        .WithFabricatorType(CraftTree.Type.Workbench)
        .WithCraftingTime(4.5f);

        prefab.SetPdaGroupCategory(TechGroup.Personal, TechCategory.Tools);

        prefab.Register();
    }
}
