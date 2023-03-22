using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState CurrentState { get; private set; }

    public virtual void ChangeState(PlayerState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        newState.Enter();
    }

    public void Initialize(PlayerState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }
}
