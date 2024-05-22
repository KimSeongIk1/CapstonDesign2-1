using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Skill : MonoBehaviour
{
    public GameObject[] spawnPoints; // 게임오브젝트 위치를 저장할 배열
    public GameObject[] alertBoxPrefabs; // 얼럿박스 프리팹을 저장할 배열

    // 원하는 배열의 위치에 원하는 얼럿박스 프리팹을 생성하는 함수
    public void SpawnAlertBox(int spawnPointIndex, int alertBoxIndex)
    {
        // spawnPointIndex와 alertBoxIndex가 배열의 범위 내에 있는지 확인
        if (spawnPointIndex < 0 || spawnPointIndex >= spawnPoints.Length ||
            alertBoxIndex < 0 || alertBoxIndex >= alertBoxPrefabs.Length)
        {
            Debug.LogWarning("Invalid spawn point index or alert box index.");
            return;
        }

        // 선택한 spawnPointIndex의 위치에 alertBoxPrefabs의 alertBoxIndex번째 프리팹을 생성
        Instantiate(alertBoxPrefabs[alertBoxIndex], spawnPoints[spawnPointIndex].transform.position, Quaternion.identity);
    }

    // 구분선----------------------------
    void H()
    {
        SpawnAlertBox(0, 0);
    }

    void V()
    {
        SpawnAlertBox(1, 1);
    }

    // 구분선----------------------------

    void Thunder()
    {
        SpawnAlertBox(2, 2);
    }

    // 구분선----------------------------

    void STOMP()
    {
        SpawnAlertBox(3, 3);
    }

}