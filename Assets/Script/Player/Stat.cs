using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[System.Serializable]
public class Stat
{
    [SerializeField]
    private StatType statName;
    [SerializeField]
    private int statValue;

    public StatType StatName => statName;
    public int Value
    {
        get => statValue;
        set => statValue = value;
    }

    public Stat(StatType name, int startValue = 0)
    {
        statName = name;
        statValue = startValue;
    }
}