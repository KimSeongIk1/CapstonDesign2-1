using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider; //체력 슬라이더
    public TMP_Text healthBarText; //체력 텍스트

    Damageable playerDamageable; //데미지 컴포넌트

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");//플레이어 태그 찾기

        if(player == null)
        {
            Debug.Log("No player found in the scene. Make sure it has tag 'Player'");//못 찾으면 디버그 로그 출력
        }
        playerDamageable = player.GetComponent<Damageable>();//데미지 컴포넌트 캐싱
    }
    
    void Start()
    {
        healthSlider.value = CalculateSliderPercentage(playerDamageable.Health, playerDamageable.MaxHealth); // 플레이어의 최대 체력과 현재 체력을 이용해 슬라이더 값 설정
        //healthBarText.text = "HP " + playerDamageable.Health + " / " + playerDamageable.MaxHealth;//현재 체력과 최대 체력 표시
    }
    private void OnEnable()//게임 오브젝트가 활성화 될 때 호출
    {
        playerDamageable.healthChanged.AddListener(OnPlayerHealthChanged);//데미지 컴포넌트의 HealthChanged 이벤트 리스너 등록
    }
    private void OnDisable()
    {
        playerDamageable.healthChanged.RemoveListener(OnPlayerHealthChanged);//이벤트 리스너 해제
    }

    private float CalculateSliderPercentage(float currentHealth, float maxHealth)
    {
        return  currentHealth / maxHealth; //현재 체력과 최대 체력을 이용해 슬라이더 퍼센트 값 계산
    }

    private void OnPlayerHealthChanged(int newHealth, int maxHealth)
    {
        //Debug.Log("OnHealth");
        healthSlider.value = CalculateSliderPercentage(newHealth, maxHealth);//데미지 컴포넌트의 체력변경 이벤트가 발생 시 호출
        //healthBarText.text = "HP " + newHealth + " / " + maxHealth;//새로운 체력과 최대 체력 값을 이용해 슬라이더 값과 체력 텍스트 업데이트
    }

}
