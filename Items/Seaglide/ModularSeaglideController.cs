using System.Collections.Generic;
using UnityEngine;

namespace CallOfTheAbyss.Items.Seaglide;

public class ModularSeaglideController : MonoBehaviour
{
    public TechType CurrentModule { get; set; } = TechType.None;

    private global::Seaglide _seaglide;
    private EnergyMixin _energyMixin;
    private float _nextO2Timer;
    private float _nextTelemetryTimer;
    private float _lastEnergyValue;

    private static readonly List<TechType> CompatibleModules = new List<TechType>();

    private void Start()
    {
        _seaglide = GetComponent<global::Seaglide>();
        _energyMixin = GetComponent<EnergyMixin>();

        if (CompatibleModules.Count == 0)
        {
            CompatibleModules.Add(SeaglideSpeedModule.Info.TechType);
            CompatibleModules.Add(SeaglideOxygenModule.Info.TechType);
            CompatibleModules.Add(SeaglideBatteryModule.Info.TechType);
            CompatibleModules.Add(SeaglideSonarModule.Info.TechType);
            CompatibleModules.Add(EmptySeaglideModule.Info.TechType);
        }

        if (CurrentModule == TechType.None)
        {
            CurrentModule = EmptySeaglideModule.Info.TechType;
        }

        if (_energyMixin != null)
        {
            _lastEnergyValue = _energyMixin.charge;
        }
    }

    private void Update()
    {
        if (Inventory.main == null) return;

        bool isHeld = Inventory.main.GetHeldTool() == (PlayerTool)_seaglide;
        if (!isHeld) return;

        // Check for module swapping (F key or AltTool button)
        if (GameInput.GetButtonDown(GameInput.Button.AltTool) || Input.GetKeyDown(KeyCode.F))
        {
            TrySwapModuleFromInventory();
        }

        // Display module status on reticle
        string moduleName = GetModuleName(CurrentModule);
        string telemetryExtra = "";

        // 1. Oxygen Module Active Effect
        if (CurrentModule == SeaglideOxygenModule.Info.TechType)
        {
            if (Player.main != null && Player.main.IsUnderwater())
            {
                _nextO2Timer += Time.deltaTime;
                if (_nextO2Timer >= 2.5f)
                {
                    _nextO2Timer = 0f;
                    if (Player.main.oxygenMgr != null)
                    {
                        Player.main.oxygenMgr.AddOxygen(1f);
                    }
                }
            }
        }

        // 2. Battery Efficiency Module Active Effect
        if (CurrentModule == SeaglideBatteryModule.Info.TechType && _energyMixin != null)
        {
            float currentCharge = _energyMixin.charge;
            if (currentCharge < _lastEnergyValue)
            {
                float consumed = _lastEnergyValue - currentCharge;
                // Refund 65% of the consumed energy back to the battery
                _energyMixin.ModifyCharge(consumed * 0.65f);
            }
            _lastEnergyValue = _energyMixin.charge;
        }
        else if (_energyMixin != null)
        {
            _lastEnergyValue = _energyMixin.charge;
        }

        // 3. Sonar Telemetry Module Active Effect
        if (CurrentModule == SeaglideSonarModule.Info.TechType && Player.main != null)
        {
            _nextTelemetryTimer += Time.deltaTime;
            bool hitSeabed = Physics.Raycast(Player.main.transform.position, Vector3.down, out RaycastHit downHit, 250f);
            string seabedStr = hitSeabed ? $"{downHit.distance:F0}m" : ">250m";
            telemetryExtra = $" | Seabed: {seabedStr}";
        }

        HandReticle.main.SetText(
            HandReticle.TextType.Hand,
            $"Modular Seaglide [{moduleName}]{telemetryExtra}\n[F / Alt] Swap Module from Inventory",
            false
        );
    }

    private void TrySwapModuleFromInventory()
    {
        if (Inventory.main == null || Inventory.main.container == null) return;

        // Find first compatible module in inventory that isn't the currently installed one
        TechType foundModule = TechType.None;
        foreach (var mod in CompatibleModules)
        {
            if (mod != CurrentModule && Inventory.main.container.Contains(mod))
            {
                foundModule = mod;
                break;
            }
        }

        if (foundModule != TechType.None)
        {
            // Remove the new module from inventory
            Inventory.main.container.DestroyItem(foundModule);

            // Return the old module to inventory if it's not None
            if (CurrentModule != TechType.None)
            {
                CraftData.AddToInventory(CurrentModule, 1, false, false);
            }

            CurrentModule = foundModule;
            ErrorMessage.AddMessage($"[Modular Seaglide] Sockets updated: {GetModuleName(CurrentModule)}");
        }
        else
        {
            ErrorMessage.AddMessage("[Modular Seaglide] No alternate Seaglide modules in inventory!");
        }
    }

    public static string GetModuleName(TechType mod)
    {
        if (mod == SeaglideSpeedModule.Info.TechType) return "Speed Overdrive (+35%)";
        if (mod == SeaglideOxygenModule.Info.TechType) return "Rebreather & O2 Injection";
        if (mod == SeaglideBatteryModule.Info.TechType) return "65% Power Efficiency";
        if (mod == SeaglideSonarModule.Info.TechType) return "Acoustic Telemetry";
        if (mod == EmptySeaglideModule.Info.TechType) return "Empty Module Chassis";
        return "None";
    }
}
