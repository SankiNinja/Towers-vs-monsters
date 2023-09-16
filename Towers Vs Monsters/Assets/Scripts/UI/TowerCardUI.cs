using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerCardUI : MonoBehaviour
{
    public TowerData TowerData;

    public Image towerSprite;

    public TextMeshProUGUI range;
    public TextMeshProUGUI damage;
    public TextMeshProUGUI rateOfFire;
    public TextMeshProUGUI cost;

    public void OnValidate()
    {
        if (TowerData == null)
            return;

        towerSprite.sprite = TowerData.TowerSprite;

        var stats = TowerData.TowerStats;
        range.SetText(stats.Range.ToString());
        damage.SetText(stats.Damage.ToString());
        rateOfFire.SetText(stats.AttacksPerSecond.ToString()+"/Sec");
        cost.SetText(stats.Cost.ToString());
    }
}