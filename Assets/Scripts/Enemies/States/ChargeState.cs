using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeState : State
{
    protected D_ChargeState stateData;
    protected bool isPlayerInMinArgoRange;
    protected bool isDetectingLedge;
    protected bool isDetectingWall;
    protected bool isChargeTimeOver;
    protected bool performCloseRangeAction;

    public ChargeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChargeState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinArgoRange = entity.CheckPlayerInMinAgroRange();
        isDetectingLedge = core.CollisionSenses.LedgeVertical;
        isDetectingWall = core.CollisionSenses.WallFront;

        performCloseRangeAction = entity.CheckPlayerInCloseRange();
    }

    public override void Enter()
    {
        base.Enter();
        isChargeTimeOver = false;
        core.Movement.SetVelocityX(stateData.chargeSpeed * core.Movement.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + stateData.chargeTime)
        {
            isChargeTimeOver = true; 
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
