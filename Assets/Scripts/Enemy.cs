using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Animator anim;
    private float hp;
    public bool isDead => hp <= 0;
    private string currentAnimName;

    public virtual void OnInit()
    {

    }

    public virtual void OnDespawn()
    {

    }

    protected virtual void OnDeath()
    {
        
    }

    protected void ChangeAnim(string animName)
    {
        if(currentAnimName != animName)
        {
            anim.ResetTrigger(currentAnimName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }


    public void OnHit(float damage) 
    {
        if(!isDead)
        {
            hp -= damage;
            if(isDead)
            {
                OnDeath();
            }
        }

    }
}
