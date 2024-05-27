using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//<작업 날짜>
/*24.05.26 두억시니 인트로 제작

1. 프로젝트 <Tag> 에 추가 후 태그 지정
    BossName = BossCanvas의 BossName 
    PlayerUI = PlayerCanvas

*/
//<인트로 순서>
/*
1. 일반 스테이지에서 보스 스테이지로 이동하면 플레이어 키 입력, 모든 UI가 꺼짐
2. 카메라가 가운데 고정되며 도깨비불을 줌인
3. 도깨비불이 잠시 후 사라지며 번개 한개가 내려옴
4. 흙먼지와 함께 두억시니 생성 후 애니메이션 트리거 , 보스 이름 UI가 옆에서 나온 후 잠시 뒤 사라짐
5. 보스전에 돌입함과 동시에 보스 체력바 UI가 내려옴
 */
public class DueogsiniIntro : MonoBehaviour
{
    private GameObject fireObj, introCamera, bossName, introBoss;

    void Start()
    {
       
        introCamera = GameObject.Find("Intro Camera");
        bossName = GameObject.Find("UIManager"); //보스 이름
        //boss = GameObject.Find("Dueogsini"); //보스
        fireObj = GameObject.FindWithTag("IntroFire"); // 인트로용 도깨비불
        introBoss = GameObject.Find("Intro"); //보스

        //camera.GetComponent<CameraController>().stop = true; //카메라를 못 움직이게 한다.

        //camera.GetComponent<Transform>().position = new Vector3(0.5f, -5, -10); //정해진 위치로 카메라를 옮긴다.
        //camera.GetComponent<Camera>().orthographicSize = 10; //카메라 크기(시아범위)를 넓게 정한다.
        //player.GetComponent<PMC>().stop = true; //플레이어가 못 움직이게 한다.
        StartCoroutine(Intro());
    }

    [SerializeField] private GameObject player, playerUI, mainCamera, boss, bossUI;
    IEnumerator Intro()
    {
        Debug.Log("인트로 시작");
        yield return new WaitForSeconds(1f);
        fireObj.SetActive(false);
        yield return new WaitForSeconds(2f);
        bossName.GetComponent<BossNameShower>().ShowBossTitel();
        yield return new WaitForSeconds(5f);
        introCamera.SetActive(false);
        mainCamera.SetActive(true);
        player.SetActive(true);
        playerUI.SetActive(true);
        introBoss.SetActive(false);
        bossUI.SetActive(true) ;
        boss.SetActive(true);
    }
    //IEnumerator Intro()
    //{
    //    playerUI.SetActive(false); //플레이어 UI 끔
    //    fireObj.GetComponent<Animator>().SetTrigger("introStartFire"); // 도깨비불 시작 애니메이션 재생
    //    yield return new WaitForSeconds(5f);

    //    fireObj.GetComponent<Animator>().SetTrigger("introHideFire"); // 도깨비불이 사라지는 애니메이션 재생
    //    fireObj.gameObject.SetActive(false); // 도깨비불 비활성화
    //    yield return new WaitForSeconds(1f);
    //    boss.GetComponent<Animator>().SetTrigger("intro"); // 두억이 인트로 애니메이션 재생
    //    camera.GetComponent<cameraShake>().shakeCamera(0.5f); //번개 애니메이션과 함께 진동이 울림

    //    yield return new WaitForSeconds(3);

    //    bossName.GetComponent<BossNameShower>().ShowBossTitel(); //보스 이름이 옆에서 나타남
    //    yield return new WaitForSeconds(3);

    //    bossName.GetComponent<BossNameShower>().HideBossTitel(); //보스 이름이 옆으로 사라짐

    //    boss.GetComponent<Animator>().SetTrigger("ready"); // 보스전 시작 애니메이션
    //    yield return new WaitForSeconds(1);

    //    playerUI.SetActive(true); //플레이어 UI 켬
    //    boss.GetComponent<Boss_Controller>().NextPatternPlay(0); // 보스 패턴 0번부터 시작
    //    player.GetComponent<PMC>().stop = false; //플레이어를 움직이게 한다.
    //}
}
