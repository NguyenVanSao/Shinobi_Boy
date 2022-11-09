using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    [SerializeField] float timer;
    [SerializeField] float randomTime;
    public void OnEnter(EnemyHeavyBandi enemyBandi)
    {
        timer = 0;
        randomTime = Random.Range(4f, 6f);
    }

    public void OnExecute(EnemyHeavyBandi enemyBandi)
    {
        timer += Time.deltaTime;
        if(enemyBandi.target != null)
        {
            enemyBandi.ChangeDir(enemyBandi.transform.position.x - enemyBandi.target.position.x > 0);

            if (enemyBandi.IsTargetInRange())
            {
                enemyBandi.ChangeState(new AttackState());
            }
            else
            {
                enemyBandi.Moving();
            }

        }
        else
        {
            if (timer < randomTime)
            {
                enemyBandi.Moving();
            }
            else
            {
                enemyBandi.ChangeState(new IdleState());
            }
        }

    }

    public void OnExit(EnemyHeavyBandi enemyBandi)
    {
    }
}

