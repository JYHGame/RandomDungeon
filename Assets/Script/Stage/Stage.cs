using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "ScriptableObjects/Stage")]
public class Stage : ScriptableObject
{
    [SerializeField] 
    private string stageName;
    [SerializeField] 
    private List<Monster> monstersInStage = new List<Monster>();

    public string StageName => stageName;
    public List<Monster> MonstersInStage => monstersInStage;
}