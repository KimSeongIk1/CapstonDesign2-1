using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class DetectionZone : MonoBehaviour //트리거 콜라이더가 감지하는 콜라이더 관리
{
    public UnityEvent noCollidersRemain; // 트리거 콜라이더 영역 내에 남은 콜라이더가 없을때 발생하는 이벤트

    public List<Collider2D> detectedColliders = new List<Collider2D>(); // 트리거 콜라이더랑 겹쳐있는 콜라이더 목록
    Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>(); // 캐싱
    }

    private void OnTriggerEnter2D(Collider2D collision)  // 콜라이더가 트리거 콜라이더 영역에 진입할 때
    {
        detectedColliders.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision) // 벗어날 때
    {
        detectedColliders.Remove(collision); // 리스트에서 벗어난 콜라이더 제거

        if(detectedColliders.Count <= 0) // 남은 콜라이더가 없으면 이벤트 발생
        {
            noCollidersRemain.Invoke(); 
        }
    }

    
}
