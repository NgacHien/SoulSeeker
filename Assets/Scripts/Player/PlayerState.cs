using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{


    protected Rigidbody2D rb;

    protected PlayerStateMachine stateMachine;
    protected Player player;

    protected float stateTimer;
    protected bool triggerCalled;



    protected float xInput;
    protected float yInput;
    //animator
    private string animBoolName;
    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = animBoolName;
    }


    //state function
    public virtual  void Enter()
    {
        player.anim.SetBool(animBoolName, true);
        rb = player.rb;

        triggerCalled = false;
    }


    //Update
    public virtual void Update() 
    {
        stateTimer -= Time.deltaTime;

        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        player.anim.SetFloat("yVelocity", rb.velocity.y);
    }

    public virtual void Exit() 
    {
        player.anim.SetBool(animBoolName, false);
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
