using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameStates state = GameStates.PlayerTurn;

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

}

public enum GameStates
{
    PlayerTurn, EnemyTurn, Pause, Menu, Death, EndGame
}