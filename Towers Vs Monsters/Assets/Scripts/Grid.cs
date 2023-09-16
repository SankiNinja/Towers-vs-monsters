using System.Collections.Generic;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public static Grid Instance;

    [SerializeField] private Tile[] tilemap;

    public List<Tile> Path;

    public Tile[] TileMap => tilemap;

    public int GridSize => gridSize;

    public List<Vector3> pathCurve;

    [SerializeField] private Tile tilePrefab;

    [SerializeField] private int gridSize;
    [SerializeField] private Vector2 spacing;

    private void Awake()
    {
        Instance = this;
    }

    [Button]
    public void GenerateGrid()
    {
        tilemap = new Tile[GridSize * GridSize];

        var xInitial = -(GridSize / 2.0f) + (spacing.x / 2);
        var spawnPos = new Vector3(xInitial, 0, -(GridSize / 2.0f) + (spacing.y / 2));

        var xOffset = spacing.x;
        var zOffset = spacing.y;

        for (int x = 0; x < GridSize; x++)
        {
            for (int z = 0; z < GridSize; z++)
            {
                var tile = PrefabUtility.InstantiatePrefab(tilePrefab, transform) as Tile;
                tile!.transform.localPosition = spawnPos;
                spawnPos.x += xOffset;
                tilemap[x * GridSize + z] = tile;
            }

            spawnPos.x = xInitial;
            spawnPos.z += zOffset;
        }
    }

    [Button]
    public void Clear()
    {
        for (int i = 0; i < tilemap.Length; i++)
        {
            DestroyImmediate(tilemap[i].gameObject);
        }
    }
}