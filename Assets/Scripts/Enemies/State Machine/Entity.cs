using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class of all enemies 
public class Entity : MonoBehaviour
{
    protected FiniteStateMachine stateMachine;
    public D_Entity entityData;

    public Animator anim { get; private set; }
    public AnimationToStatemachine atsm { get; private set; }
    public int lastDamageDirection { get; private set; }

    public Core Core { get; private set; }

    [SerializeField]
    private Transform wallCheck, ledgeCheck, playerCheck, groundCheck;

    private Vector2 velocityWorkspace;
    private float currentHealth;
    private float currentStunResistance;
    private float lastDamageTime;
    protected bool isStunned;
    protected bool isDead;

    public virtual void Awake()
    {
        Core = GetComponentInChildren<Core>();

        anim = GetComponent<Animator>();
        atsm = GetComponent<AnimationToStatemachine>();

        stateMachine = new FiniteStateMachine();
        currentHealth = entityData.maxHealth;
        currentStunResistance = entityData.stunResistance;
    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();

        anim.SetFloat("yVelocity", Core.Movement.RB.velocity.y);

        if(Time.time >= lastDamageTime + entityData.stunRecoveryTime)
        {
            ResetStunResistance();
        }
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, transform.right, entityData.wallCheckDistance, entityData.whatIsGround);
    }

    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);

    }

    public virtual bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.minAgroDistance, entityData.whatIsPlayer);
    }

    public virtual bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.maxAgroDistance, entityData.whatIsPlayer);
    }

    public virtual bool CheckPlayerInCloseRange()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.closeRangeActionDistance, entityData.whatIsPlayer);
    }

    public virtual bool CheckGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, entityData.groundCheckRadius, entityData.whatIsGround);
    }

    public virtual void DamageHop(float velocity)
    {
        velocityWorkspace.Set(Core.Movement.RB.velocity.x, velocity);
		Core.Movement.RB.velocity = velocityWorkspace;
    }

    public virtual void ResetStunResistance()
    {
        isStunned = false;
        currentStunResistance = entityData.stunResistance;
    }

    public virtual void Damage(AttackDetails attackDetails)
    {
        currentHealth -= attackDetails.damageAmount;
        DamageHop(entityData.damageHopSpeed);
        Instantiate(entityData.hitParticle, transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
        lastDamageTime = Time.time;
        currentStunResistance -= attackDetails.stunDamageAmount;

        if(attackDetails.position.x > transform.position.x)
        {
            lastDamageDirection = -1;
        } else
        {
            lastDamageDirection = 1;
        }

        if (currentStunResistance <= 0)
        {
            isStunned = true;
        }

        if (currentHealth <= 0)
        {
            isDead = true;
        }
    }

    public virtual void OnDrawGizmos()
    {
        if (Core)
        {
			Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * Core.Movement.FacingDirection * entityData.wallCheckDistance));
			Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));

			Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.closeRangeActionDistance), 0.2f);
			Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.minAgroDistance), 0.2f);
			Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.maxAgroDistance), 0.2f);
			Gizmos.DrawWireSphere(groundCheck.position, entityData.groundCheckRadius);
		}
        
    }
}
