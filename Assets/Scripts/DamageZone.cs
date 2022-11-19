using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField] bool isActive;
    [SerializeField] float activeTime;
    [SerializeField] float timer;
    [SerializeField] float dam;

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            isActive = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("trap");
        if(!isActive)
            return;

        if(collision.tag == "Player")
        {
            isActive = false;
            timer = activeTime;
            collision.GetComponent<PlayerCombatController>().GetHit(dam);
        }
    }
}
