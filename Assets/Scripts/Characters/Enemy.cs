using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] 
    private float moveSpeed = 1f;

    [Header("Attack")]
    [SerializeField] 
    private Transform attackPoint;
    [SerializeField] 
    private int attackDamage = 1;
    [SerializeField] 
    private float attackRange = 0.5f;
    [SerializeField] 
    private float attackRate = 0.75f;
    [SerializeField] 
    private AudioClip attackSFX;
    [SerializeField] 
    private BoxCollider2D boxCollider;

    private Rigidbody2D myRigidBody;
    private Animator animator;
    private float nextAttackTime = 0f;

    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        Move();
        CheckForPlayer();
    }
    public void PlayAttackSFX()
    {
        AudioSource.PlayClipAtPoint(
            attackSFX,
            Camera.main.transform.position,
            PlayerPrefsManager.GetMasterVolume());
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
    public void SetMovementSpeed(float speed) => moveSpeed = speed;
    private void CheckForPlayer()
    {
        bool isTouchingPlayer = boxCollider.IsTouchingLayers(LayerMask.GetMask("Player"));
        if (isTouchingPlayer)
        {
            SetMovementSpeed(0f);
            //myRigidBody.velocity = new Vector2(0f, 0f);
            myRigidBody.velocity = Vector2.zero;
            animator.SetBool("Walking", false);
            if (Time.time >= nextAttackTime)
            {
                nextAttackTime = Time.time + 1f / attackRate;
                animator.SetTrigger("Attack");
                PlayAttackSFX();
            }
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
    private bool IsFacingRight() => transform.localScale.x > 0;
    private void OnTriggerExit2D(Collider2D other) 
        => transform.localScale = new Vector2(-Mathf.Sign(myRigidBody.velocity.x), 1f);

}
