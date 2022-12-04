using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRiffleController : MonoBehaviour, IWeaponController
{

    [SerializeField]
    private LineRenderer lineRenderer;
    [SerializeField]
    private Transform projectileDirection;
    [SerializeField]
    private float fireRate = 0.5f;
    [SerializeField]
    private float range = 100f;
    [SerializeField]
    private int damage = 1;
    private float lastFireTime = 0f;

    public void Fire()
    {
        if (Time.time - lastFireTime < fireRate)
        {
            return;
        }
        lastFireTime = Time.time;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (projectileDirection.position - transform.position).normalized, range);
        if (hit.collider != null)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hit.point);
            StartCoroutine(FireAnimation());
            if (hit.collider.gameObject.tag == "Enemy")
            {
                hit.collider.gameObject.GetComponent<EnemyController>().Hit(damage);
                ApplyOnHitEffects(hit.collider.gameObject);
            }
            return;
        }
        else
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, (projectileDirection.position - transform.position).normalized * range);
            StartCoroutine(FireAnimation());
        }
    }

    IEnumerator FireAnimation()
    {
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.1f);
        lineRenderer.enabled = false;
    }

    public void ApplyOnHitEffects(GameObject target)
    {
        foreach (IWeaponBuff buff in SkillTree.GetWeaponBuffs(SkillType.AssaultRifleOnHit))
        {
            buff.ApplyOnHit(target);
        }
    }

}
