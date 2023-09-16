using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileActions : MonoBehaviour
{
    [SerializeField] private Tile tile;
    
    public void ShowActionContext()
    {
        TileActionsUI.Instance.ShowActionContext(tile);
    }
}
