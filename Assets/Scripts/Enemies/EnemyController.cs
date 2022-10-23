using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyHealth healthController;

    void Start()
    {
        healthController = GetComponent<EnemyHealth>();
    }

    public void Hit(int damage)
    {
        healthController.Damage(damage);
        StartCoroutine(HitAnimation());
    }

    IEnumerator HitAnimation()
    {
        Color originalColor = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = originalColor;
    }


}
