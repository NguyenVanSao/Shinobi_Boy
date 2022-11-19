using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    public HealthBar healthBar;
    [SerializeField] float _maxHealth; 
    [SerializeField] float _currentHealth;
    [SerializeField] Transform _attackPoint;
    [SerializeField] float _attackRange;
    [SerializeField] float _attackSpeed;
    [SerializeField] LayerMask _enemyLayers;
     float timeCountDown;
    bool _isDeath => _currentHealth <= 0;
    public bool isDeath => _isDeath;

    private void Start()
    {
        OnInit();
    }

    void OnInit()
    {
        healthBar.SetMaxHealth(_maxHealth);
        _currentHealth = _maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(_isDeath)
        {
            return;
        }

        timeCountDown -= Time.deltaTime;

        if (timeCountDown > 0)
            return;

        if(Input.GetMouseButtonDown(0))
        {
            timeCountDown = _attackSpeed;
            // animation
            this.GetComponent<PlayerController>().AttackStateChange();

            Attack();
        }
    }

    void Attack()
    {
        SoundManager.instant.PlaySound(Constant.Attack);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayers);

        foreach (Collider2D e in hitEnemies)
        {
            e.GetComponent<EnemyHeavyBandi>().OnHit(30);
        }
    }

    public void GetHit(float damage)
    {
        if (!_isDeath)
        {
            SoundManager.instant.PlaySound(Constant.GetHit);
            _currentHealth -= damage;
            healthBar.SetHealth(_currentHealth);

            if(_isDeath)
            {   
                this.gameObject.tag = "DeadBoy";
            }
        }

    }

    void OnDrawGizmosSelected()
    {
        if(_attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }

    
}
