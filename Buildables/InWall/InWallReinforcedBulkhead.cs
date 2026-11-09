using System.Collections.Generic;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using Nautilus.Utility;
using UnityEngine;

namespace CallOfTheAbyss.Buildables.InWall;

public static class InWallReinforcedBulkhead
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType(
        "InWallReinforcedBulkhead",
        "In-Wall Structural Reinforcement Bulkhead",
        "Heavy-duty carbon-titanium composite partition embedded directly into interior walls. Dramatically bolsters structural integrity (+7.0 Hull Strength) against extreme trench pressures."
    ).WithIcon(SpriteManager.Get(TechType.BaseReinforcement));

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
            new Ingredient(TechType.Lithium, 2)
        ))
        .WithFabricatorType(CraftTree.Type.Constructor)
        .WithCraftingTime(3.5f);

        prefab.Register();
    }

    public static void ConfigurePrefab(GameObject obj)
    {
        ConstructableFlags flags = ConstructableFlags.Inside | ConstructableFlags.Wall | ConstructableFlags.Base | ConstructableFlags.Submarine;

        var storage = obj.GetComponent<StorageContainer>();
        if (storage != null)
        {
            Object.DestroyImmediate(storage);
        }

        var renderers = obj.GetComponentsInChildren<Renderer>();
        foreach (var r in renderers)
        {
            foreach (var mat in r.materials)
            {
                mat.color = new Color(0.18f, 0.22f, 0.26f);
            }
        }

        PrefabUtils.AddConstructable(obj, Info.TechType, flags, obj);

        var module = obj.GetComponent<InWallModule>();
        if (module == null)
        {
            module = obj.AddComponent<InWallModule>();
        }
        module.PartName = "In-Wall Structural Reinforcement Bulkhead";
        module.AddedHullStrength = 7.0f;
        module.RefundMaterials = new List<TechType> { TechType.Titanium, TechType.Titanium, TechType.Lithium, TechType.Lithium };
    }

    public static GameObject CreateInWallPart(Vector3 position, Quaternion rotation, Transform parent)
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.name = "InWallReinforcedBulkhead";
        go.transform.position = position;
        go.transform.rotation = rotation;
        go.transform.SetParent(parent, true);
        go.transform.localScale = new Vector3(0.9f, 1.4f, 0.15f);

        ConfigurePrefab(go);
        return go;
    }
}
