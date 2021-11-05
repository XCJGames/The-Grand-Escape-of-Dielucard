using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] 
    private int maxHealth = 100;
    [SerializeField] 
    private int currentHealth;

    [Header("SFX")]
    [SerializeField] 
    private AudioClip deathSFX;
    [SerializeField] 
    private AudioClip hurtSFX;

    public int CurrentHealth { get => currentHealth; }

    void Start()
    {
        if (GetComponent<Player>())
        {
            currentHealth = GameManager.Instance.GetHealthAtStartOfLevel();
            maxHealth -= Mathf.RoundToInt(PlayerPrefsManager.GetDifficulty());
        }
        else
        {
            currentHealth = maxHealth;
        }
    }
    public void RemoveHealth(int amount)
    {
        currentHealth -= amount;
        Animator animator = GetComponent<Animator>();
        if(animator != null)
        {
            animator.SetTrigger("Hurt");
        }

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        else
        {
            if (hurtSFX)
            {
                AudioSource.PlayClipAtPoint(
                    hurtSFX,
                    Camera.main.transform.position,
                    PlayerPrefsManager.GetMasterVolume());
            }
        }
    }
    public void RecoverHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
    }
    private void Die()
    {
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetBool("Dead", true);
            DisableThisCharacter();
        }
        if (deathSFX)
        {
            AudioSource.PlayClipAtPoint(
                deathSFX,
                Camera.main.transform.position,
                PlayerPrefsManager.GetMasterVolume());
        }
    }
    private void DisableThisCharacter()
    {
        switch (gameObject.layer)
        {
            case 9:
                GetComponent<Player>().enabled = false;
                GameManager.Instance.IncreaseDeathCounter();
                StartCoroutine(RestartLevel());
                break;
            case 11:
                GetComponent<Enemy>().enabled = false;
                GameManager.Instance.IncreaseEnemyCounter();
                break;
        }
        enabled = false;
    }
    private IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(2f);
        GameManager.Instance.RestartLevel();
    }

}
