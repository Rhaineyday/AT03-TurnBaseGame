using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameStates state = GameStates.PlayerTurn;
    public GameObject menuScreen;
    public Text menuText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null && instance != this)
        {
            Destroy(this);
        }
    }

    public void PlayerLose()
    {
        state = GameStates.PlayerLose;
        menuScreen.SetActive(true);
        menuText.text = "You lose";
    }

    public void PlayerWin()
    {
        state = GameStates.PlayerWin;
        menuScreen.SetActive(true);
        menuText.text = "You win";
    }
}

public enum GameStates
{
    PlayerTurn, EnemyTurn, Pause, Menu, PlayerLose, PlayerWin
}