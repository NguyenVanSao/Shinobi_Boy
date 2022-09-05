using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAnimation : MonoBehaviour
{
    [SerializeField] protected PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void ChangeAnim();
}
