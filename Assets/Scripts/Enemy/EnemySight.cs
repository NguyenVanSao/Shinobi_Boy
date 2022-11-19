using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    [SerializeField] EnemyHeavyBandi enemy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == Constant.Player)
        {
            enemy.setTarget(collision.GetComponent<Transform>());
        }
        else if(collision.tag == Constant.DeadBoy)
        {
            enemy.setTarget(null);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == Constant.Player || collision.tag == Constant.DeadBoy)
        {
            enemy.setTarget(null);
        }
    }
}
