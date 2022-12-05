using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceWeaponBuff : IWeaponBuff
{
    public void ApplyOnHit(GameObject target)
    {
        Debug.Log("Apply on hit! (Ice)");
        EnemyController enemyController = target.GetComponent<EnemyController>();
        enemyController.StartCoroutine(Ice(enemyController));
    }

    private IEnumerator Ice(EnemyController targetEnemyController)
    {
        targetEnemyController.SlowDown(Time.time);
        yield return new WaitForSeconds(2f);
        targetEnemyController.GetSpeed(Time.time);
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
