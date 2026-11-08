using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using Nautilus.Utility;
using CallOfTheAbyss.Items;
using UnityEngine;

namespace CallOfTheAbyss.Buildables;

public static class DeepSeaPurifier
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType(
        "DeepSeaPurifier",
        "Abyssal Desalination Station",
        "High-pressure reverse-osmosis filtration console. Rapidly extracts potable filtered water and industrial salt crystals from extreme-depth oceanic brine."
    ).WithIcon(SpriteManager.Get(TechType.FiltrationMachine));

    public static void Register()
    {
        CustomPrefab prefab = new CustomPrefab(Info);

        CloneTemplate clone = new CloneTemplate(Info, TechType.FiltrationMachine);
        clone.ModifyPrefab += obj =>
        {
            ConstructableFlags flags = ConstructableFlags.Inside | ConstructableFlags.Wall | ConstructableFlags.Base;

            var renderers = obj.GetComponentsInChildren<Renderer>();
            foreach (var r in renderers)
            {
                foreach (var mat in r.materials)
                {
                    mat.color = new Color(0.18f, 0.35f, 0.5f);
                }
            }

            PrefabUtils.AddConstructable(obj, Info.TechType, flags, obj);
        };

        prefab.SetGameObject(clone);

        prefab.SetPdaGroupCategory(TechGroup.InteriorModules, TechCategory.InteriorModule);

        prefab.SetRecipe(new RecipeData(
            new Ingredient(TechType.TitaniumIngot, 2),
            new Ingredient(QuantumAlloy.Info.TechType, 1),
            new Ingredient(TechType.CopperWire, 1),
            new Ingredient(TechType.ComputerChip, 1)
        ))
        .WithFabricatorType(CraftTree.Type.Constructor)
        .WithCraftingTime(5.0f);

        prefab.Register();
    }
}
