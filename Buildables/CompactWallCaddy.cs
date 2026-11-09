using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using Nautilus.Utility;
using UnityEngine;

namespace CallOfTheAbyss.Buildables;

public static class CompactWallCaddy
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType(
        "CompactWallCaddy",
        "Compact Wall Caddy",
        "Compact wall-mounted organizer providing 25 storage slots (5x5). Economical to construct in early outposts and lifepod quarters."
    ).WithIcon(SpriteManager.Get(TechType.SmallLocker));

    public static void Register()
    {
        CustomPrefab prefab = new CustomPrefab(Info);

        CloneTemplate clone = new CloneTemplate(Info, TechType.SmallLocker);
        clone.ModifyPrefab += obj =>
        {
            ConstructableFlags flags = ConstructableFlags.Inside | ConstructableFlags.Wall | ConstructableFlags.Base | ConstructableFlags.Submarine;

            var renderers = obj.GetComponentsInChildren<Renderer>();
            foreach (var r in renderers)
            {
                foreach (var mat in r.materials)
                {
                    mat.color = new Color(0.18f, 0.28f, 0.35f);
                }
            }

            PrefabUtils.AddConstructable(obj, Info.TechType, flags, obj);
            PrefabUtils.AddStorageContainer(obj, "StorageRoot", "Wall Storage Caddy", 5, 5, true);
        };

        prefab.SetGameObject(clone);

        prefab.SetPdaGroupCategory(TechGroup.InteriorModules, TechCategory.InteriorModule);

        prefab.SetRecipe(new RecipeData(
            new Ingredient(TechType.Titanium, 1),
            new Ingredient(TechType.Quartz, 1)
        ))
        .WithFabricatorType(CraftTree.Type.Constructor)
        .WithCraftingTime(2.0f);

        prefab.Register();
    }
}
