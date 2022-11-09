using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeavyBandi : Enemy
{
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] IState currentState;
    [SerializeField] float attackRange;
    [SerializeField] float moveSpeed;
    [SerializeField] bool isRight;


    private Transform _target;
    public Transform target => this._target;

    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

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
            if(target!=null)
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
        _rb.velocity = -transform.right * moveSpeed  * Time.deltaTime;
    }

    public void Attack()
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
        _rb.velocity = Vector2.zero;
    }

    public bool IsTargetInRange()
    {
        if(target != null && Vector2.Distance(target.position, transform.position) <= attackRange)
            return true;
        return false;
    }


}
