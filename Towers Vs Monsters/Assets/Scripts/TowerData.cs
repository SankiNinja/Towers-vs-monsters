using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public struct TowerStats
{
    [Range(0, 10)] public float Range;
    [Range(0, 100)] public float Damage;
    [Range(0.25f, 10)] public float AttacksPerSecond;
    [Range(1, 1000)] public float Cost;
}

public class TowerData : ScriptableObject
{
    [FormerlySerializedAs("TowerImage")] public Sprite TowerSprite;
    public TowerType TowerType;
    public WeaponType WeaponType;
    public TowerStats TowerStats;
}