using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] Health playerHealth;

    void Start()
    {
        healthText = GetComponent<TextMeshProUGUI>();
        playerHealth = FindObjectOfType<Player>().GetComponent<Health>();
        UpdateHealthDisplay();
    }

    private void Update()
    {
        UpdateHealthDisplay();
    }
    public void UpdateHealthDisplay()
    {
        healthText.text = playerHealth.GetHealth().ToString();
    }
}
