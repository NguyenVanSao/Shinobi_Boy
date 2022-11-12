using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLCombat : MonoBehaviour
{

    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange;
    [SerializeField] float attackSpeed;
    [SerializeField] LayerMask enemyLayers;
     float timeCountDown;


    // Update is called once per frame
    void Update()
    {
        timeCountDown -= Time.deltaTime;

        if (timeCountDown > 0)
            return;

        if(Input.GetMouseButtonDown(0))
        {
            // animation
            this.GetComponent<PlayerController>().Attack();

            _Attack();
            timeCountDown = attackSpeed;
        }
    }

    void _Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D e in hitEnemies)
        {
            e.GetComponent<EnemyHeavyBandi>().OnHit(33);
        }
    }

    void GetHit(float damage)
    {
        
    }

    void OnDrawGizmosSelected()
    {
        
        if(attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
