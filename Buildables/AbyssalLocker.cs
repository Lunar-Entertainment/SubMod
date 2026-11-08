using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using Nautilus.Utility;
using CallOfTheAbyss.Items;
using UnityEngine;

namespace CallOfTheAbyss.Buildables;

public static class AbyssalLocker
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType(
        "AbyssalLocker",
        "Abyssal Titanium Locker",
        "Reinforced high-capacity containment locker engineered with Quantum Alloy framing. Provides 80 inventory storage slots."
    ).WithIcon(SpriteManager.Get(TechType.Locker));

    public static void Register()
    {
        CustomPrefab prefab = new CustomPrefab(Info);

        CloneTemplate clone = new CloneTemplate(Info, TechType.Locker);
        clone.ModifyPrefab += obj =>
        {
            ConstructableFlags flags = ConstructableFlags.Inside | ConstructableFlags.Ground | ConstructableFlags.Rotatable | ConstructableFlags.Submarine;
            
            // Set dark composite visual style
            var renderers = obj.GetComponentsInChildren<Renderer>();
            foreach (var r in renderers)
            {
                foreach (var mat in r.materials)
                {
                    mat.color = new Color(0.12f, 0.2f, 0.32f);
                }
            }

            // Configure constructable component
            PrefabUtils.AddConstructable(obj, Info.TechType, flags, obj);

            // Configure 8x10 capacity storage container (80 slots)
            PrefabUtils.AddStorageContainer(obj, "StorageRoot", "Abyssal Locker", 8, 10, true);
        };

        prefab.SetGameObject(clone);

        prefab.SetPdaGroupCategory(TechGroup.InteriorModules, TechCategory.InteriorModule);

        prefab.SetRecipe(new RecipeData(
            new Ingredient(TechType.TitaniumIngot, 2),
            new Ingredient(QuantumAlloy.Info.TechType, 1),
            new Ingredient(TechType.Glass, 1)
        ))
        .WithFabricatorType(CraftTree.Type.Constructor)
        .WithCraftingTime(3.0f);

        prefab.Register();
    }
}
