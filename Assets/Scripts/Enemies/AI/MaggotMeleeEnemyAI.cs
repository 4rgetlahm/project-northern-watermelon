using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaggotMeleeEnemyAI : MeleeEnemyAI
{
    public event System.Action OnEChase;
    public event System.Action OnEIdle;
    public event System.Action OnEAttack;
    public event System.Action OnEEndAttack;

    protected override void Chase()
    {
        base.Chase();
        OnEChase?.Invoke();
        OnEEndAttack?.Invoke();
    }

    protected override void Idle()
    {
        base.Idle();
        OnEIdle?.Invoke();
        OnEEndAttack?.Invoke();
    }

    protected override void Attack()
    {
        base.Attack();
        OnEAttack?.Invoke();
    }
}
