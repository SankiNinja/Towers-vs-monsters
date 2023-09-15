using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class Grid : MonoBehaviour
{
    [SerializeField] private List<Tile> grid;

    [FormerlySerializedAs("grassTile")] [SerializeField]
    private Tile tilePrefab;

    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private Vector2 spacing;

    [SerializeField] private Tile spawnTile;
    [SerializeField] private Tile exitTile;

    [Button]
    public void GenerateGrid()
    {
        var zInitial = -(gridSize.y / 2.0f);
        var spawnPos = new Vector3(-(gridSize.x / 2.0f), 0, zInitial);

        var xOffset = spacing.x;
        var zOffset = spacing.y;

        for (int x = 0; x < gridSize.y; x++)
        {
            for (int z = 0; z < gridSize.x; z++)
            {
                var tile = PrefabUtility.InstantiatePrefab(tilePrefab, transform) as Tile;
                tile!.transform.position = spawnPos;
                spawnPos.z += zOffset;
            }

            spawnPos.z = zInitial;
            spawnPos.x += xOffset;
        }
    }

    [Button]
    public void PopulateGrid()
    {
        grid.Clear();
        var childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            var tile = transform.GetChild(i).GetComponent<Tile>();
            grid.Add(tile);

            if (tile.TileType == TileType.Spawn)
                spawnTile = tile;

            if (tile.TileType == TileType.Exit)
                exitTile = tile;
        }
    }

    [Button]
    public void AnimateGrid()
    {
        var tileTypes = new TileType[grid.Count];


        //Store the types.
        for (int i = 0; i < grid.Count; i++)
        {
            tileTypes[i] = grid[i].TileType;
            grid[i].SetTileType(TileType.Tree1);
        }

        StartCoroutine(AnimateGrid(grid, tileTypes));
    }

    [SerializeField] private float _showInterval = 0.1f;

    IEnumerator AnimateGrid(List<Tile> tileGrid, TileType[] targetState)
    {
        var waitTime = new WaitForSeconds(_showInterval);

        for (int i = 0; i < tileGrid.Count; i++)
        {
            if (grid[i].TileType == targetState[i])
                continue;

            grid[i].SetTileType(targetState[i]);
            yield return waitTime;
        }

        yield return null;
    }

    [Button]
    public void AnimatePath()
    {
        var tileTypes = new TileType[grid.Count];


        //Store the types.
        for (int i = 0; i < grid.Count; i++)
        {
            tileTypes[i] = grid[i].TileType;
            grid[i].SetTileType(TileType.Tree1);
        }

        StartCoroutine(AnimateGrid(grid, tileTypes));
    }

    private void GetPath(List<Tile> path)
    {
        var xLen = gridSize.x;
        var yLen = gridSize.y;
        
        
    }

    private int GridToLinearCoordinate()
    {
        
        
        return 0;
    }
}