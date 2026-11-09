using System.Collections.Generic;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using Nautilus.Utility;
using UnityEngine;

namespace CallOfTheAbyss.Buildables.InWall;

public static class InWallBioLight
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType(
        "InWallBioLight",
        "In-Wall Bio-Luminescent Light Panel",
        "Flush architectural light inset infused with bioluminescent enzyme fluids. Casts a soothing cyan-teal ambient illumination down dark corridors."
    ).WithIcon(SpriteManager.Get(TechType.LEDLight));

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
            new Ingredient(TechType.Glass, 1),
            new Ingredient(TechType.Titanium, 1),
            new Ingredient(TechType.CreepvinePiece, 1)
        ))
        .WithFabricatorType(CraftTree.Type.Constructor)
        .WithCraftingTime(2.0f);

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
                mat.color = new Color(0.15f, 0.85f, 0.75f);
            }
        }

        var lightObj = new GameObject("BioLightPoint");
        lightObj.transform.SetParent(obj.transform, false);
        var light = lightObj.AddComponent<Light>();
        light.type = LightType.Point;
        light.color = new Color(0.1f, 0.9f, 0.75f);
        light.range = 16f;
        light.intensity = 1.4f;

        PrefabUtils.AddConstructable(obj, Info.TechType, flags, obj);

        var module = obj.GetComponent<InWallModule>();
        if (module == null)
        {
            module = obj.AddComponent<InWallModule>();
        }
        module.PartName = "In-Wall Bio-Luminescent Light Panel";
        module.RefundMaterials = new List<TechType> { TechType.Glass, TechType.Titanium, TechType.CreepvinePiece };
    }

    public static GameObject CreateInWallPart(Vector3 position, Quaternion rotation, Transform parent)
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.name = "InWallBioLight";
        go.transform.position = position;
        go.transform.rotation = rotation;
        go.transform.SetParent(parent, true);
        go.transform.localScale = new Vector3(0.6f, 0.9f, 0.12f);

        ConfigurePrefab(go);
        return go;
    }
}
