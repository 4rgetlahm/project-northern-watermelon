using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private MultipleSpriteHandler spriteHandler;
    private HealthController healthController;

    void Start()
    {
        healthController = GetComponent<HealthController>();
    }

    public void Hit(int damage)
    {
        healthController.Damage(damage);
        StartCoroutine(HitAnimation());
    }

    public void Hit()
    {
        Hit(1);
    }

    IEnumerator HitAnimation()
    {
        if (spriteHandler != null)
        {
            spriteHandler.ChangeColor(Color.red);
            yield return new WaitForSeconds(0.1f);
            spriteHandler.ChangeColor(Color.white);
        }
    }
}
