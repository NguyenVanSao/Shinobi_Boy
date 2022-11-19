using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    [SerializeField] float timer;
    [SerializeField] float randomTime;
    public void OnEnter(EnemyHeavyBandi enemyBandi)
    {
        enemyBandi.StopMoving();
        timer = 0;
        randomTime = Random.Range(2f, 3f);
    }

    public void OnExecute(EnemyHeavyBandi enemyBandi)
    {
        timer += Time.deltaTime;

        if(timer > randomTime)
        {
            enemyBandi.ChangeState(new PatrolState());
        }

    }

    public void OnExit(EnemyHeavyBandi enemyBandi)
    {
    }
}

