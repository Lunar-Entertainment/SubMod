using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using UnityEngine;

namespace CallOfTheAbyss.Items;

public class EmergencyO2CanisterTool : PlayerTool
{
    public override string animToolName => "firstaidkit";

    public override bool OnRightHandDown()
    {
        return ActivateCanister();
    }

    public override bool OnLeftHandDown()
    {
        return ActivateCanister();
    }

    private bool ActivateCanister()
    {
        if (Player.main == null || Player.main.oxygenMgr == null)
            return false;

        // Inject 25 units of emergency oxygen
        Player.main.oxygenMgr.AddOxygen(25f);
        ErrorMessage.AddMessage("Emergency O2 Injected! (+25 O2)");

        var pickupable = GetComponent<Pickupable>();
        if (pickupable != null && Inventory.main != null)
        {
            Inventory.main.container.RemoveItem(pickupable);
            Destroy(gameObject);
        }

        return true;
    }
}

public static class EmergencyO2Canister
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType(
        "EmergencyO2Canister",
        "Emergency O2 Micro-Canister",
        "Single-use pressurized micro-cylinder. Instantly injects 25 units of emergency oxygen when activated in hand. Essential for exploring deep early-game caves and winding wrecks."
    ).WithIcon(SpriteManager.Get(TechType.AirBladder));

    public static void Register()
    {
        CustomPrefab prefab = new CustomPrefab(Info);

        CloneTemplate clone = new CloneTemplate(Info, TechType.AirBladder)
        {
            ModifyPrefab = obj =>
            {
                var bladder = obj.GetComponent<AirBladder>();
                if (bladder != null)
                {
                    Object.DestroyImmediate(bladder);
                }

                if (obj.GetComponent<EmergencyO2CanisterTool>() == null)
                {
                    obj.AddComponent<EmergencyO2CanisterTool>();
                }

                // High-visibility cyan/orange canister styling
                var renderers = obj.GetComponentsInChildren<Renderer>();
                foreach (var renderer in renderers)
                {
                    foreach (var mat in renderer.materials)
                    {
                        mat.color = new Color(0.1f, 0.8f, 0.95f);
                    }
                }
            }
        };

        prefab.SetGameObject(clone);

        prefab.SetRecipe(new RecipeData(
            new Ingredient(TechType.Titanium, 1),
            new Ingredient(TechType.Glass, 1),
            new Ingredient(TechType.CreepvinePiece, 1)
        ))
        .WithFabricatorType(CraftTree.Type.Fabricator)
        .WithStepsToFabricatorTab("Personal", "Tools")
        .WithCraftingTime(2.0f);

        prefab.SetPdaGroupCategory(TechGroup.Personal, TechCategory.Tools);

        prefab.Register();
    }
}
