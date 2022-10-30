using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTakenArgs
{
    public int damageTaken;

    public DamageTakenArgs(int damageTaken)
    {
        this.damageTaken = damageTaken;
    }
}

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int health = 3;

    public event OnDamageTaken OnDamage;
    public event System.Action OnDeath;

    public delegate void OnDamageTaken(object sender, DamageTakenArgs e);


    public void Damage(int damage)
    {
        Debug.Log("enemy health: " + health);
        health -= damage;
        if (OnDamage != null)
        {
            OnDamage(this, new DamageTakenArgs(damage));
        }
        if (health <= 0)
        {
            Death();
        }
    }

    public void Heal(int health)
    {
        health += health;
    }

    void Death()
    {
        if (OnDeath != null)
        {
            OnDeath();
        }
        Destroy(gameObject);
    }

}