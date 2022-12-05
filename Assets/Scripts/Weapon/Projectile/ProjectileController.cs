using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileController : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float lifeTime = 5f;
    [SerializeField]
    private int damage = 1;
    private Vector3 direction;

    public List<IWeaponBuff> buffs = new List<IWeaponBuff>();

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }

    public void ChangeSpeed(float speed)
    {
        this.speed = speed;
    }

    public void ChangeDamage(int damage)
    {
        this.damage = damage;
    }

    public void ChangeRange(float lifeTime)
    {
        this.lifeTime = lifeTime;
    }

    public int GetDamage()
    {
        return damage;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public float GetRange()
    {
        return lifeTime;
    }

    public void ApplyOnHitBuffs(GameObject target)
    {
        foreach (IWeaponBuff buff in buffs)
        {
            buff.ApplyOnHit(target);
        }
    }

    public void ApplyProjectileBuffs()
    {
        foreach (IWeaponBuff buff in buffs)
        {
            buff.ApplyOnProjectile(this);
        }
    }
    void Start()
    {
        ApplyProjectileBuffs();
        Destroy(gameObject, lifeTime);
    }

    void Hit(GameObject target)
    {
        if (target.tag == "Enemy")
        {
            Debug.Log("Total buffs: " + buffs.Count);
            ApplyOnHitBuffs(target);
            target.GetComponent<EnemyController>().Hit(damage);
        }
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Hit(collision.gameObject);
    }
}
