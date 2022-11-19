using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeavyBandi : Character
{

    [SerializeField] Transform attackPoint;
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] IState currentState;
    [SerializeField] float dammage;
    [SerializeField] float attackRange;
    [SerializeField] float moveSpeed;
    [SerializeField] bool isRight;
    [SerializeField] LayerMask playerLayers;


    private Transform _target;
    public Transform target => this._target;

    // Update is called once per frame
    void Update()
    {
        if(currentState!=null)
        {
            currentState.OnExecute(this);
        }
    }

    public override void OnInit()
    {
        base.OnInit();
        this.ChangeState(new IdleState());
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
    }

    protected override void OnDeath()
    {
        base.OnDeath();
    }



    public void ChangeState(IState newState)
    {
        if(currentState != null)
        {
            currentState.OnExit(this);
        }


        currentState = newState;

        if(currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    internal void setTarget(Transform transform)
    {
        this._target = transform;

        if(IsTargetInRange())
        {
            ChangeState(new AttackState());
        }
        else
        {
            if(_target!=null)
            {
                ChangeState(new PatrolState());
            }
            else
            {
                ChangeState(new IdleState());
            }
        }
    }

    public void Moving()
    {  
        ChangeAnim("Run");
        if(transform.rotation.y == 0)
        {
            _rb.velocity =  new Vector2(-moveSpeed  * Time.deltaTime, _rb.velocity.y);
        }
        else
        {
            _rb.velocity = new Vector2(moveSpeed * Time.deltaTime, _rb.velocity.y);
        }
    }

    public void ChangeEnemyAttackAnim()
    {
        ChangeAnim("Attack");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "wall")
        {
            ChangeDir(!isRight);
        }
    }
    public void ChangeDir(bool isRight)
    {
        this.isRight = isRight;
        this.transform.rotation = isRight ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
        
    }

    public void StopMoving()
    {
        ChangeAnim("Idle");
        _rb.velocity = new Vector2(0, _rb.velocity.y);
    }

    public bool IsTargetInRange()
    {
        if(_target != null && Vector2.Distance(_target.position, transform.position) <= attackRange)
            return true;
        return false;
    }

    public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);
        foreach (Collider2D e in hitEnemies)
        {
            e.GetComponent<PlayerController>().ChangeAnimGetHit();
            e.GetComponent<PlayerCombatController>().GetHit(dammage);

        }
    }

    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }


}
