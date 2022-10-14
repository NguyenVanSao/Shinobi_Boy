using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using playerState = PlayerController.playerState;

public class UnityAnimation : BaseAnimation
{
    [SerializeField] Animator _animControl;
    public override void ChangeAnim()
    {
        for (int i = 1; i <= (int)playerState.Attack_Crouched; i++)
        {
            playerState tmp = (playerState)i;
            if (tmp != player.currentSTATE)
                _animControl.SetBool(tmp.ToString(), false);
            else
                _animControl.SetBool(tmp.ToString(), true);

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _animControl = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeAnim();
    }
}
