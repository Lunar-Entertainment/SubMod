using System.Collections.Generic;
using UnityEngine;

namespace CallOfTheAbyss.Buildables.InWall;

public class InWallModule : MonoBehaviour
{
    public string PartName = "In-Wall Module";
    public List<TechType> RefundMaterials = new List<TechType>();
    public float AddedHullStrength = 0f;
    public float AddedPowerCapacity = 0f;

    private SubRoot _subRoot;
    private bool _buffApplied = false;

    private void Start()
    {
        FindSubRoot();
        ApplyBuffs();
    }

    private void FindSubRoot()
    {
        if (_subRoot == null)
        {
            _subRoot = GetComponentInParent<SubRoot>();
            if (_subRoot == null && Player.main != null)
            {
                _subRoot = Player.main.currentSub;
            }
        }
    }

    public void ApplyBuffs()
    {
        if (_buffApplied) return;
        FindSubRoot();

        if (_subRoot != null)
        {
            if (AddedHullStrength > 0f)
            {
                var hull = _subRoot.GetComponent<BaseHullStrength>();
                if (hull != null)
                {
                    hull.totalStrength += AddedHullStrength;
                    ErrorMessage.AddMessage($"Base Hull Reinforced (+{AddedHullStrength:F1}) -> Total: {hull.totalStrength:F1}");
                }
            }

            if (AddedPowerCapacity > 0f)
            {
                var ps = GetComponent<PowerSource>();
                if (ps == null)
                {
                    ps = gameObject.AddComponent<PowerSource>();
                }
                ps.maxPower = AddedPowerCapacity;
                ps.power = AddedPowerCapacity;
                ErrorMessage.AddMessage($"Base Power Grid Expanded (+{AddedPowerCapacity:F0} Energy)");
            }

            _buffApplied = true;
        }
    }

    private void OnDestroy()
    {
        if (_buffApplied && _subRoot != null)
        {
            if (AddedHullStrength > 0f)
            {
                var hull = _subRoot.GetComponent<BaseHullStrength>();
                if (hull != null)
                {
                    hull.totalStrength = Mathf.Max(0f, hull.totalStrength - AddedHullStrength);
                }
            }
        }
    }

    public void Demolish()
    {
        // Refund materials directly to player inventory
        if (RefundMaterials != null && RefundMaterials.Count > 0)
        {
            foreach (var mat in RefundMaterials)
            {
                CraftData.AddToInventory(mat, 1, false, false);
            }
            ErrorMessage.AddMessage($"Demolished {PartName}. Materials recovered.");
        }
        else
        {
            ErrorMessage.AddMessage($"Demolished {PartName}.");
        }

        Destroy(gameObject);
    }
}
