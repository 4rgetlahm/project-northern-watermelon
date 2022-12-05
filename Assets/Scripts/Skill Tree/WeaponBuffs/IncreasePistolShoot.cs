using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreasePistolShoot : IWeaponBuff
{
    public void ApplyOnHit(GameObject target)
    {
        return;
    }

    public void ApplyOnProjectile(ProjectileController projectileController)
    {
        return;
        //projectileController.ChangeSpeed(projectileController.GetSpeed() + 5f);
    }

    public void ApplyOnPistol(PistolController pistolController)
    {
        //pistolController.ChangeFireRate(pistolController.GetFireRate() - 0.2f);
        return;
    }
}
