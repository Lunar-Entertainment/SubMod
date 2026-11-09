using System.Collections.Generic;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using Nautilus.Utility;
using UnityEngine;

namespace CallOfTheAbyss.Buildables.InWall;

public static class InWallPowerConduit
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType(
        "InWallPowerConduit",
        "In-Wall Micro-Capacitor Conduit",
        "Recessed internal power relay embedded directly into bulkheads. Augments the seabase electrical storage matrix with +100 Energy capacity."
    ).WithIcon(SpriteManager.Get(TechType.PowerTransmitter));

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
            new Ingredient(TechType.Gold, 1),
            new Ingredient(TechType.CopperWire, 1)
        ))
        .WithFabricatorType(CraftTree.Type.Constructor)
        .WithCraftingTime(3.0f);

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
                mat.color = new Color(0.1f, 0.5f, 0.95f);
            }
        }

        var lightObj = new GameObject("PowerConduitLight");
        lightObj.transform.SetParent(obj.transform, false);
        var light = lightObj.AddComponent<Light>();
        light.type = LightType.Point;
        light.color = new Color(0.2f, 0.6f, 1.0f);
        light.range = 8f;
        light.intensity = 1.0f;

        PrefabUtils.AddConstructable(obj, Info.TechType, flags, obj);

        var module = obj.GetComponent<InWallModule>();
        if (module == null)
        {
            module = obj.AddComponent<InWallModule>();
        }
        module.PartName = "In-Wall Micro-Capacitor Conduit";
        module.AddedPowerCapacity = 100f;
        module.RefundMaterials = new List<TechType> { TechType.Titanium, TechType.Gold, TechType.CopperWire };
    }

    public static GameObject CreateInWallPart(Vector3 position, Quaternion rotation, Transform parent)
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.name = "InWallPowerConduit";
        go.transform.position = position;
        go.transform.rotation = rotation;
        go.transform.SetParent(parent, true);
        go.transform.localScale = new Vector3(0.6f, 0.8f, 0.12f);

        ConfigurePrefab(go);
        return go;
    }
}
