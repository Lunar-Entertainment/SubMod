using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using UnityEngine;

namespace CallOfTheAbyss.Vehicles;

public static class SeamothTrenchSonar
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType(
        "SeamothTrenchSonar",
        "Seamoth Trench Sonar Pulse",
        "Acoustic echolocation transceiver tuned for mid-game exploration. Pings surroundings to calculate seabed depth and detect submerged obstacles or ravines."
    ).WithIcon(SpriteManager.Get(TechType.SeamothSonarModule));

    public static void Register()
    {
        CustomPrefab prefab = new CustomPrefab(Info);

        CloneTemplate clone = new CloneTemplate(Info, TechType.SeamothSonarModule)
        {
            ModifyPrefab = obj =>
            {
                var renderers = obj.GetComponentsInChildren<Renderer>();
                foreach (var renderer in renderers)
                {
                    foreach (var mat in renderer.materials)
                    {
                        mat.color = new Color(0.15f, 0.7f, 0.65f);
                    }
                }
            }
        };

        prefab.SetGameObject(clone);

        prefab.SetRecipe(new RecipeData(
            new Ingredient(TechType.Magnetite, 2),
            new Ingredient(TechType.CopperWire, 1),
            new Ingredient(TechType.ComputerChip, 1)
        ))
        .WithFabricatorType(CraftTree.Type.Workbench)
        .WithCraftingTime(3.0f);

        prefab.SetVehicleUpgradeModule(EquipmentType.SeamothModule, QuickSlotType.Instant)
            .WithEnergyCost(5f)
            .WithCooldown(4.0f)
            .WithOnModuleUsed((Vehicle vehicle, int slotId, float charge, float chargeScalar) =>
            {
                if (vehicle == null) return;

                float currentDepth = vehicle.transform.position.y * -1f;

                // Ping downwards to detect seabed depth
                bool hitSeabed = Physics.Raycast(vehicle.transform.position, Vector3.down, out RaycastHit downHit, 600f);

                // Ping ahead to detect cavern walls or obstacles
                bool hitForward = Physics.Raycast(vehicle.transform.position, vehicle.transform.forward, out RaycastHit forwardHit, 150f);

                string seabedStr = hitSeabed ? $"{downHit.distance:F0}m below" : ">600m (Abyssal Void)";
                string obstacleStr = hitForward ? $"Obstacle ahead ({forwardHit.distance:F0}m)" : "Clear path forward";

                ErrorMessage.AddMessage($"[Trench Sonar] Depth: {currentDepth:F0}m | Seabed: {seabedStr} | {obstacleStr}");
            });

        prefab.SetPdaGroupCategory(TechGroup.VehicleUpgrades, TechCategory.VehicleUpgrades);

        prefab.Register();
    }
}
