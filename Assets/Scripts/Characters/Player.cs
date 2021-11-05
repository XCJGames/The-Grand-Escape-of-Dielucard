using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] 
    private float runSpeed = 5f;
    [SerializeField] 
    private float jumpSpeed = 3f;

    [Header("Attack")]
    [SerializeField] 
    private Transform attackPoint;
    [SerializeField] 
    private float attackDamage = 50;
    [SerializeField] 
    private float attackRange = 0.5f;
    [SerializeField] 
    private float attackRate = 2f;
    private float nextAttackTime = 0f;

    private Rigidbody2D myRigidBody;
    private Animator animator;
    private CapsuleCollider2D capsuleCollider;
    private BoxCollider2D boxCollider;

    private PlayerControls controls;

    [Header("SFX")]
    [SerializeField] 
    AudioClip attackSFX;
    [SerializeField] 
    AudioClip jumpSFX;

    private void Awake() => controls = new PlayerControls();
    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        float difficulty = PlayerPrefsManager.GetDifficulty();
        if(difficulty != 0)
        {
            difficulty *= -2;
            difficulty /= 10;
            difficulty += 1;
            attackDamage *= difficulty;
        }
    }
    private void OnEnable() => controls.Player.Enable();
    private void OnDisable()
    {
        controls.Player.Disable();
        boxCollider.enabled = false;
        capsuleCollider.enabled = false;
        myRigidBody.bodyType = RigidbodyType2D.Static;
    }
    private void Update()
    {
        Move();
        Jump();
        Attack();
        CheckHazards();
    }
    public void IdleAnimation() => animator.SetBool("Running", false);
    public void Attack()
    {
        if (controls.Player.Attack.triggered && Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + 1f / attackRate;
            // Attack animation
            animator.SetTrigger("Attack");
            PlayAttackSFX();
        }
    }
    public void PlayAttackSFX()
    {
        AudioSource.PlayClipAtPoint(
            attackSFX,
            Camera.main.transform.position,
            PlayerPrefsManager.GetMasterVolume());
    }
    public void StrikeEnemies()
    {
        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRange,
            LayerMask.GetMask("Enemy"));
        // Damage first enemy
        if (hitEnemies.Length > 0)
        {
            hitEnemies[0].GetComponent<Health>().RemoveHealth(Mathf.RoundToInt(attackDamage));
        }
    }
    private void OnDrawGizmosSelected()
    {
        if(attackPoint != null)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
    private void CheckHazards()
    {
        bool isTouchingHazard = boxCollider.IsTouchingLayers(LayerMask.GetMask("Hazards"));
        if (isTouchingHazard)
        {
            GetComponent<Health>().RemoveHealth(99);
        }
    }
    private void Jump()
    {
        bool isTouchingGround = boxCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
        if (controls.Player.Jump.triggered && isTouchingGround)
        {
            Vector2 playerVelocity = new Vector2(
                0f, jumpSpeed);
            myRigidBody.velocity += playerVelocity;

            AudioSource.PlayClipAtPoint(
                jumpSFX,
                Camera.main.transform.position,
                PlayerPrefsManager.GetMasterVolume());
        }
        ChangeJumpingFallingAnimation(isTouchingGround);
    }
    private void ChangeJumpingFallingAnimation(bool isTouchingGround)
    {
        if (!isTouchingGround)
        {
            if(myRigidBody.velocity.y > 0)
            {
                animator.SetBool("Jumping", true);
            }
            else if(myRigidBody.velocity.y < 0)
            {
                animator.SetBool("Jumping", false);
                animator.SetBool("Falling", true);
            }
        }
        else
        {
            animator.SetBool("Jumping", false);
            animator.SetBool("Falling", false);
        }
    }
    private void Move()
    {
        var movementInput = controls.Player.Move.ReadValue<float>();
        if (movementInput != 0)
        {
            FlipSprite(movementInput);
            Vector2 playerVelocity = new Vector2(
                movementInput * runSpeed, myRigidBody.velocity.y);
            myRigidBody.velocity = playerVelocity;
        }
        ChangeRunningAnimation(movementInput);
    }
    private void ChangeRunningAnimation(float movementInput)
    {
        if (!animator.GetBool("Jumping") && !animator.GetBool("Falling"))
        {
            animator.SetBool("Running", movementInput != 0 ? true : false);
        }
    }
    private void FlipSprite(float movementInput)
    {
        if (movementInput < 0) transform.localRotation = Quaternion.Euler(0, 180, 0);
        else transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}
