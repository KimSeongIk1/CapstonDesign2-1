using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //public Text Text_GameResult; // 게임의 결과를 표시해줄 Text Ui

    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject clearCamera;
    [SerializeField] private GameObject boss;
    private Animator animator;

    [SerializeField] GameObject setUI;

    private void Awake()
    {
        
        setUI.SetActive(false); // 게임이 시작되면 UI 팝업 창을 보이지 않도록 한다.
    }
    [SerializeField] private GameObject clearUI;
    [SerializeField] private GameObject overUI;
    [SerializeField] private GameObject bossUI;
    [SerializeField] private AudioClip[] audioClip;

    //[SerializeField] private AudioClip[] clipAudio;
    public void clipShow(AudioClip num)
    {
        GetComponent<AudioSource>().PlayOneShot(num);
    }
    public void Show(string ui, bool set)
    {
        setUI.SetActive(set);
        switch (ui)
        {
            case "클리어":
                clearUI.SetActive(set); // GameOver 팝업 창을 화면에 표시 시키고
                break;
            case "오버":
                overUI.SetActive(set); // GameOver 팝업 창을 화면에 표시 시키고
                break;
            case "보스UI":
                bossUI.SetActive(set); // GameOver 팝업 창을 화면에 표시 시키고
                break;
        }

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

    public IEnumerator GameClearLogic()
    {
        animator = boss.GetComponent<Animator>();

        //bossUI.SetActive(false);
        Show("보스UI", false);
        yield return new WaitForSeconds(1f);
        mainCamera.SetActive(false);
        clearCamera.SetActive(true);
        animator.SetTrigger("Die"); //* 체력이 0 이하라 죽음
        clipShow(audioClip[0]);
        yield return new WaitForSeconds(3f);
        clearCamera.SetActive(false);      
        mainCamera.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        Show("클리어", true);
    }
    

}
