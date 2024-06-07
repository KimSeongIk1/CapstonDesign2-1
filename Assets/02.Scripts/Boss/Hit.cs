using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UIElements;

public class Hit : MonoBehaviour // 보스가 피격 받으면 생기는 이벤트를 관리하는 스크립트
{
    private SpriteRenderer sprite; 
    private int hitDamage;

    [SerializeField] private Material whiteFlashMaterial;
    private Material originMaterial;
    private void Awake()
    {
        sprite = GameObject.FindGameObjectWithTag("Boss").GetComponent<SpriteRenderer>();
        originMaterial = sprite.material;
    }
    private void OnTriggerEnter2D(Collider2D collision) // 플레이어 공격의 콜라이더랑 접촉시 데미지를 입고 피격효과 코루틴을 실행
    {
        if (collision.gameObject.CompareTag("PlayerAttack")) // 플레이어 공격 콜라이더
        {
            //Debug.Log("보스피격당함"); // 피격 확인 디버깅용 로그
            
            StartCoroutine(HitSprite()); // 피격효과 코루틴
            hitDamage = collision.gameObject.GetComponent<Attack>().attackDamage; // 플레이어 공격 오브젝트 <Attack> 스크립트 함수를 호출하여 데미지를 적용하기 위해 hitDamage 변수에 초기화
            Debug.Log("보스가 " + hitDamage + "데미지를 입음"); // 데미지가 들어갔는지 확인하는 디버깅용 로그
            GameObject.FindGameObjectWithTag("BossHealth").GetComponent<BossHealth>().Damage(hitDamage); // 보스 체력바의 <BossHealth> 스크립트 Damage 함수에 hitDamage 변수를 매개변수로 사용하여 데미지를 줌
            
        }
    }
    IEnumerator HitSprite() // 피격시 색상값이 변하는 효과를 줌
    {
        sprite.material = whiteFlashMaterial;
        yield return new WaitForSeconds(0.5f);
        sprite.material = originMaterial;
        //sprite.color = new Color(0.8f, 0.8f, 0.8f, 1f);
        //yield return new WaitForSeconds(0.5f);
        //sprite.color = new Color(1, 1, 1, 1);
    }
    public Material GetWhiteFlash()
    {
        return whiteFlashMaterial;
    }
}


