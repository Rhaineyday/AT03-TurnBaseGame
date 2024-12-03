using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class PlayerStats
{
    public static int attackStat = 0;
    public static int rangedStat = 0;
    public static int blockStat = 0;
    public static int dodgeStat = 0;
    public static int ammoStat = 0;
    public static int healPotStat = 0;
    public static int healthStat = 0;

    public static void Increase()
    {
        attackStat +=1;
        rangedStat +=1;
        blockStat +=1;
        dodgeStat +=1;
        ammoStat +=1;
        healPotStat +=1;
        healthStat +=1;

    }
}
