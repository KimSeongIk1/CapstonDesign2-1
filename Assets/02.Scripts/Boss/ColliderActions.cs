using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderActions : MonoBehaviour
{
    //public GameObject target;
    
    [SerializeField] int damage = 0; // 데미지
    [SerializeField] Vector2 knockBack = Vector2.zero; // 넉백 거리
    private GameObject target;
    private void Awake()
    {
        target = GameObject.FindWithTag("Player");
    }
    void OnTriggerEnter2D(Collider2D collision) // 콜라이더에 접촉하는 동안 발생
    {
        if (collision.gameObject.tag == "Player") // 콜라이더를 가진 오브젝트가 플레이어라면 플레이어의 데미지 함수 호출
        {
            Debug.Log("충돌!");
            target.GetComponent<Damageable>().Hit(damage, knockBack);

        }
    }
    public void ObjDisable()
    {
        this.gameObject.SetActive(false);
    }
}
