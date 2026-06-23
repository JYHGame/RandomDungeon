using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[System.Serializable]
public class Stat
{
    [SerializeField] 
    private string statName;
    [SerializeField] 
    private int statValue;

    public string StatName => statName;
    public int Value
    {
        get => statValue;
        set => statValue = value;
    }

    public Stat(string name, int startValue = 0)
    {
        statName = name;
        statValue = startValue;
    }
}