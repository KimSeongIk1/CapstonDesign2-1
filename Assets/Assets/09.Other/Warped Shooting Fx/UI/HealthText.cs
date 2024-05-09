using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    public Vector3 moveSpeed = new Vector3(0, 75, 0); //텍스트 이동 속도
    public float timeToFade = 1f; // 텍스트가 페이드 처리되어 사라지는 시간


    RectTransform textTransform; //텍스트 위치 조작하는 컴포넌트
    TextMeshProUGUI textMeshPro; //텍스트 내용 표시하는 컴포넌트

    private float timeElapsed = 0f; //텍스트과 생성된 후 시간 누적 값
    private Color startColor; //텍스트 초기 색상

    private void Awake()
    {
        textTransform = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
        startColor = textMeshPro.color;
    }
    private void Update()
    {
        textTransform.position += moveSpeed * Time.deltaTime; // 텍스트 위치를 이동 속도와 프레임 시간을 이용해 업데이트

        timeElapsed += Time.deltaTime; // 텍스트 생성된 후 시간 누적

        if(timeElapsed < timeToFade) // 텍스트 생성 시간이 페이드 시간보다 작을 때
        {
            float fadeAlpha = startColor.a * (1 - (timeElapsed / timeToFade)); // 텍스트의 알파 값 페이드 처리 계산
            textMeshPro.color = new Color(startColor.r, startColor.g, startColor.b, fadeAlpha); // 계산된 알파 값을 이용해 텍스트 색상 업데이트
        } else
        {
            Destroy(gameObject); // 게임 오브젝트 제거
        }
    }

}
