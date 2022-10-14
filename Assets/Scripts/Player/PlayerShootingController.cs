using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingController : MonoBehaviour
{
    public IWeaponController equippedWeaponController;

    public void Shoot()
    {
        if (equippedWeaponController == null)
        {
            equippedWeaponController = FindActiveWeaponController();
        }
        Debug.Log("Shoot");
        equippedWeaponController.Fire();
    }

    public void SetActiveWeapon(IWeaponController weaponController)
    {
        equippedWeaponController = weaponController;
    }

    public IWeaponController FindActiveWeaponController()
    {
        GameObject[] weapons = GameObject.FindGameObjectsWithTag("Weapon");
        foreach (GameObject weapon in weapons)
        {
            if (weapon.activeSelf)
            {
                return weapon.GetComponent<IWeaponController>(); ;
            }
        }
        return null;
    }
}
