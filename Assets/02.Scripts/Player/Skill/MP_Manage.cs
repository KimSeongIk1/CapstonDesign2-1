using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// 작성자 : 김장후
public class MP_Manage : MonoBehaviour
{
    public Image mpBar;                // 마나 게이지 UI
    public GameObject playerObj;        // 플레이어 오브젝트
    PlayerController playerController;  // 플레이어 컨트롤러 스크립트
    private void Awake() {
        playerController = playerObj.GetComponent<PlayerController>(); // 플레이어 컨트롤러 스크립트 받아오기
    }

    private void Update()  // 플레이어의 마나량에 접근해 마나량에 따라 마나UI의 채우기값 조정
    {
        /*if (playerController.manaValueNow > 15)
            manaImg.color = new Color(manaImg.color.r, manaImg.color.g, manaImg.color.b, 1f);   // 마나가 15 이상일 시 투명도 조절로 보이게 만듦
        if (playerController.manaValueNow <= 5)
        {
            manaImg.fillAmount = 0f;
            manaImg.color = new Color(manaImg.color.r, manaImg.color.g, manaImg.color.b, 0f);   // 일정 마나 미달일 시 투명화
        }
        else if (playerController.manaValueNow <= 15)
            manaImg.fillAmount = 0.1f;
        else if(playerController.manaValueNow <= 20)
            manaImg.fillAmount = 0.2f;
        else if (playerController.manaValueNow <= 40)
            manaImg.fillAmount = 0.4f;
        else if (playerController.manaValueNow <= 60)
            manaImg.fillAmount = 0.6f;
        else if (playerController.manaValueNow <= 80)
            manaImg.fillAmount = 0.8f;
        else if (playerController.manaValueNow == 100)
        {
            manaImg.fillAmount = 1f;
        }*/ //과거 원형 마나 게이지 구현방법
        if (mpBar != null) // 만약 mpBar 이미지가 할당되었다면
            mpBar.fillAmount = ((float)playerController.manaValueNow / (float)playerController.MaxMana); // hpBar fillAmount값 변경

        //print((playerController.manaValueNow / playerController.MaxMana));
    }
    public void MpCharge()
    {
        if(playerController.manaValueNow < playerController.MaxMana)
        {
            Debug.Log("MP 충전, 현재 마나: "+ playerController.manaValueNow);
            playerController.manaValueNow += 20; // 플레이어 스크립트에 마나량 추가
        }
    }
}
