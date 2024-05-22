using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider staminaSlider; //스테미나 슬라이더
    PlayerController playerControl; //플레이어 컴포넌트
    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");//플레이어 태그 찾기

        if (player == null)
        {
            Debug.Log("No player found in the scene. Make sure it has tag 'Player'");//못 찾으면 디버그 로그 출력
        }
        playerControl = player.GetComponent<PlayerController>();//데미지 컴포넌트 캐싱
    }

    void Start()
    {
        
        staminaSlider.value = SPCalculateSliderPercentage(playerControl.StaminaValue, playerControl.MaxStamina);
        Debug.Log("set");// 플레이어의 최대 체력과 현재 체력을 이용해 슬라이더 값 설정
    }
    private void OnEnable()//게임 오브젝트가 활성화 될 때 호출
    {
        playerControl.staminaChanged.AddListener(OnPlayerStaminaChanged);//데미지 컴포넌트의 HealthChanged 이벤트 리스너 등록
    }
    private void OnDisable()
    {
        playerControl.staminaChanged.RemoveListener(OnPlayerStaminaChanged);//이벤트 리스너 해제
    }
    private float SPCalculateSliderPercentage(float currentStamina, float maxStamina)
    {
        Debug.Log("Calculate");
        return currentStamina / maxStamina; //현재 스태미나와 최대 스태미나를 이용해 슬라이더 퍼센트 값 계산
    }

    private void OnPlayerStaminaChanged(int newStamina, int maxStamina)
    {
        Debug.Log("OnplayerSys");
        staminaSlider.value = SPCalculateSliderPercentage(newStamina, maxStamina);//플레이어 컴포넌트의 스태미나 변경 이벤트가 발생 시 호출
        //healthBarText.text = "HP " + newHealth + " / " + maxHealth;//새로운 체력과 최대 체력 값을 이용해 슬라이더 값과 체력 텍스트 업데이트
    }
}
