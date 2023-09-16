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

public enum TowerType
{
    None,
    Selection,
    Small,
    Tall,
}

[SelectionBase]
public class Tile : MonoBehaviour
{
    [SerializeField] private GameObject[] tiles;

    [SerializeField] private Tower[] towers;

    [SerializeField] private TileType tileType;

    [SerializeField] private TowerType towerType;

    public TileType TileType => tileType;

    public TowerType TowerType => towerType;

    public void SetTileType(TileType type)
    {
        tileType = type;
        UpdateTileVisual();
    }

    public void SetTowerType(TowerType type)
    {
        towerType = type;
        UpdateTileVisual();
    }

    public void OnValidate()
    {
        UpdateTileVisual();
        //UpdateName();
    }

    private void UpdateTileVisual()
    {
        if (tiles != null)
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i].SetActive(i == (int)tileType);
            }
        }

        if (towers != null)
        {
            for (int i = 0; i < towers.Length; i++)
            {
                towers[i].gameObject.SetActive(i == (int)towerType && tileType == TileType.Grass);
            }
        }
    }

    private void UpdateName()
    {
        var siblingIndex = transform.GetSiblingIndex();
        var gridIndex = IndexToGrid(siblingIndex, 12);
        gameObject.name = nameof(Tile) + ": " + siblingIndex.ToString("00") + " || X " + gridIndex.x.ToString("00") +
                          "  || Y " +
                          gridIndex.y.ToString("00");
    }

    private static Vector2Int IndexToGrid(int index, int xSize)
    {
        return new Vector2Int(index / xSize, index % xSize);
    }

    public Tower GetTower()
    {
        return towers[(int)towerType];
    }
}