using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 싱글톤 통합 관리 시스템(미완), DataManager와 둘 중 하나 사용 고려중
// 작성자 : 김장후
//유니티 프로젝트 에셋들은 런타임이 아닌 EditTime에 생성하여 동작한다.
#if UNITY_EDITOR // UnityEditor를 Using선언을 함으로써 Edit타임에도 사용되지만, 유니티 프로젝트에서 선언한 해당 스크립트 형태상 런타임에서도 사용이 되니 런타임에서도 빠질 수 있도록 전처리를 해주어야 한다.
using UnityEditor;
#endif

public class GlobalSetting : ScriptableObject
{
  /*  private const string SettingFileDirectory = "Assets/Resources";
    private const string SettingFilePath = "Assets/Resources/GlobalSetting.asset";

    private static GlobalSetting _instance;
    public static GlobalSetting Instance
    {
        get
        {
            if(_instance != null) // 싱글톤 인스턴스가 있다면 인스턴스 로드
            {
                return _instance;
            }
            _instance = Resources.Load<GlobalSetting>("GlobalSetting"); // 있다면 가져옴

            if (_instance == null)  // 여전히 인스턴스가 Null이라면
            {
                if (!AssetDatabase.IsValidFolder(SettingFileDirectory)) // 해당 경로에 에셋이 없었다는 뜻, 우선 해당 경로에 폴더가 있는 지 확인 AssetDataBase : 유니티 에디터 경로에 포함된 클래스이자 유니티 프로젝트 에셋들을 관리할 수 있음 
                {
                    AssetDatabase.CreateFolder();//
                }
            }
        }
    }*/

}
