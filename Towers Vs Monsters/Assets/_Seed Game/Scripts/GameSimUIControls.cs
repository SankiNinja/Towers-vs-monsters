using TMPro;
using UnityEngine;

public class GameSimUIControls : MonoBehaviour
{
    [SerializeField] private GridSimulation gridSimulation;

    [SerializeField] private TextMeshProUGUI dayLabel;

    public void OnDayChanged(float value)
    {
        dayLabel.SetText("DAY: {00}", value);
        gridSimulation.SimGame((int)value);
    }
}