using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneJump : MonoBehaviour
{
    public void NextSceneWithNum()
    {
        // 씬 번호를 이용해서 씬 이동
        SceneManager.LoadScene(1);  // 1 번째 씬 로드
    }
}
