using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState currentState {  get; private set; } //you can only see the value but you can't change the value

    public void Initialize(PlayerState _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }


    public void ChangeState(PlayerState _newState)
    {
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }


}
