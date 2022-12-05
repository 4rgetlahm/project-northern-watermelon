using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponBuff
{
    void ApplyOnHit(GameObject target);
    void ApplyOnProjectile(ProjectileController projectileController);
    void ApplyOnPistol(PistolController pistolController);
}
