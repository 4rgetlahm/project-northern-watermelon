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
    private float fireRate;
    [SerializeField]
    private Animator anim;
    private float lastFireTime = 0f;


    public List<IWeaponBuff> buffs = new List<IWeaponBuff>();
    //audio
    public AudioSource src;
    public AudioClip shoot;

    void Start()
    {
        //audio
        src = gameObject.GetComponent<AudioSource>();
    }

    public void Fire()
    {
        if (Time.time - lastFireTime < fireRate- ApplySpeedBuff())
        {
            return;
        }
        //audio
        src.clip = shoot;
        src.Play();

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
        //projectileController.buffs.AddRange(SkillTree.GetWeaponBuffs(SkillType.PistolPassive));

        buffs = SkillTree.GetWeaponBuffs(SkillType.PistolPassive);

        Vector3 direction = projectileSpawnPoint.position - transform.position;
        projectileController.SetDirection(direction.normalized);
        projectileController.ApplyProjectileBuffs();
    }

    public void ChangeFireRate(float newfireRate)
    {
        fireRate = newfireRate;
    }

    public float GetFireRate()
    {
        return fireRate;
    }

    public float ApplySpeedBuff()
    {
        float speedup = 0;
        foreach (IWeaponBuff buff in buffs)
        {
            speedup += 0.01f;
            
        }
        Debug.Log("speedup="+buffs.Capacity);
        return speedup;
    }
}