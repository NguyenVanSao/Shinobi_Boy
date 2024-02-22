using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] Animator anim;
    private float currentHealth;
    private float maxHealth = 100;
    public bool isDead => currentHealth <= 0;
    private string currentAnimName;
    public HealthBar healthBar;

    public void Start()
    {
        OnInit();

    }

    public virtual void OnInit()
    {
        currentHealth = maxHealth;
    }

    public virtual void OnDespawn()
    {

    }

    protected virtual void OnDeath()
    {
        SoundManager.instant.PlaySound(Constant.Enemy_Dead);
        ChangeAnim("Death");
        Destroy(this.gameObject, 1.5f);
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
            currentHealth -= damage;

            if(isDead)
            {
                OnDeath();
                GameManager.instant.ScoreIncrease();
            }
        }

    }
}
