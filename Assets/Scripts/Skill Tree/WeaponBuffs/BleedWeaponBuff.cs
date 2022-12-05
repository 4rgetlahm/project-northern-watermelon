using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BleedWeaponBuff : IWeaponBuff
{
    public void ApplyOnHit(GameObject target)
    {
        Debug.Log("Apply on hit! (Bleed)");
        EnemyController enemyController = target.GetComponent<EnemyController>();
        enemyController.StartCoroutine(Bleed(enemyController));
    }

    private IEnumerator Bleed(EnemyController targetEnemyController)
    {
        yield return new WaitForSeconds(1f);
        targetEnemyController.Hit(1);
        yield return new WaitForSeconds(1f);
        targetEnemyController.Hit(1);
        yield return new WaitForSeconds(1f);
        targetEnemyController.Hit(1);
    }

    public void ApplyOnProjectile(ProjectileController projectileController)
    {
        return;
    }

    public void ApplyOnPistol(PistolController pistolController)
    {
        return;
    }
}
