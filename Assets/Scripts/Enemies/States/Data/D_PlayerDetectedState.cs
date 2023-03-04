using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerDetectedData", menuName = "Data/State Data/Player Detected State")]
public class D_PlayerDetectedState : ScriptableObject
{
    // time to wait before charging at player 
    public float longRangeActionTime = 1.5f;
}
