using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 작성자 : 김장후
/*본 스크립트는 싱글톤 패턴을 사용해 플레이어와 같이 중요한 정보를 모든 씬에 사용될 수 있도록 저장하는 게임 매니저입니다.*/
public class DataManager : MonoBehaviour
{
    private static DataManager instance = null; // Instance를 Static으로 선언하여 다른 오브젝트에서도 접근 가능
    public GameObject Player;  // 플레이어 변수

    public static DataManager Instance
    {
        get
        {
            if(null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    void Awake()
    {
        if(instance == null)
        {
            instance = this;

            // 씬이 전환되어도 유지되도록
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(instance);
        }
    }

    public void Save() {
        // 추후 저장 기능 등 제작
    }
}
