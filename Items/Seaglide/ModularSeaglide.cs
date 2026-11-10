using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using UnityEngine;

namespace CallOfTheAbyss.Items.Seaglide;

public static class ModularSeaglide
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType(
        "ModularSeaglide",
        "Modular Seaglide",
        "Advanced customizable Seaglide chassis equipped with an internal expansion socket. Supports hot-swappable speed, rebreather oxygen, power capacitor, and acoustic telemetry modules."
    ).WithIcon(SpriteManager.Get(TechType.Seaglide));

    public static void Register()
    {
        CustomPrefab prefab = new CustomPrefab(Info);

        CloneTemplate clone = new CloneTemplate(Info, TechType.Seaglide)
        {
            ModifyPrefab = obj =>
            {
                // Attach our ModularSeaglideController
                if (obj.GetComponent<ModularSeaglideController>() == null)
                {
                    obj.AddComponent<ModularSeaglideController>();
                }

                // Apply sleek cobalt composite body with glowing cyan trim
                var renderers = obj.GetComponentsInChildren<Renderer>();
                foreach (var renderer in renderers)
                {
                    foreach (var mat in renderer.materials)
                    {
                        mat.color = new Color(0.12f, 0.45f, 0.78f);
                        if (mat.HasProperty("_SpecColor"))
                            mat.SetColor("_SpecColor", new Color(0.2f, 0.85f, 1.0f));
                    }
                }
            }
        };

        prefab.SetGameObject(clone);

        // Recipe: 1 Seaglide + 1 Empty Seaglide Module Chassis
        prefab.SetRecipe(new RecipeData(
            new Ingredient(TechType.Seaglide, 1),
            new Ingredient(EmptySeaglideModule.Info.TechType, 1)
        ))
        .WithFabricatorType(CraftTree.Type.Workbench)
        .WithCraftingTime(4.0f);

        prefab.SetPdaGroupCategory(TechGroup.Personal, TechCategory.Tools);

        prefab.Register();
    }
}
