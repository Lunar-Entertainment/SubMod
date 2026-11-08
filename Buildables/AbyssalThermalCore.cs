using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using Nautilus.Utility;
using CallOfTheAbyss.Items;
using UnityEngine;

namespace CallOfTheAbyss.Buildables;

public static class AbyssalThermalCore
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType(
        "AbyssalThermalCore",
        "Abyssal Geothermal Core",
        "Deep sea power reactor harnessing tectonic and hydrothermal gradient currents to produce continuous high-wattage electricity for seabases."
    ).WithIcon(SpriteManager.Get(TechType.ThermalPlant));

    public static void Register()
    {
        CustomPrefab prefab = new CustomPrefab(Info);

        CloneTemplate clone = new CloneTemplate(Info, TechType.ThermalPlant);
        clone.ModifyPrefab += obj =>
        {
            ConstructableFlags flags = ConstructableFlags.Outside | ConstructableFlags.Ground | ConstructableFlags.Rotatable | ConstructableFlags.Base;

            var renderers = obj.GetComponentsInChildren<Renderer>();
            foreach (var r in renderers)
            {
                foreach (var mat in r.materials)
                {
                    mat.color = new Color(0.2f, 0.45f, 0.6f);
                }
            }

            PrefabUtils.AddConstructable(obj, Info.TechType, flags, obj);

            var powerSource = obj.GetComponent<PowerSource>();
            if (powerSource != null)
            {
                powerSource.maxPower = 500f;
            }
        };

        prefab.SetGameObject(clone);

        prefab.SetPdaGroupCategory(TechGroup.ExteriorModules, TechCategory.ExteriorModule);

        prefab.SetRecipe(new RecipeData(
            new Ingredient(TechType.TitaniumIngot, 2),
            new Ingredient(QuantumAlloy.Info.TechType, 2),
            new Ingredient(TechType.Aerogel, 2),
            new Ingredient(TechType.WiringKit, 1)
        ))
        .WithFabricatorType(CraftTree.Type.Constructor)
        .WithCraftingTime(6.0f);

        prefab.Register();
    }
}
