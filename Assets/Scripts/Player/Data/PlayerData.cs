using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 10f;

    [Header("Jump State")]
    public float jumpVelocity = 15f;
    public int amountOfJumps = 1;

    [Header("Touching Wall State")]
    public float wallSlideVelocity = 2f;

    [Header("Wall Climb State")]
    public float wallClimbVelocity = 3f;

    [Header("Wall Jump State")]
    public float wallJumpVelocity = 20f;
    public float wallJumpTime = 0.4f;
    public Vector2 wallJumpAngle = new Vector2(1, 2);

    [Header("Ledge Climb State")]
    public Vector2 startOffset;
    public Vector2 stopOffset;

    [Header("In Air State")]
    public float coyoteTime = 0.2f;
    public float jumpHeightMultiplier = 0.5f;

    [Header("Dash State")]
    public float dashCoolDown = 0.5f;
    // max time hold before determing a direction to dash
    public float maxHoldTime = 1f;
    // used for slow motion, time becomes a quarter of regular time passing
    public float holdTimeScale = 0.25f;
    public float dashTime = 0.2f;
    public float dashSpeed = 30f;
    // a subtle property that is Rigidbody2D's property determing how air density affects velocity
    public float drag = 10f;
    // decrease y velocity after dashing so that the player drops slower
    public float dashEndYMultiplier = 0.2f;
    // distance between two different after images
    public float distanceBetweenAfterImages = 0.5f;

    [Header("Check Variables")]
    public float groundCheckRadius = 0.3f;
    public float wallCheckDistance = 0.5f;
    public LayerMask whatIsGround;
    
}
