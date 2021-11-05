using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI healthText;
    [SerializeField] 
    private Health playerHealth;

    void Start()
    {
        healthText = GetComponent<TextMeshProUGUI>();
        if (playerHealth == null)
        {
            playerHealth = FindObjectOfType<Player>().GetComponent<Health>();
        }
        UpdateHealthDisplay();
    }

    private void Update() => UpdateHealthDisplay();
    public void UpdateHealthDisplay() 
        => healthText.text = playerHealth.CurrentHealth.ToString();
}
