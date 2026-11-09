using System.Collections.Generic;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using Nautilus.Utility;
using UnityEngine;

namespace CallOfTheAbyss.Buildables.InWall;

public static class InWallStorageNiche
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType(
        "InWallStorageNiche",
        "Recessed In-Wall Storage Niche",
        "Flush-mounted wall storage compartment embedded directly into base bulkhead walls. Provides 36 slots without obstructing narrow corridor walking paths."
    ).WithIcon(SpriteManager.Get(TechType.SmallLocker));

    public static void Register()
    {
        CustomPrefab prefab = new CustomPrefab(Info);

        CloneTemplate clone = new CloneTemplate(Info, TechType.SmallLocker);
        clone.ModifyPrefab += obj =>
        {
            ConfigurePrefab(obj);
        };

        prefab.SetGameObject(clone);

        prefab.SetPdaGroupCategory(TechGroup.InteriorModules, TechCategory.InteriorModule);

        prefab.SetRecipe(new RecipeData(
            new Ingredient(TechType.Titanium, 2),
            new Ingredient(TechType.Quartz, 1)
        ))
        .WithFabricatorType(CraftTree.Type.Constructor)
        .WithCraftingTime(2.5f);

        prefab.Register();
    }

    public static void ConfigurePrefab(GameObject obj)
    {
        ConstructableFlags flags = ConstructableFlags.Inside | ConstructableFlags.Wall | ConstructableFlags.Base | ConstructableFlags.Submarine;

        var renderers = obj.GetComponentsInChildren<Renderer>();
        foreach (var r in renderers)
        {
            foreach (var mat in r.materials)
            {
                mat.color = new Color(0.12f, 0.28f, 0.38f);
            }
        }

        PrefabUtils.AddConstructable(obj, Info.TechType, flags, obj);
        PrefabUtils.AddStorageContainer(obj, "StorageRoot", "In-Wall Niche", 6, 6, true);

        var module = obj.GetComponent<InWallModule>();
        if (module == null)
        {
            module = obj.AddComponent<InWallModule>();
        }
        module.PartName = "In-Wall Storage Niche";
        module.RefundMaterials = new List<TechType> { TechType.Titanium, TechType.Titanium, TechType.Quartz };
    }

    public static GameObject CreateInWallPart(Vector3 position, Quaternion rotation, Transform parent)
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.name = "InWallStorageNiche";
        go.transform.position = position;
        go.transform.rotation = rotation;
        go.transform.SetParent(parent, true);
        go.transform.localScale = new Vector3(0.8f, 1.2f, 0.15f);

        ConfigurePrefab(go);
        return go;
    }
}
