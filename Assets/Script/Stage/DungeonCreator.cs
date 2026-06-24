using UnityEngine;
using System.Collections.Generic;

// 1. 몬스터를 레벨(단계)별로 묶어두는 수영장(Pool)
[System.Serializable]
public class MonsterPool
{
    public int level;                  // 몬스터 단계 (예: 1, 2, 3...)
    public List<Monster> monsters; // 해당 단계에 속하는 몬스터 파일들 (슬라임, 고블린 등)
}

// 2. 스테이지마다 요구하는 몬스터 스폰 규칙
[System.Serializable]
public class SpawnRule
{
    public int monsterLevel; // 뽑을 몬스터의 단계
    public int spawnCount;   // 몇 마리 뽑을지
}

// 3. 각 스테이지의 설계도
[System.Serializable]
public class StageBlueprint
{
    public string stageName;          // 예: "1층", "10층(보스)"
    public List<SpawnRule> rules;     // 이 스테이지의 스폰 규칙들
}

// 4. 게임 시작 시 메모리에 '실제'로 무작위 생성되는 스테이지 데이터
public class RuntimeStage
{
    public string stageName;
    public List<Monster> spawnedMonsters = new List<Monster>();
}

public class DungeonCreator : MonoBehaviour
{
    [Header("몬스터 데이터베이스 (레벨별 세팅)")]
    [SerializeField] private List<MonsterPool> monsterDatabase;

    [Header("1~10스테이지 무작위 생성 설계도")]
    [SerializeField] private List<StageBlueprint> stageBlueprints;

    // 게임 시작 시 조립된 실제 던전 데이터 보관소
    private List<RuntimeStage> currentDungeon = new List<RuntimeStage>();
    private int currentStageIndex = 0;

    private void Start()
    {
        // 게임이 시작되자마자 10개의 스테이지를 무작위로 생성합니다.
        GenerateRandomDungeon();
        LoadCurrentStage();
    }

    // 설계도를 보고 무작위 몬스터를 뽑아 던전을 생성하는 핵심 로직
    private void GenerateRandomDungeon()
    {
        currentDungeon.Clear();

        // 10개의 설계도를 하나씩 읽어옵니다.
        foreach (StageBlueprint blueprint in stageBlueprints)
        {
            RuntimeStage newStage = new RuntimeStage();
            newStage.stageName = blueprint.stageName;

            // 설계도에 적힌 규칙대로 몬스터를 뽑습니다.
            foreach (SpawnRule rule in blueprint.rules)
            {
                // 1. 데이터베이스에서 해당 레벨의 몬스터 목록(Pool) 찾기
                MonsterPool pool = monsterDatabase.Find(p => p.level == rule.monsterLevel);

                if (pool == null || pool.monsters.Count == 0)
                {
                    Debug.LogWarning($"{rule.monsterLevel}단계 몬스터 데이터가 없습니다!");
                    continue;
                }

                // 2. 요구하는 마리 수만큼 랜덤으로 뽑아서 현재 스테이지에 넣기
                for (int i = 0; i < rule.spawnCount; i++)
                {
                    int randomIndex = Random.Range(0, pool.monsters.Count);
                    newStage.spawnedMonsters.Add(pool.monsters[randomIndex]);
                }
            }

            // 완성된 스테이지를 던전에 추가
            currentDungeon.Add(newStage);
        }

        Debug.Log($"<color=green>--- 총 {currentDungeon.Count}개의 스테이지가 무작위로 생성되었습니다! ---</color>");
    }

    // 만들어진 던전에서 현재 스테이지의 몬스터 불러오기
    public void LoadCurrentStage()
    {
        if (currentStageIndex >= currentDungeon.Count)
        {
            Debug.Log("★ 게임 클리어! 모든 던전을 돌파했습니다! ★");
            return;
        }

        RuntimeStage currentStage = currentDungeon[currentStageIndex];
        Debug.Log($"<color=yellow>====== {currentStage.stageName} 진입 ======</color>");

        foreach (Monster monster in currentStage.spawnedMonsters)
        {
            Debug.Log($"[출현] {monster.MonsterName}");
        }

        // TODO: 여기서 캐릭터와 생성된 몬스터 간의 '자동 전투'를 시작하면 됩니다.
    }

    // 전투 승리 시 호출될 함수
    public void OnStageClear()
    {
        currentStageIndex++;
        LoadCurrentStage();
    }
}
