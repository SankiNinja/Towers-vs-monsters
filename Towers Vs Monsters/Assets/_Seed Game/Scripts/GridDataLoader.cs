using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class GridDataLoader : MonoBehaviour
{
    [SerializeField] private Grid grid;

    [SerializeField] private GridData gridData;

    public void SetGridData(GridData data)
    {
        gridData = data;
    }

    [Button]
    public void SaveGridData()
    {
        if (gridData == null)
            return;

        var tilemap = grid.TileMap;

        gridData.tiles = new TileType[tilemap.Length];
        for (int i = 0; i < tilemap.Length; i++)
            gridData.tiles[i] = tilemap[i].TileType;
    }

    [Button]
    public void LoadData()
    {
        if (gridData == null)
            return;

        var tilemap = grid.TileMap;

        if (gridData.tiles.Length != tilemap.Length)
            return;

        for (int i = 0; i < tilemap.Length; i++)
            tilemap[i].SetTileType(gridData.tiles[i]);
    }
}