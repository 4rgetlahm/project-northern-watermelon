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
    private List<IWeaponBuff> buffs = new List<IWeaponBuff>();

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
        Vector3 direction = projectileSpawnPoint.position - transform.position;
        Debug.Log(direction);
        projectileController.SetDirection(direction.normalized);
        projectileController.buffs = buffs;
        projectileController.ApplyProjectileBuffs();
    }

    public void AddBuff(IWeaponBuff buff)
    {
        buffs.Add(buff);
    }

}
