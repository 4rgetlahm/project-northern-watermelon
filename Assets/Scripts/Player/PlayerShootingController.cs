using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShootingController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> weapons = new List<GameObject>();
    private GameObject equippedWeapon;
    private IWeaponController equippedWeaponController;
    private AudioSource shoot;

    private bool isShooting = false;

    void Start()
    {
        LoadWeapons();
        SetActiveWeapon(weapons[0]);
        shoot = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (isShooting)
        {
            Shoot();
        }
    }

    public void SetActiveWeapon(GameObject weapon)
    {
        if (equippedWeapon != null)
        {
            equippedWeapon.SetActive(false);
        }
        weapon.SetActive(true);
        equippedWeapon = weapon;
        equippedWeaponController = weapon.GetComponent<IWeaponController>();
    }

    public void SetActiveWeapon1()
    {
        SetActiveWeapon(weapons[0]);
    }

    public void SetActiveWeapon2()
    {
        SetActiveWeapon(weapons[1]);
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

    private List<IWeaponController> LoadWeapons()
    {
        List<IWeaponController> weapons = new List<IWeaponController>();
        foreach (GameObject weapon in GameObject.FindGameObjectsWithTag("Weapon"))
        {
            weapons.Add(weapon.GetComponent<IWeaponController>());
        }
        return weapons;
    }

    public void MouseDown(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isShooting = true;
        }
        else if (context.canceled)
        {
            isShooting = false;
        }
    }

    public void Shoot()
    {
        if (equippedWeaponController == null)
        {
            equippedWeaponController = FindActiveWeaponController();
        }
        equippedWeaponController.Fire();
        shoot.Play();
    }
}
