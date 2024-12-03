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

    [SerializeField] public int maxAmmo = 3;
    [SerializeField] public int currentAmmo;
    [SerializeField] public bool hasAmmo;

    public Text healthPotionDisplayText;
    public Text ammoDisplayText;


    public void DamagePlayer(float damageValue)
    {
        canHeal = false;
        currentHealth -= DefenceChance(damageValue);
        UpdateUI();
    }

    public int DefenceChance(float damage)
    {
        int dice = Random.Range(1,21);
        int dodgeVsBlock = Random.Range(0,2);
        int defence = 0;
        if (dice == 20)
        {
            Debug.Log("Nat 20");
            return defence;
        }
        else if (dice >= 10 && dice < 20)
        {
            Debug.Log("High Roll");
            if (dodgeVsBlock < 1f)
            {
                defence = (int)(damage / 2) - PlayerStats.dodgeStat;
                Debug.Log("Dodge");
            }
            else
            {
                defence = (int)(damage / (1.25 + (1 * PlayerStats.blockStat)));
                Debug.Log("Block");
            }
        }
        else if (dice >1 && dice <10)
        {
            Debug.Log("Low Roll");
            defence = (int)damage;
        }
        else
        {
            Debug.Log("Nat 1");
            defence = (int)damage*2;
        }
        
        return defence;
    }
    public void HealPlayer(float healValue)
    {
        if (playerMovement.actionsInTurn > 1)
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
                    playerMovement.UpdateActionPoints(2);
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

    public void UseAmmo()
    {
        if (hasAmmo == true)
        {
            if (currentAmmo > 0)
            {
                currentAmmo -= 1;
            }
        }
    }

    void UpdateUI()
    {
        displayImage.fillAmount = Mathf.Clamp01(currentHealth / maxHealth);
        displayImage.color = gradientHealth.Evaluate(displayImage.fillAmount);
    }

    private void Awake()
    {
        currentHealthPotion = maxHealthPotion + (3*PlayerStats.healPotStat);
        hasPotion = true;
        hasAmmo = true;
    }

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        currentHealth = maxHealth;
        currentAmmo = maxAmmo;
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
    public void SetValue()
    {
        maxHealthPotion += (3 * PlayerStats.healPotStat);
        maxHealth += (10 * PlayerStats.healthStat);
        maxAmmo += (3 * PlayerStats.ammoStat);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Shrine"))
        {
            PlayerStats.Increase();
            SetValue();
            Destroy(other);
        }
    }
}
