using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneMove : MonoBehaviour
{
    public GameObject enemyContainer; // 적들이 포함된 게임 오브젝트
    public float delayBeforeNextScene = 3.0f; // 씬 전환 전에 기다리는 시간

    // 다음 씬을 로드합니다. (씬 빌드 인덱스를 기준으로)
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // 만약 다음 씬 인덱스가 빌드된 씬의 수보다 크다면, 첫 번째 씬으로 돌아갑니다.
        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0; // 첫 번째 씬 인덱스 (0)
        }

        SceneManager.LoadScene(nextSceneIndex);
    }

    // 적들이 모두 죽었는지 체크합니다.
    private void Update()
    {
        // enemyContainer의 자식 오브젝트가 모두 제거되었는지 확인합니다.
        if (enemyContainer.transform.childCount == 0)
        {
            StartCoroutine(LoadNextSceneWithDelay());
        }
    }

    // 지연 후 다음 씬을 로드합니다.
    private IEnumerator LoadNextSceneWithDelay()
    {
        yield return new WaitForSeconds(delayBeforeNextScene);
        LoadNextScene();
    }
}