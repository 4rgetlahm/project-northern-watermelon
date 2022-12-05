using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private MultipleSpriteHandler spriteHandler;
    private EnemyHealth healthController;
    [SerializeField]
    public PathfinderMovement script;

    private float timeStart = 0;

    private bool slowed = false;

    void Start()
    {
        healthController = GetComponent<EnemyHealth>();
    }

    public void Hit(int damage)
    {
        StartCoroutine(HitAnimation());
        healthController.Damage(damage);
    }

    public void SlowDown(float time)
    {
        timeStart = time;
        if(!slowed)
        {
            slowed = true;
            script.ChangeSpeed(script.GetSpeed() * 0.5f);
        }
        
    }
    public void GetSpeed(float time)
    {
        if(time - timeStart  >= 2f)
        {
            script.ChangeSpeed(script.GetSpeed() * 2f);
            slowed = false;
        }

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
