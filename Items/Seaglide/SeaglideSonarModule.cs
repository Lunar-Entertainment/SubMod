using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using UnityEngine;

namespace CallOfTheAbyss.Items.Seaglide;

public static class SeaglideSonarModule
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType(
        "SeaglideSonarModule",
        "Seaglide Acoustic Telemetry Module",
        "Acoustic micro-transponder for the Modular Seaglide. Projects real-time HUD telemetry detailing current seabed clearance, cavern ceiling distance, and ambient water temperature."
    ).WithIcon(SpriteManager.Get(TechType.Compass));

    public static void Register()
    {
        CustomPrefab prefab = new CustomPrefab(Info);

        CloneTemplate clone = new CloneTemplate(Info, TechType.VehicleHullModule1)
        {
            ModifyPrefab = obj =>
            {
                var renderers = obj.GetComponentsInChildren<Renderer>();
                foreach (var r in renderers)
                {
                    foreach (var mat in r.materials)
                    {
                        mat.color = new Color(0.2f, 0.45f, 0.95f);
                    }
                }
            }
        };

        prefab.SetGameObject(clone);

        prefab.SetRecipe(new RecipeData(
            new Ingredient(EmptySeaglideModule.Info.TechType, 1),
            new Ingredient(TechType.CopperWire, 1),
            new Ingredient(TechType.Quartz, 1)
        ))
        .WithFabricatorType(CraftTree.Type.Fabricator)
        .WithStepsToFabricatorTab("Personal", "Equipment")
        .WithCraftingTime(3.0f);

        prefab.SetPdaGroupCategory(TechGroup.Personal, TechCategory.Equipment);

        prefab.Register();
    }
}
