using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileType
{
    Rocket,
    Bullet,
    Laser
}

public class ProjectileController : MonoBehaviour
{
    [SerializeField]
    private ProjectileType projectileType;
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
        switch (projectileType)
        {
            case ProjectileType.Laser:
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);
                if (hit.collider != null)
                {
                    ApplyOnHitBuffs(hit.collider.gameObject);
                }
                Destroy(gameObject);
                break;
            default:
                break;
        }
        ApplyProjectileBuffs();
        Destroy(gameObject, lifeTime);
    }

    void Hit(GameObject target)
    {
        if (target.tag == "Enemy")
        {
            ApplyOnHitBuffs(target);
        }
        Destroy(gameObject);
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Hit(collision.gameObject);
    }
}
