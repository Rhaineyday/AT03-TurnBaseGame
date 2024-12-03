using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combat : MonoBehaviour
{
    #region Player Variables
    public float damageValue = 5;
    private PlayerHealth playerHealth;
    #endregion
    #region Enemy Variables
    public float enemyAttackValue = 10;
    public float enemyMaxHealth = 100;
    public float enemyCurrentHealth;
    public Image Enemyhealthbar;
    public GameObject enemy;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        enemyCurrentHealth = enemyMaxHealth;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }
    #region Enemy Taking Damage
    public void PlayerAttackMelee()
    {
        if (playerHealth.GetComponent<PlayerMovement>().actionsInTurn > 0)
        {
            playerHealth.GetComponent<PlayerMovement>().UpdateActionPoints(1);
            UpdateHP(damageValue);
            if (enemyCurrentHealth == 0f)
            {
                enemy.SetActive(false);
            }
        }    
    }
    public void PlayerAttackRange()
    {

        if (playerHealth.GetComponent<PlayerMovement>().actionsInTurn > 1)
        {
            playerHealth.GetComponent<PlayerMovement>().UpdateActionPoints(2);
            UpdateHP(damageValue * 0.5f);
            if (enemyCurrentHealth == 0f)
            {
                enemy.SetActive(false);
            }
        }

    }
    private void Update()
    {
    
    }

    private void UpdateHP(float damageValue)
    {
        //apply damage
        enemyCurrentHealth -= damageValue;
        //Update UI
        Enemyhealthbar.fillAmount = Mathf.Clamp01(enemyCurrentHealth / enemyMaxHealth);
    }
    #endregion
    #region Player Taking Damage
    public void EnemyAttack(float value)
    {
        Debug.Log("Enemy Attack");

        playerHealth.DamagePlayer(value);
        Debug.Log("Change Player Health");
        playerHealth.GetComponent<PlayerMovement>().StartPlayerTurn();
        Debug.Log("Player Turn");
    }
    #endregion
}
