using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemOnDeath : MonoBehaviour
{
    [SerializeField]
    private GameObject dropItemPrefab;
    [SerializeField]
    private float dropRate = 1f;

    void Start()
    {
        EnemyHealth enemyHealth = GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            GetComponent<EnemyHealth>().OnDeath += DropItem;
        }
        else
        {
            Debug.LogError("Enemy Health was not found by DropItemOnDeath");
        }
    }

    private void DropItem()
    {
        if (Random.Range(0f, 1f) > dropRate)
        {
            return;
        }
        GameObject dropItem = GameObject.Instantiate(dropItemPrefab);
        dropItem.transform.SetPositionAndRotation(transform.position, new Quaternion(0, 0 ,0 ,0));
    }
}
