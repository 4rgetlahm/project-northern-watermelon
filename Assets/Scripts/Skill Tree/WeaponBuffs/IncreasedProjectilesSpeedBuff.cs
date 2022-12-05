using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreasedProjectilesSpeedBuff : IWeaponBuff
{
    public void ApplyOnHit(GameObject target)
    {
        return;
    }

    public void ApplyOnProjectile(ProjectileController projectileController)
    {
        projectileController.ChangeSpeed(projectileController.GetSpeed() + 5f);
    }

    public void ApplyOnPistol(PistolController pistolController)
    {
        return;
    }
}
