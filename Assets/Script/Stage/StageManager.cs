using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [Header("게임의 전체 스테이지 순서")]
    [SerializeField]
    private List<Stage> stageSequence = new List<Stage>();

    private int currentStageIndex = 0; // 현재 진행 중인 스테이지 번호 (0부터 시작)

    private void Start()
    {
        // 게임 시작 시 첫 번째 스테이지를 불러옵니다.
        LoadCurrentStage();
    }

    // 현재 스테이지의 정보를 가져오고 몬스터를 불러오는 함수
    public void LoadCurrentStage()
    {
        // 예외 처리: 등록된 스테이지가 없을 때
        if (stageSequence == null || stageSequence.Count == 0)
        {
            Debug.LogError("StageManager에 등록된 스테이지 데이터가 없습니다!");
            return;
        }

        // 승리 조건: 모든 스테이지를 다 깼을 때
        if (currentStageIndex >= stageSequence.Count)
        {
            Debug.Log("★ 축하합니다! 마지막 스테이지까지 모두 클리어하여 승리했습니다! ★");
            return;
        }

        // 현재 순서의 스테이지 데이터 가져오기
        Stage currentStage = stageSequence[currentStageIndex];
        Debug.Log($"<color=yellow>====== {currentStage.StageName} 시작! ======</color>");

        // 스테이지에 포함된 몬스터들 불러오기
        foreach (Monster monster in currentStage.MonstersInStage)
        {
            Debug.Log($"몬스터 로드 성공 -> 이름: {monster.MonsterName}");

            // 몬스터가 가진 스탯들 출력해보기
            foreach (Stat stat in monster.MonsterStats)
            {
                Debug.Log($"   [{monster.MonsterName}] {stat.StatName} : {stat.Value}");
            }
        }

        // TODO: 여기서 몬스터 로드가 끝나면 캐릭터와의 [자동 전투 시작] 함수를 호출하면 됩니다.
    }

    // 전투에서 이겼을 때 외부(전부 매니저 등)에서 호출해줄 함수
    public void OnStageClear()
    {
        Debug.Log($"{stageSequence[currentStageIndex].StageName} 클리어!");
        currentStageIndex++; // 다음 스테이지 인덱스로 증가
        LoadCurrentStage();  // 다음 스테이지 불러오기
    }
}

