using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom_Approach : MonoBehaviour
{
    private Collider2D collider;
    private bool bossRoomEntered = false;
    public GameObject bossObject; // 보스 오브젝트를 연결할 변수
    public GameObject bossHP; // 보스 체력바 연결할 변수

    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !bossRoomEntered)
        {
            bossRoomEntered = true;
            Debug.Log("보스방 진입");

            // 보스 오브젝트 활성화
            bossHP.SetActive(true);
            bossObject.SetActive(true);

            // 이벤트 리스너 제거
            Destroy(gameObject.GetComponent<Collider2D>());
        }
    }
}