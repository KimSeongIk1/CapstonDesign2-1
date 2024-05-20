using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStage : MonoBehaviour
{
    public GameObject mobsObject; // "mobs" GameObject에 대한 참조
    public GameObject player; // 플레이어 프리팹에 대한 참조
    private int currentSceneIndex = 0; // 현재 씬의 인덱스를 추적하는 변수
    private int previousChildCount = 0; // 이전 프레임에서의 "mobsObject"의 자식 개수를 추적하는 변수
    private List<string> sceneNames = new List<string> { "GameplayScene", "stage2", "stage3" }; // 씬 이름을 저장하는 배열
    private bool[] sceneLoadedStatus; // 각 씬이 로드되었는지 여부를 저장하는 배열

    void Start()
    {
        // 씬 로드 상태 배열 초기화
        sceneLoadedStatus = new bool[sceneNames.Count];

        // 현재 씬을 로드하고 로드 상태를 갱신
        LoadScene(currentSceneIndex);
    }

    void Update()
    {
        // 모든 몬스터가 파괴되었는지 확인
        int currentChildCount = mobsObject.transform.childCount;

        // 이전 프레임에서 자식 개수가 0이고 현재 자식 개수가 1 이상이라면
        if (previousChildCount == 0 && currentChildCount > 0)
        {
            // 다음 씬으로 이동
            LoadNextScene();
        }

        // 이전 프레임에서의 자식 개수를 현재 프레임의 자식 개수로 업데이트
        previousChildCount = currentChildCount;
    }

    void LoadNextScene()
    {
        // 현재 씬을 비활성화
        SceneManager.UnloadSceneAsync(sceneNames[currentSceneIndex]);

        // 다음 씬으로 인덱스를 이동
        currentSceneIndex++;

        // 모든 씬을 로드했는지 확인하고, 다음 씬이 있다면 로드
        if (currentSceneIndex < sceneNames.Count)
        {
            LoadScene(currentSceneIndex);
        }
    }

    void LoadScene(int index)
    {
        // 이미 해당 씬이 로드되었다면 로드하지 않음
        if (!sceneLoadedStatus[index])
        {
            SceneManager.LoadScene(sceneNames[index], LoadSceneMode.Additive);
            sceneLoadedStatus[index] = true;
        }
    }
}