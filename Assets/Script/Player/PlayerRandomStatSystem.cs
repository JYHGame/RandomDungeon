using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;


public class PlayerRandomStatSystem : MonoBehaviour
{
    [Header("Stat Setting")]
    [SerializeField]
    private int totalStatPoints = 100; // 스탯 총합
    [SerializeField]
    private List<StatType> statTypes = new List<StatType>() { }; // 스탯 종류

    [Header("스탯을 부여할 대상 캐릭터")]
    [SerializeField] private Character targetCharacter; // Character.cs가 붙어있는 오브젝트를 연결

    public void OnClickGenerateCharacter()
    {
        if (targetCharacter == null)
        {
            Debug.LogError("대상 캐릭터가 할당되지 않았습니다! 인스펙터 창을 확인해주세요.");
            return;
        }

        // 2. 스탯 생성 함수 실행
        List<Stat> newStats = CreateRandomStats();

        // 3. 생성된 스탯을 대상 캐릭터에게 전달
        targetCharacter.ApplyGeneratedStats(newStats);

        Debug.Log("캐릭터 스탯 생성 및 적용 완료!");
    }

    // 실제 스탯을 랜덤으로 만들어내는 내부 함수
    private List<Stat> CreateRandomStats()
    {
        List<Stat> tempStats = new List<Stat>();

        // 규칙에 등록된 스탯 종류대로 0점짜리 목록을 만듭니다.
        foreach (StatType type in statTypes)
        {
            tempStats.Add(new Stat(type, 0));
        }

        // 총합 점수에 도달할 때까지 무작위 분배
        int remainingPoints = totalStatPoints;
        while (remainingPoints > 0)
        {
            int randomIndex = Random.Range(0, tempStats.Count);
            tempStats[randomIndex].Value++;
            remainingPoints--;
        }

        return tempStats; // 완성된 스탯 목록을 반환
    }
}
