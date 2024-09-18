using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public float damage;
    [SerializeField] List<GameObject> weapon;

    public void EnableWeapon(int weaponIndex)
    {
        weaponIndex = weapon.Count - 1;
        if (weaponIndex >= 0 && weaponIndex <= weapon.Count)
        {
            for (int i = 0; i <= weaponIndex; i++)
            {
                weapon[i].SetActive(true);
            }

        }
    }
    public void DisableWeapon(int weaponIndex)
    {
        weaponIndex = weapon.Count - 1;
        if (weaponIndex >= 0 && weaponIndex <= weapon.Count)
        {
            for (int i = 0; i <= weaponIndex; i++)
            {
                weapon[i].SetActive(false);
            }

        }

    }

}