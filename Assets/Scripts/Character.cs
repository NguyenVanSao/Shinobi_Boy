using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] Animator anim;
    private float hp;
    public bool isDead => hp <= 0;
    private string currentAnimName;

    public void Start()
    {
        OnInit();
    }

    public virtual void OnInit()
    {
        hp = 100;
    }

    public virtual void OnDespawn()
    {

    }

    protected virtual void OnDeath()
    {
        ChangeAnim("Death");
        Destroy(this.gameObject, 3);
    }

    protected void ChangeAnim(string animName)
    {
        if(currentAnimName == "Death")
        {
            return;
        }
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
            ChangeAnim("Hurt");
            hp -= damage;
            if(isDead)
            {
                OnDeath();
            }
        }

    }
}
