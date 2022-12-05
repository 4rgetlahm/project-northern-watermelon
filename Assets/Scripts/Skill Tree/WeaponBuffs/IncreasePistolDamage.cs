using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreasePistolDamage : IWeaponBuff
{
    public void ApplyOnHit(GameObject target)
    {
        return;
    }

    public void ApplyOnProjectile(ProjectileController projectileController)
    {
        projectileController.ChangeDamage(projectileController.GetDamage() + 1);
    }

    public void ApplyOnPistol(PistolController pistolController)
    {
        //pistolController.ChangeFireRate(pistolController.GetFireRate() - 0.2f);
        return;
    }
}
