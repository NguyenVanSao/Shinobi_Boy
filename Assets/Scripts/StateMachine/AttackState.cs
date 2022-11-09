using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    [SerializeField] float timer;
    public void OnEnter(EnemyHeavyBandi enemyBandi)
    {
        if(enemyBandi.target != null)
        {
            enemyBandi.ChangeDir(enemyBandi.transform.position.x - enemyBandi.target.position.x > 0);
            enemyBandi.StopMoving();
            enemyBandi.Attack();
            Debug.LogError("AttackState");
        }

        timer = 0;
    }

    public void OnExecute(EnemyHeavyBandi enemyBandi)
    {
        timer += Time.deltaTime;
        if(timer > 1.5f)
        {
            enemyBandi.ChangeState(new PatrolState());
        }
    }

    public void OnExit(EnemyHeavyBandi enemyBandi)
    {
    }
}
