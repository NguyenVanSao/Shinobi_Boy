using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    [SerializeField] EnemyHeavyBandi enemy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            enemy.setTarget(collision.GetComponent<Transform>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            enemy.setTarget(null);
        }
    }
}
