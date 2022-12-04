using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemOnDeath : MonoBehaviour
{
    /*
    [SerializeField]
    private GameObject dropItemPrefab;
    [SerializeField]
    private float dropRate = 1f;*/

    [SerializeField]
    public List<GameObject> dropItemPrefab = new List<GameObject>();

    [SerializeField]
    public List<float> chance = new List<float>();

    [SerializeField]
    public List<int> maxQuantity = new List<int>();

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
        int size = dropItemPrefab.Capacity;

        for(int i=0; i<size; i++)
        {
            //calculate if dropped
            if (Random.Range(0f, 1f) > 1 - chance[i])
            {
                for (int j = 0; j < Random.Range(1, maxQuantity[i]); j++)
                {
                    GameObject dropItem = GameObject.Instantiate(dropItemPrefab[i]);
                    dropItem.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(.0f, 5.0f), Random.Range(.0f, 5.0f));
                    dropItem.transform.SetPositionAndRotation(transform.position, new Quaternion(0, 0, 0, 0));
                }
            }
        }
    }
}
