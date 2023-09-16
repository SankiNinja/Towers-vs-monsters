using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Tile Sim Data",fileName = "Tile_Sim_Data_")]
public class TileSimulationData : ScriptableObject
{
    public TileType tileType;
    public Vector2Int[] dirDelta;
}
