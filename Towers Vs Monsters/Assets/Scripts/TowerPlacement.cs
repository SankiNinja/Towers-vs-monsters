using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    public static TowerPlacement Instance;

    [SerializeField] private Tile tile;

    private TowerData towerData;

    public Camera cam;

    private bool showPlacement;

    private void Start()
    {
        Instance = this;
    }

    public void ShowSelectionMode(TowerData data)
    {
        towerData = data;
        tile.SetTowerType(data.TowerType);
        var tower = tile.GetTower();
        tower.Weapons.SetWeaponType(data.WeaponType);

        var tiles = Grid.Instance.TileMap;

        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].TileType != TileType.Grass) continue;

            if (tiles[i].TowerType == TowerType.None)
            {
                tiles[i].SetTowerType(TowerType.Selection);
                tile.transform.position = tiles[i].transform.position;
                tile.gameObject.SetActive(true);
            }
        }

        showPlacement = tile.gameObject.activeSelf;
    }

    public void HideSelectionMode()
    {
        var tiles = Grid.Instance.TileMap;

        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].TileType != TileType.Grass) continue;

            if (tiles[i].TowerType == TowerType.Selection)
                tiles[i].SetTowerType(TowerType.None);
        }

        showPlacement = false;
        tile.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (showPlacement == false)
            return;

        var ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hitInfo, 100))
        {
            var hoverTile = hitInfo.collider.GetComponent<Tile>();
            if (hoverTile == null)
                return;

            if (hoverTile.TileType != TileType.Grass)
                return;

            tile.transform.position = hoverTile.transform.position;

            if (Input.GetMouseButtonDown(0))
            {
                hoverTile.SetTowerType(towerData.TowerType);
                hoverTile.GetTower().Weapons.SetWeaponType(towerData.WeaponType);
                HideSelectionMode();
            }
        }
    }
}