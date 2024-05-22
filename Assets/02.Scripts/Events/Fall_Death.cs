using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fall_Death : MonoBehaviour
{
    private Collider2D collider;
    private bool isFall = false;
    public GameObject Player; // 플레이어 오브젝트를 연결할 변수

    private Damageable playerDamageable; // 플레이어의 Damageable 컴포넌트를 저장할 변수

    void Start()
    {
        collider = GetComponent<Collider2D>();
        playerDamageable = Player.GetComponent<Damageable>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isFall)
        {
            isFall = true;
            Debug.Log("추락");

            // 플레이어의 체력을 0으로 설정하여 추락사 처리
            if (playerDamageable != null)
            {
                playerDamageable.Health = 0;
            }


            // 이벤트 리스너 제거
            Destroy(gameObject.GetComponent<Collider2D>());
        }
    }
}