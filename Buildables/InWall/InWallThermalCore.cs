using System.Collections.Generic;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using Nautilus.Utility;
using UnityEngine;

namespace CallOfTheAbyss.Buildables.InWall;

public static class InWallThermalCore
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType(
        "InWallThermalCore",
        "In-Wall Convection Thermal Core",
        "Internal heating coil embedded directly into seabase bulkheads. Radiates warmth to stabilize base interior temperatures and reinforces structural hull integrity (+3.0 Hull Strength)."
    ).WithIcon(SpriteManager.Get(TechType.ThermalPlant));

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
            new Ingredient(TechType.Titanium, 1),
            new Ingredient(TechType.Magnetite, 1),
            new Ingredient(TechType.CopperWire, 1)
        ))
        .WithFabricatorType(CraftTree.Type.Constructor)
        .WithCraftingTime(3.0f);

        prefab.Register();
    }

    public static void ConfigurePrefab(GameObject obj)
    {
        ConstructableFlags flags = ConstructableFlags.Inside | ConstructableFlags.Wall | ConstructableFlags.Base | ConstructableFlags.Submarine;

        // Remove storage if cloned from SmallLocker
        var storage = obj.GetComponent<StorageContainer>();
        if (storage != null)
        {
            Object.DestroyImmediate(storage);
        }

        // Radiant heating core styling
        var renderers = obj.GetComponentsInChildren<Renderer>();
        foreach (var r in renderers)
        {
            foreach (var mat in r.materials)
            {
                mat.color = new Color(0.9f, 0.45f, 0.1f);
            }
        }

        // Add soft ambient thermal glow
        var lightObj = new GameObject("ThermalGlow");
        lightObj.transform.SetParent(obj.transform, false);
        var light = lightObj.AddComponent<Light>();
        light.type = LightType.Point;
        light.color = new Color(1.0f, 0.5f, 0.15f);
        light.range = 10f;
        light.intensity = 1.2f;

        PrefabUtils.AddConstructable(obj, Info.TechType, flags, obj);

        var module = obj.GetComponent<InWallModule>();
        if (module == null)
        {
            module = obj.AddComponent<InWallModule>();
        }
        module.PartName = "In-Wall Convection Thermal Core";
        module.AddedHullStrength = 3.0f;
        module.RefundMaterials = new List<TechType> { TechType.Titanium, TechType.Magnetite, TechType.CopperWire };
    }

    public static GameObject CreateInWallPart(Vector3 position, Quaternion rotation, Transform parent)
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.name = "InWallThermalCore";
        go.transform.position = position;
        go.transform.rotation = rotation;
        go.transform.SetParent(parent, true);
        go.transform.localScale = new Vector3(0.7f, 1.0f, 0.15f);

        ConfigurePrefab(go);
        return go;
    }
}
