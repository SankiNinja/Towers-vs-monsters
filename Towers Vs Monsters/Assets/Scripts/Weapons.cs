using UnityEngine;

public enum WeaponType
{
    Ballista,
    Catapult,
    Canon,
    Crystal
}

public class Weapons : MonoBehaviour
{
    [SerializeField] private WeaponType weaponType;

    [SerializeField] private GameObject[] weapons;

    public WeaponType WeaponType => weaponType;

    private void OnValidate()
    {
        UpdateWeaponVisuals();
    }

    public void SetWeaponType(WeaponType type)
    {
        weaponType = type;
        UpdateWeaponVisuals();
    }

    private void UpdateWeaponVisuals()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(i == (int)weaponType);
        }
    }
}