using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATKEffect : MonoBehaviour
{
    // 플레이어의 공격 트리거가 접근 시 맞은 몬스터의 트랜스폼 기준으로 히트 스프라이트 이펙트 생성
    // 작성자 : 김장후
    GameObject[] ATK = new GameObject[3];
    public GameObject[] HitEffect = new GameObject[2];
    public Vector2 origin;
    public int randomEffectType;
    public float hitSpawnRange;
    public Vector2 hitEffectPos;


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy") || other.CompareTag("Boss"))
        {
            Vector2 contactPoint = other.ClosestPoint(transform.position);
            hitEffectPos = (contactPoint + (Random.insideUnitCircle * hitSpawnRange));
            SpawnEffect();
            //StartCoroutine(ActiveEffect());
        }
    }
    private void SpawnEffect() {

        GameObject effect = Instantiate(HitEffect[Random.Range(randomEffectType, HitEffect.Length)],hitEffectPos,Quaternion.identity);
        Destroy(effect,2f);
    }
    IEnumerator ActiveEffect() {

        HitEffect[0].SetActive(true);
        yield return new WaitForSeconds(3);
    }
}
