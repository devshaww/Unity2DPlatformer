using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// inherite from MonoBehaviour if you wanna attach this script to game object 
public class AnimationToStatemachine : MonoBehaviour
{
    public AttackState attackState;

    private void TriggerAttack()
    {
        attackState.TriggerAttack();
    }

    private void FinishAttack()
    {
        attackState.FinishAttack();
    }
}
