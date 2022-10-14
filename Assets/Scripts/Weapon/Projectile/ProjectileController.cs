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
        transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
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

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //collision.gameObject.GetComponent<HealthController>().Damage(damage);
            ApplyOnHitBuffs(collision.gameObject);
        }
        Destroy(this.gameObject);
    }
}
