using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 3f;

    [SerializeField] Transform attackPoint;
    [SerializeField] int attackDamage = 50;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] float attackRate = 2f;
    float nextAttackTime = 0f;

    Rigidbody2D myRigidBody;
    Animator animator;
    CapsuleCollider2D capsuleCollider;
    BoxCollider2D boxCollider;

    PlayerControls controls;

    [SerializeField] AudioClip attackSFX;
    [SerializeField] AudioClip jumpSFX;

    private void Awake() => controls = new PlayerControls();
    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnEnable() => controls.Player.Enable();
    private void OnDisable()
    {
        controls.Player.Disable();
        boxCollider.enabled = false;
        capsuleCollider.enabled = false;
        myRigidBody.bodyType = RigidbodyType2D.Static;
    }

    void Update()
    {
        Move();
        Jump();
        Attack();
        CheckHazards();
    }

    private void CheckHazards()
    {
        bool isTouchingHazard = boxCollider.IsTouchingLayers(LayerMask.GetMask("Hazards"));
        if (isTouchingHazard)
        {
            GetComponent<Health>().RemoveHealth(99);
        }
    }

    private void Attack()
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
            PlayerPrefsController.GetMasterVolume());
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
            hitEnemies[0].GetComponent<Health>().RemoveHealth(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(attackPoint != null)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
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
                PlayerPrefsController.GetMasterVolume());
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
