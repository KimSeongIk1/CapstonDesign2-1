using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertBox : MonoBehaviour
{
    public float targetTransparency = 0f; // 목표 투명도
    public float transparencyDuration = 1f; // 변경에 걸리는 시간
    public float visibleDuration = 0.3f; // 스프라이트를 보여줄 시간

    private SpriteRenderer spriteRenderer;
    private Collider2D collider;
    private Color originalColor;
    private float elapsedTime = 0f;
    private bool isVisible = true;

    Attack Attack;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        originalColor = spriteRenderer.color;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (isVisible)
        {
            float t = Mathf.Clamp01(elapsedTime / transparencyDuration); // 보간값 계산

            Color newColor = spriteRenderer.color;
            newColor.a = Mathf.Lerp(originalColor.a, targetTransparency, t); // 보간된 투명도 설정
            spriteRenderer.color = newColor;

            if (t >= 1f)
            {
                CheckForPlayerCollision(); // 플레이어 충돌 확인
                
                elapsedTime = 0f;
                isVisible = false;
                Invoke("DisableRenderer", visibleDuration);
                Destroy(gameObject, 3f); // 3초 후에 해당 게임 오브젝트 파괴
            }
        }
    }

    void DisableRenderer()
    {
        spriteRenderer.enabled = false;
    }

    void CheckForPlayerCollision()
    {
        if (targetTransparency == 1f && collider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            Debug.Log("플레이어가 데미지를 입음"); // 데미지 로그 출력
        }
    }
}