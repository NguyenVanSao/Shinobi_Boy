using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public void OnEnter(EnemyHeavyBandi enemyBandi);
    public void OnExecute(EnemyHeavyBandi enemyBandi);
    public void OnExit(EnemyHeavyBandi enemyBandi);
}
