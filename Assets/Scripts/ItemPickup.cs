using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] bool hasPickedUp;

    [SerializeField] bool ammo;
    [SerializeField] public int maxAmmoREF;
    [SerializeField] public int currentAmmoREF;

    [SerializeField] bool potion;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] public int maxHealthPotionREF;
    [SerializeField] public int currentHealthPotionREF;

    [SerializeField] private GameObject _playerREF;
    [SerializeField] private GameObject uiObject;
    [SerializeField] private Text uiText;

    private void OnTriggerStay(Collider other)   
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!hasPickedUp)
            {
                if (potion == true)
                {
                    currentHealthPotionREF = playerHealth.currentHealthPotion;
                    PickUpPotion();
                }
                else if (ammo == true)
                {
                    currentAmmoREF = playerHealth.currentAmmo;
                    PickUpAmmo();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        uiObject.SetActive(false);
        if (hasPickedUp == true)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void Start()
    {
        _playerREF = GameObject.FindWithTag("Player");
        playerHealth = _playerREF.GetComponent<PlayerHealth>();

        if (potion)
        {
            currentHealthPotionREF = playerHealth.currentHealthPotion;
            maxHealthPotionREF = playerHealth.maxHealthPotion;
        }
        else if (ammo)
        {
            currentAmmoREF = playerHealth.currentAmmo;
            maxAmmoREF = playerHealth.maxAmmo;
        }
    }

    void PickUpPotion()
    {
        if (currentHealthPotionREF >= 0 && currentHealthPotionREF <= maxHealthPotionREF-1)
        {
            uiObject.SetActive(true);
            uiText.text = "You found a Health Potion\r\nDrink up when hurt!";
            playerHealth.GiveHealthPotion();
            hasPickedUp = true;
        }
        else
        {
            uiObject.SetActive(true);
            uiText.text = "You have max Health Potions!";
        }
    }

    void PickUpAmmo()
    {
        if (currentAmmoREF <= 0)
        {
            uiObject.SetActive(true);
            uiText.text = "You found more arrows!";
            playerHealth.GiveAmmo();
            hasPickedUp = true;
        }
        else
        {
            uiObject.SetActive(true);
            uiText.text = "You can't carry anymore arrows!";
        }
    }
}
