using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 100, currentHealth;
    [SerializeField] Image displayImage;
    [SerializeField] Gradient gradientHealth;
    private bool canHeal = true;
    private PlayerMovement playerMovement;
    [SerializeField] public int maxHealthPotion = 3;
    [SerializeField] public int currentHealthPotion;
    [SerializeField] private bool hasPotion;

    public void DamagePlayer(float damageValue)
    {
        canHeal = false;
        currentHealth -= damageValue;
        UpdateUI();
    }

    public void HealPlayer(float healValue)
    {
        if (playerMovement.actionsInTurn > 0)
        {
            if (hasPotion == true)
            {
                if (currentHealth != maxHealth)
                { 
                    currentHealthPotion -= 1;
                    canHeal = false;
                    currentHealth += healValue;
                    if (currentHealth > maxHealth)
                    {
                        currentHealth = maxHealth;
                    }
                    UpdateUI();
                    playerMovement.UpdateActionPoints(1);
                    if (currentHealthPotion <= 0)
                    {
                        currentHealthPotion = 0;
                        hasPotion = false;
                    }
                }
            }
        }
    }

    public void GiveHealthPotion()
    {
        currentHealthPotion += 1;
        if (!hasPotion)
        {
            hasPotion = true;
        }
    }

    void UpdateUI()
    {
        displayImage.fillAmount = Mathf.Clamp01(currentHealth / maxHealth);
        displayImage.color = gradientHealth.Evaluate(displayImage.fillAmount);
    }

    private void Awake()
    {
        currentHealthPotion = maxHealthPotion;
        hasPotion = true;
    }

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        currentHealth = maxHealth;
        displayImage.fillAmount = 1;
        UpdateUI();
    }

    private void OnCollissionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Damage"))
        {
            DamagePlayer(10);
        }
    }
}
