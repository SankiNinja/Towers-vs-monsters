using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class GridSimulation : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] [Range(0, 81)] private int step = 1;
    [SerializeField] private bool simOnValidate = false;
    [SerializeField] private GridDataLoader loader;

    [SerializeField] private TileSimulationData[] seedBehaviours;

    private void OnValidate()
    {
        if (simOnValidate == false)
            return;

        SimGame();
    }

    public void SimGame()
    {
        loader.LoadData();
        SimulateWorld(step);
    }

    public void SimulateWorld(int steps)
    {
        using (ListPool<TileType>.Get(out var gameTiles))
        using (ListPool<Stack<int>>.Get(out var seedStackList))
        using (ListPool<TileType>.Get(out var seeds))
        {
            foreach (var seedData in seedBehaviours)
                seeds.Add(seedData.tileType);

            var gameTileMap = grid.TileMap;
            for (var i = 0; i < gameTileMap.Length; i++)
            {
                var tileType = gameTileMap[i].TileType;


                gameTiles.Add(tileType);

                var isSeed = false;

                foreach (var seed in seeds)
                {
                    if (tileType != seed)
                        continue;

                    isSeed = true;
                    break;
                }

                if (isSeed == false)
                    continue;

                var stack = new Stack<int>(32);
                stack.Push(i);
                seedStackList.Add(stack);
            }

            SimulateTileMap(seedStackList, gameTiles, steps);

            for (int i = 0; i < gameTileMap.Length; i++)
                gameTileMap[i].SetTileType(gameTiles[i]);

            for (int i = 0; i < seedStackList.Count; i++)
                seedStackList[i] = null;
        }
    }

    private void SimulateTileMap(List<Stack<int>> seedStackList, List<TileType> tiles, int steps)
    {
        for (int i = 0; i < steps; i++)
        {
            foreach (var seedStack in seedStackList)
            {
                StepSeed(seedStack, tiles);
            }
        }
    }

    private void StepSeed(Stack<int> stack, List<TileType> tiles)
    {
        if (stack.Count == 0)
            return;

        var gridSize = grid.GridSize;
        var index = stack.Peek();
        var pos = IndexToGird(stack.Peek(), gridSize);
        var tile = tiles[index];

        TileSimulationData currentSeedBehaviour = null;
        foreach (var data in seedBehaviours)
        {
            if (tile == data.tileType)
            {
                currentSeedBehaviour = data;
                break;
            }
        }

        if (currentSeedBehaviour == null)
            return;

        var dirData = currentSeedBehaviour.dirDelta;

        foreach (var dir in dirData)
        {
            var nextPos = GetDir(pos, dir.x, dir.y);
            var nextTile = GetNeighbour(nextPos, gridSize, tiles);
            if (IsNextSeed(tile, nextTile, nextPos, gridSize, stack, tiles))
                return;
        }

        if (stack.Count == 0)
            return;

        stack.Pop();
        StepSeed(stack, tiles);
    }

    private bool IsNextSeed(TileType current, TileType tile, Vector2Int pos, int gridSize, Stack<int> stack,
        List<TileType> tiles)
    {
        if (tile != TileType.Grass)
            return false;

        var index = GridToIndex(pos, gridSize);
        tiles[index] = current;
        stack.Push(index);
        return true;
    }

    private TileType GetNeighbour(Vector2Int pos, int gridSize, List<TileType> tiles)
    {
        if (IsValidIndex(pos.x, gridSize) == false || IsValidIndex(pos.y, gridSize) == false)
            return TileType.Tree1;

        return tiles[GridToIndex(pos, gridSize)];
    }

    private static Vector2Int GetDir(Vector2Int value, int xDelta, int yDelta) =>
        new(value.x + xDelta, value.y + yDelta);

    private bool IsValidIndex(int index, int size)
    {
        return index > 0 && index < size;
    }

    private Vector2Int IndexToGird(int index, int size)
    {
        return index == 0 ? Vector2Int.zero : new Vector2Int(index % size, index / size);
    }

    private int GridToIndex(Vector2Int pos, int size)
    {
        return pos.y * size + pos.x;
    }
}