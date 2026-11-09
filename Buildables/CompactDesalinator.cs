using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using Nautilus.Utility;
using UnityEngine;

namespace CallOfTheAbyss.Buildables;

public class CompactDesalinatorController : MonoBehaviour, IHandTarget
{
    private float _progress = 0f;
    private int _storedWater = 0;
    private const int MaxWater = 2;
    private const float TimeToDistill = 80f;

    private Constructable _constructable;

    private void Start()
    {
        _constructable = GetComponent<Constructable>();
    }

    private void Update()
    {
        if (_constructable != null && !_constructable.constructed)
            return;

        if (_storedWater < MaxWater)
        {
            _progress += Time.deltaTime;
            if (_progress >= TimeToDistill)
            {
                _progress = 0f;
                _storedWater++;
            }
        }
    }

    public void OnHandHover(GUIHand hand)
    {
        if (_constructable != null && !_constructable.constructed)
            return;

        if (_storedWater > 0)
        {
            HandReticle.main.SetText(HandReticle.TextType.Hand, $"Collect Filtered Water ({_storedWater}/{MaxWater})", true, GameInput.Button.LeftHand);
        }
        else
        {
            float pct = Mathf.Clamp01(_progress / TimeToDistill) * 100f;
            HandReticle.main.SetText(HandReticle.TextType.Hand, $"Solar Desalinator: Distilling ({pct:F0}%)", false);
        }
    }

    public void OnHandClick(GUIHand hand)
    {
        if (_constructable != null && !_constructable.constructed)
            return;

        if (_storedWater > 0)
        {
            CraftData.AddToInventory(TechType.FilteredWater, 1, false, false);
            _storedWater--;
            ErrorMessage.AddMessage("Collected Filtered Water from Desalinator");
        }
    }
}

public static class CompactDesalinator
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType(
        "CompactDesalinator",
        "Compact Solar Desalinator",
        "Wall-mounted solar-thermal evaporation condenser. Slowly distills ocean water into potable Filtered Water without consuming high base electrical power. Stores up to 2 bottles."
    ).WithIcon(SpriteManager.Get(TechType.FiltrationMachine));

    public static void Register()
    {
        CustomPrefab prefab = new CustomPrefab(Info);

        CloneTemplate clone = new CloneTemplate(Info, TechType.SmallLocker);
        clone.ModifyPrefab += obj =>
        {
            ConstructableFlags flags = ConstructableFlags.Inside | ConstructableFlags.Wall | ConstructableFlags.Base | ConstructableFlags.Submarine;

            // Remove generic locker storage if present
            var storage = obj.GetComponent<StorageContainer>();
            if (storage != null)
            {
                Object.DestroyImmediate(storage);
            }

            // Fresh aquatic blue hue
            var renderers = obj.GetComponentsInChildren<Renderer>();
            foreach (var r in renderers)
            {
                foreach (var mat in r.materials)
                {
                    mat.color = new Color(0.2f, 0.65f, 0.9f);
                }
            }

            PrefabUtils.AddConstructable(obj, Info.TechType, flags, obj);

            if (obj.GetComponent<CompactDesalinatorController>() == null)
            {
                obj.AddComponent<CompactDesalinatorController>();
            }
        };

        prefab.SetGameObject(clone);

        prefab.SetPdaGroupCategory(TechGroup.InteriorModules, TechCategory.InteriorModule);

        prefab.SetRecipe(new RecipeData(
            new Ingredient(TechType.Titanium, 2),
            new Ingredient(TechType.Glass, 1),
            new Ingredient(TechType.CopperWire, 1)
        ))
        .WithFabricatorType(CraftTree.Type.Constructor)
        .WithCraftingTime(3.0f);

        prefab.Register();
    }
}
