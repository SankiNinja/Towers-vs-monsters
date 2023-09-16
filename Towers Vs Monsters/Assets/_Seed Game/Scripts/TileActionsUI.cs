using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TileActionsUI : MonoBehaviour
{
    public static TileActionsUI Instance;

    public Camera cam;

    public GameObject visual;

    public Button actionA;
    public Button actionB;
    public Button actionC;
    public Button actionD;

    public TextMeshProUGUI actionAText;
    public TextMeshProUGUI actionBText;
    public TextMeshProUGUI actionCText;
    public TextMeshProUGUI actionDText;

    private Tile _tile;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        actionA.onClick.AddListener(OnActionButtonClickedA);
        actionB.onClick.AddListener(OnActionButtonClickedB);
        actionC.onClick.AddListener(OnActionButtonClickedC);
        actionD.onClick.AddListener(OnActionButtonClickedD);
        
        actionAText.SetText("Hill");
        actionBText.SetText("Crystal");
        actionCText.SetText("Tree 3");
        actionDText.SetText("Rock");

        visual.SetActive(false);
    }

    public void ShowActionContext(Tile tile)
    {
        var screenPoint = cam.WorldToScreenPoint(tile.transform.position);
        transform.position = screenPoint;

        _tile = tile;
        visual.gameObject.SetActive(true);
    }

    private void OnActionButtonClickedA()
    {
        _tile.SetTileType(TileType.Hill);
        visual.gameObject.SetActive(false);
    }

    private void OnActionButtonClickedB()
    {
        _tile.SetTileType(TileType.Crystal);
        visual.gameObject.SetActive(false);
    }

    private void OnActionButtonClickedC()
    {
        _tile.SetTileType(TileType.Tree3);
        visual.gameObject.SetActive(false);
    }

    private void OnActionButtonClickedD()
    {
        _tile.SetTileType(TileType.Rock);
        visual.gameObject.SetActive(false);
    }
}