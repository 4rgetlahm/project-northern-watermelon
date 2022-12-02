using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private MultipleSpriteHandler spriteHandler;
    private EnemyHealth healthController;
    AudioSource maggotsound;

    void Start()
    {
        healthController = GetComponent<EnemyHealth>();
        maggotsound = GetComponent<AudioSource>();
    }

    public void Hit(int damage)
    {
        maggotsound.Play();
        StartCoroutine(HitAnimation());
        healthController.Damage(damage);
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

    IEnumerator HealAnimation()
    {
        if (spriteHandler != null)
        {
            spriteHandler.ChangeColor(Color.green);
            yield return new WaitForSeconds(0.1f);
            spriteHandler.ChangeColor(Color.white);
        }
    }


}
