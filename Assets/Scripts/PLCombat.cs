using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLCombat : MonoBehaviour
{
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange;
    [SerializeField] LayerMask enemyLayers;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            Attack();
        }
    }

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D e in hitEnemies)
        {
            Debug.Log("i hit " + e.name);
        }
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
