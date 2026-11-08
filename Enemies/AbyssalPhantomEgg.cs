using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using UnityEngine;

namespace CallOfTheAbyss.Enemies;

public static class AbyssalPhantomEgg
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType(
        "AbyssalPhantomEgg",
        "Abyssal Phantom Egg",
        "An armored bio-crystalline egg laid by an Abyssal Phantom. Can be incubated within an Alien Containment Unit."
    ).WithIcon(SpriteManager.Get(TechType.CrashEgg));

    public static void Register()
    {
        CustomPrefab customEgg = new CustomPrefab(Info);
        customEgg.Info.WithSizeInInventory(new Vector2int(2, 2));

        // Configure egg gadget
        EggGadget eggGadget = customEgg.CreateCreatureEgg();
        eggGadget.SetAcidImmune(true);
        eggGadget.WithRequiredAcuSize(2);

        // Template cloning CrashEgg visual
        EggTemplate egg = new EggTemplate(customEgg.Info, TechType.CrashEgg)
        {
            HatchingCreature = AbyssalPhantom.Info.TechType,
            HatchingTime = 3f
        };
        egg.SetUndiscoveredTechType();

        customEgg.SetGameObject(egg);
        customEgg.SetPdaGroupCategory(TechGroup.Resources, TechCategory.BasicMaterials);

        customEgg.Register();
    }
}
