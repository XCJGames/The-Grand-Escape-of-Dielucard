using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] int currentHealth;

    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip hurtSFX;

    void Start()
    {
        if (GetComponent<Player>())
        {
            GameSession[] gameSessions = FindObjectsOfType<GameSession>();
            currentHealth = gameSessions[0].GetHealthAtStartOfLevel();
            for(int i = 1; i < FindObjectsOfType<GameSession>().Length; i++)
            {
                if(currentHealth > gameSessions[i].GetHealthAtStartOfLevel())
                {
                    currentHealth = gameSessions[i].GetHealthAtStartOfLevel();
                }
            }
            maxHealth -= Mathf.RoundToInt(PlayerPrefsController.GetDifficulty());
        }
        else
        {
            currentHealth = maxHealth;
        }
    }

    public int GetHealth()
    {
        return currentHealth;
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
                    PlayerPrefsController.GetMasterVolume());
            }
        }
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
                PlayerPrefsController.GetMasterVolume());
        }
    }

    private void DisableThisCharacter()
    {
        switch (gameObject.layer)
        {
            case 9:
                GetComponent<Player>().enabled = false;
                StartCoroutine(RestartLevel());
                break;
            case 11:
                GetComponent<Enemy>().enabled = false;
                break;
        }
        enabled = false;
    }

    private IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(2f);
        FindObjectOfType<GameSession>().RestartLevel();
    }

    public void RecoverHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
    }
}
