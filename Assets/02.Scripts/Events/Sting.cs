using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sting : MonoBehaviour
{
    private Collider2D col;
    public GameObject player; // 플레이어 오브젝트를 연결할 변수

    private Damageable P_Damage; // 플레이어의 Damageable 컴포넌트를 저장할 변수

    public bool isHit = false;
    public float invincible = 1;

    private bool isCoroutineRunning = false;

    void Start()
    {
        col = GetComponent<Collider2D>();
        P_Damage = player.GetComponent<Damageable>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isHit) // isHit이 false인 경우에만 실행
        {
            isHit = true;

            Debug.Log("깨시");

            // 가시 접촉시 Sting_hit 메서드 호출
            Sting_hit();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isHit = false;
        }
    }

    public void Sting_hit()
    {
        if (!isCoroutineRunning)
        {
            StartCoroutine("auch");
        }
    }

    IEnumerator auch()
    {
        isCoroutineRunning = true;

        P_Damage.Health -= 10;

        yield return new WaitForSeconds(1f); // 1초 대기

        isCoroutineRunning = false;

        if (isHit) // 여전히 가시와 겹쳐있는 경우 추가 피해 입히기
        {
            Sting_hit();
        }
    }
}