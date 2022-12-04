using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolController : MonoBehaviour, IWeaponController
{
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private Transform projectileSpawnPoint;
    [SerializeField]
    private float fireRate = 0.5f;
    [SerializeField]
    private Animator anim;
    private float lastFireTime = 0f;

    public void Fire()
    {
        if (Time.time - lastFireTime < fireRate)
        {
            return;
        }
        //animation
        anim.Play("shoot");

        lastFireTime = Time.time;
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        if (projectileSpawnPoint.position.x < transform.position.x)
        {
            projectile.GetComponent<SpriteRenderer>().flipX = true;
        }
        ProjectileController projectileController = projectile.GetComponent<ProjectileController>();
        projectileController.buffs.AddRange(SkillTree.GetWeaponBuffs(SkillType.PistolOnHit));
        projectileController.buffs.AddRange(SkillTree.GetWeaponBuffs(SkillType.PistolPassive));
        Vector3 direction = projectileSpawnPoint.position - transform.position;
        projectileController.SetDirection(direction.normalized);
        projectileController.ApplyProjectileBuffs();
    }

}
