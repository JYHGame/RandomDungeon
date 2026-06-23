using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "ScriptableObjects/Monster")]
public class Monster : ScriptableObject
{
    [SerializeField] 
    private string monsterName;
    [SerializeField] 
    private List<Stat> monsterStats = new List<Stat>();

    public string MonsterName => monsterName;
    public List<Stat> MonsterStats => monsterStats;
}