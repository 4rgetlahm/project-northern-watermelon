using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolController : MonoBehaviour, IWeaponController
{
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private Transform projectileSpawnPoint;

    private List<IWeaponBuff> buffs = new List<IWeaponBuff>();
    public void Fire()
    {
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        ProjectileController projectileController = projectile.GetComponent<ProjectileController>();
        Vector3 direction = projectileSpawnPoint.position - transform.position;
        projectileController.SetDirection(direction.normalized);
        projectileController.buffs = buffs;
        projectileController.ApplyProjectileBuffs();
    }

}
