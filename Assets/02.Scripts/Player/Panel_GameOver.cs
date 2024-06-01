using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Panel_GameOver : MonoBehaviour
{
    //public Text Text_GameResult; // 게임의 결과를 표시해줄 Text Ui
    private void Awake()
    {
        transform.gameObject.SetActive(false); // 게임이 시작되면 GameOver 팝업 창을 보이지 않도록 한다.
    }

    public void Show()
    {
        transform.gameObject.SetActive(true); // GameOver 팝업 창을 화면에 표시 시키고
    }

    public void OnClick_Retry() // '재도전' 버튼을 클릭하며 호출 되어질 함수
    {
        Scene nowScene = SceneManager.GetActiveScene(); //현재 활성화된 씬 불러옴
        SceneManager.LoadScene(nowScene.buildIndex);
        //switch (nowScene)
        //{
        //    case 0:
        //        SceneManager.LoadScene(0);
        //        break;
        //    case 1:
        //        SceneManager.LoadScene(1);
        //        break;
        //}
        //SceneManager.LoadScene("GameplayScene"); // SceneManager의 LoadScene 함수를 사용하여 현재 신 'GameScene'을 다시 불러오도록 시킨다.
        // 같은 신을 다시 불러오면 게임이 재시작 된다.
    }
    public void OnClick_MainMenu() // 메인화면 버튼을 클릭하며 호출 되어질 함수
    {
        SceneManager.LoadScene(0); //메인화면으로
    }
}