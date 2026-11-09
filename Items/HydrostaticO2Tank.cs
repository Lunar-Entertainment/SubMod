using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using UnityEngine;

namespace CallOfTheAbyss.Items;

public static class HydrostaticO2Tank
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType(
        "HydrostaticO2Tank",
        "Hydrostatic Compression Tank",
        "Reinforced mid-tier oxygen cylinder with dual-stage hydrostatic valves. Compresses 135 units of oxygen with minimal hydrodynamic drag, bridging the gap to ultra-capacity tanks."
    ).WithIcon(SpriteManager.Get(TechType.DoubleTank));

    public static void Register()
    {
        CustomPrefab prefab = new CustomPrefab(Info);

        CloneTemplate clone = new CloneTemplate(Info, TechType.DoubleTank)
        {
            ModifyPrefab = obj =>
            {
                var ox = obj.GetComponent<Oxygen>();
                if (ox != null)
                {
                    ox.oxygenCapacity = 135f;
                }

                var renderers = obj.GetComponentsInChildren<Renderer>();
                foreach (var renderer in renderers)
                {
                    foreach (var mat in renderer.materials)
                    {
                        mat.color = new Color(0.2f, 0.5f, 0.7f);
                    }
                }
            }
        };

        prefab.SetGameObject(clone);

        prefab.SetEquipment(EquipmentType.Tank);

        prefab.SetRecipe(new RecipeData(
            new Ingredient(TechType.DoubleTank, 1),
            new Ingredient(TechType.Lithium, 2),
            new Ingredient(TechType.Silver, 1)
        ))
        .WithFabricatorType(CraftTree.Type.Workbench)
        .WithCraftingTime(4.0f);

        prefab.SetPdaGroupCategory(TechGroup.Personal, TechCategory.Equipment);

        prefab.Register();
    }
}
