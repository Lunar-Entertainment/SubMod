using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using UnityEngine;

namespace CallOfTheAbyss.Enemies;

public static class AbyssalPhantomTooth
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType(
        "AbyssalPhantomTooth",
        "Abyssal Phantom Tooth",
        "Serrated tooth shed by an Abyssal Phantom. Possesses extreme hardness capable of slicing dense hull plating."
    ).WithIcon(SpriteManager.Get(TechType.StalkerTooth));

    public static void Register()
    {
        CustomPrefab prefab = new CustomPrefab(Info);

        CloneTemplate clone = new CloneTemplate(Info, TechType.StalkerTooth)
        {
            ModifyPrefab = obj =>
            {
                var renderers = obj.GetComponentsInChildren<Renderer>();
                foreach (var r in renderers)
                {
                    foreach (var mat in r.materials)
                    {
                        mat.color = new Color(0.15f, 0.4f, 0.55f);
                    }
                }
            }
        };

        prefab.SetGameObject(clone);
        prefab.SetPdaGroupCategory(TechGroup.Resources, TechCategory.BasicMaterials);

        prefab.Register();
    }
}
