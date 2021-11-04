using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigidBody;
    Animator animator;

    [SerializeField] Transform attackPoint;
    [SerializeField] int attackDamage = 1;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] float attackRate = 0.75f;
    float nextAttackTime = 0f;

    [SerializeField] AudioClip attackSFX;

    [SerializeField] BoxCollider2D boxCollider;
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        CheckForPlayer();
    }

    private void CheckForPlayer()
    {
        bool isTouchingPlayer = boxCollider.IsTouchingLayers(LayerMask.GetMask("Player"));
        if (isTouchingPlayer)
        {
            SetMovementSpeed(0f);
            myRigidBody.velocity = new Vector2(0f, 0f);
            animator.SetBool("Walking", false);
            if (Time.time >= nextAttackTime)
            {
                nextAttackTime = Time.time + 1f / attackRate;
                animator.SetTrigger("Attack");
                PlayAttackSFX();
            }
        }
    }

    public void PlayAttackSFX()
    {
        AudioSource.PlayClipAtPoint(
            attackSFX,
            Camera.main.transform.position,
            PlayerPrefsController.GetMasterVolume());
    }

    public void StrikePlayer()
    {
        // Detect player in range of attack
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRange,
            LayerMask.GetMask("Player"));
        // If player hitted, deal damage
        if(hitPlayer.Length > 0)
        {
            hitPlayer[0].GetComponent<Health>().RemoveHealth(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }

    private void OnDisable()
    {
        SetMovementSpeed(0f);
        myRigidBody.velocity = new Vector2(0f, 0f);
        boxCollider.enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
    }

    private void Move()
    {
        bool isTouchingPlayer = boxCollider.IsTouchingLayers(LayerMask.GetMask("Player"));
        if (!isTouchingPlayer)
        {
            animator.SetBool("Walking", true);
            SetMovementSpeed(1f);
            if (IsFacingRight())
            {
                myRigidBody.velocity = new Vector2(moveSpeed, 0f);
            }
            else
            {
                myRigidBody.velocity = new Vector2(-moveSpeed, 0f);
            }
        }
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        transform.localScale = new Vector2(-Mathf.Sign(myRigidBody.velocity.x), 1f);
    }

    public void SetMovementSpeed(float speed)
    {
        moveSpeed = speed;
    }
}
