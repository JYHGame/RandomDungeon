using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Stat")]
    [SerializeField] 
    private List<Stat> playerStat = new List<Stat>();

    public void ApplyGeneratedStats(List<Stat> generatedStats)
    {
        playerStat = generatedStats;
    }

    public List<Stat> GetPlayerStats()
    {
        return playerStat;
    }
}
