using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreasedProjectilesRange : IWeaponBuff
{
    public void ApplyOnHit(GameObject target)
    {
        return;
    }

    public void ApplyOnProjectile(ProjectileController projectileController)
    {
        projectileController.ChangeRange(projectileController.GetRange() + 2f);
    }

    public void ApplyOnPistol(PistolController pistolController)
    {
        return;
    }
}
