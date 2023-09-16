using System;
using UnityEngine;



[CreateAssetMenu(menuName = "Custom/Level Data",fileName = "Level_Data_")]
public class LevelData : ScriptableObject
{
    [Serializable]
    public struct SeedData
    {
        public TileType TileType;
        public int Count;
    }

    public int days;

    public SeedData[] seeds;
}
