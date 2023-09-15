using System.Collections;
using NaughtyAttributes;
using UnityEngine;

public enum TileType
{
    Grass,
    Path,
    Spawn,
    Exit,
    Tree1,
    Tree2,
    Tree3,
    Rock,
    Crystal,
    Hill,
}

[SelectionBase]
public class Tile : MonoBehaviour
{
    [SerializeField] private GameObject[] tiles;

    [SerializeField] private TileType tileType;

    public TileType TileType => tileType;

    public void SetTileType(TileType type)
    {
        tileType = type;
        UpdateTileVisual();
    }

    public void OnValidate()
    {
        UpdateTileVisual();
    }

    private void UpdateTileVisual()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i].SetActive(i == (int)tileType);
        }
    }
}