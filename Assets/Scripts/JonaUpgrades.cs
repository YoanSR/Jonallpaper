using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "JonaUpgrades", fileName = "JonaUpgrades", order = 0)]
public class JonaUpgrades : ScriptableObject
{
    public Sprite Sprite;
    public string Name;
    public string Description;
    public ulong Price;
    public bool OneTimeUpgrade;
    [HideIf("OneTimeUpgrade")] public float PriceIncrement;
    public JonaUpgradeType Type;
    
    [ShowIf("Type", JonaUpgradeType.Active),Header("Active")] public ulong JonathanPerSecond;
    
    [ShowIf("Type", JonaUpgradeType.Passive),Header("Passive")]public int PowerIncrease = 0;
    [ShowIf("Type", JonaUpgradeType.Passive)] public bool IsOnActiveUpgrade;
    [ShowIf("Type", JonaUpgradeType.Passive)] public JonaUpgrades ActiveUpgradeIncrease;
}

public enum JonaUpgradeType
{
    Active,
    Passive
}
