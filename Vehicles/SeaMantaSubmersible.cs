using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using UnityEngine;

namespace CallOfTheAbyss.Vehicles;

public static class SeaMantaSubmersible
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType(
        "SeaMantaSubmersible",
        "SeaManta Heavy Submersible",
        "Heavy exploration submersible engineered between the Seamoth and Cyclops. Features an armored 500 HP hull, 500m base operational depth, +45% high-velocity thrusters, and dual port/starboard hydraulic mining drill and kinetic grappling arms."
    ).WithIcon(SpriteManager.Get(TechType.Seamoth));

    public static void Register()
    {
        CustomPrefab prefab = new CustomPrefab(Info);

        CloneTemplate clone = new CloneTemplate(Info, TechType.Seamoth)
        {
            ModifyPrefab = obj =>
            {
                // 1. Scale up hull by 1.35x for imposing heavy submersible stature
                obj.transform.localScale = Vector3.one * 1.35f;

                // 2. Bolster hull armor to 500 HP (2.5x standard Seamoth)
                var liveMixin = obj.GetComponent<LiveMixin>();
                if (liveMixin != null && liveMixin.data != null)
                {
                    liveMixin.data.maxHealth = 500f;
                    liveMixin.health = 500f;
                }

                // 3. Supercharged propulsion thrusters (+45% forward speed)
                var seamoth = obj.GetComponent<SeaMoth>();
                if (seamoth != null)
                {
                    seamoth.forwardForce = 18f;
                    seamoth.backwardForce = 10f;
                    seamoth.sidewardForce = 10f;
                    seamoth.verticalForce = 10f;
                }

                // 4. Enhanced base crush depth (500 meters)
                var crush = obj.GetComponent<CrushDamage>();
                if (crush != null)
                {
                    crush.crushDepth = 500f;
                }

                // 5. Stylize hull: Deep abyssal titanium composite with electric cyan trim
                var renderers = obj.GetComponentsInChildren<Renderer>();
                foreach (var renderer in renderers)
                {
                    foreach (var mat in renderer.materials)
                    {
                        mat.color = new Color(0.12f, 0.22f, 0.35f);
                        if (mat.HasProperty("_SpecColor"))
                            mat.SetColor("_SpecColor", new Color(0.1f, 0.85f, 1.0f));
                    }
                }

                // 6. Attach dual hydraulic manipulator arm controller
                if (obj.GetComponent<SeaMantaArmController>() == null)
                {
                    obj.AddComponent<SeaMantaArmController>();
                }
            }
        };

        prefab.SetGameObject(clone);

        // Recipe: Positioned between Seamoth and Cyclops
        prefab.SetRecipe(new RecipeData(
            new Ingredient(TechType.PlasteelIngot, 1),
            new Ingredient(TechType.EnameledGlass, 2),
            new Ingredient(TechType.AdvancedWiringKit, 1),
            new Ingredient(TechType.PowerCell, 2),
            new Ingredient(TechType.Lead, 2),
            new Ingredient(TechType.Lubricant, 1)
        ))
        .WithFabricatorType(CraftTree.Type.Constructor)
        .WithStepsToFabricatorTab("Vehicles")
        .WithCraftingTime(14.0f);

        prefab.SetPdaGroupCategory(TechGroup.Constructor, TechCategory.Constructor);

        prefab.Register();
    }
}
