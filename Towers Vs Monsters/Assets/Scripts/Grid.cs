using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public static Grid Instance;

    [SerializeField] private List<Tile> grid;

    public List<Tile> Path;

    public List<Tile> Tiles => grid;

    public List<Vector3> pathCurve;

    [SerializeField] private Tile tilePrefab;

    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private Vector2 spacing;

    [SerializeField] private Tile spawnTile;
    [SerializeField] private Tile exitTile;

    private void Awake()
    {
        Instance = this;
    }

    //[Button]
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

    //[Button]
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


    [SerializeField] private bool debugPath;

    private void OnDrawGizmosSelected()
    {
        if (debugPath == false)
            return;

        if (Path == null || Path.Count < 2)
            return;


        Handles.ArrowHandleCap(0, Path[0].transform.position, Quaternion.LookRotation(Vector3.up), 1,
            EventType.Repaint);

        for (int i = 1; i < Path.Count; i++)
        {
            var current = Path[i];
            var last = Path[i - 1];
            var dir = current.transform.position - last.transform.position;
            dir = dir.normalized;
            Handles.ArrowHandleCap(0, last.transform.position, Quaternion.LookRotation(dir), .8f,
                EventType.Repaint);
            Handles.Label(last.transform.position, i.ToString());
        }

        Handles.ArrowHandleCap(0, Path[Path.Count - 1].transform.position + Vector3.up,
            Quaternion.LookRotation(Vector3.down),
            1, EventType.Repaint);
    }
}