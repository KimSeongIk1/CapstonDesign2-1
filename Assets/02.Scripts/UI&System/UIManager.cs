using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // 피해 텍스트 프리팹
    public GameObject damageTextPrefab;
    // 체력 텍스트 프리팹
    public GameObject healthTextPrefab;

    // 게임 캔버스
    public Canvas gameCanvas;

    private void Awake()
    {
        gameCanvas = FindObjectOfType<Canvas>();
    }

    private void OnEnable()
    {
        CharacterEvents.characterDamaged += CharacterTookDamage;
        CharacterEvents.characterHealed += CharacterHealed;
    }

    private void OnDisable()
    {
        CharacterEvents.characterDamaged -= CharacterTookDamage;
        CharacterEvents.characterHealed += CharacterHealed;
    }

    public void CharacterTookDamage(GameObject character, int damageReceived)
    {
        // 피해 텍스트 생성 위치 계산
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        // 피해 텍스트 프리팹 생성, 텍스트 컴포넌트 가져오기
        TMP_Text tmpText = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform)
                           .GetComponent<TMP_Text>();

        // 텍스트 내용 설정 (받은 피해량)
        tmpText.text = damageReceived.ToString();
    }

    public void CharacterHealed(GameObject character, int healthRestored)
    {
        // 체력 회복 텍스트 생성 위치 계산
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        // 체력 회복 텍스트 프리팹 생성, 텍스트 컴포넌트 가져오기
        TMP_Text tmpText = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform)
                           .GetComponent<TMP_Text>();

        // 텍스트 내용 설정 (회복한 체력량)
        tmpText.text = healthRestored.ToString();
    }


}
[CustomEditor(typeof(Damageable))]
public class DamageableTriggerEditor : Editor //Monobehaviour 대신 Editor를 넣습니다.
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        //ItemEffectTrigger.cs 의 객체를 받아옵니다 => 이래야 버튼시 명령을 내릴수 잇습니다
        Damageable damageable = (Damageable)target;

        EditorGUILayout.BeginHorizontal();  //BeginHorizontal() 이후 부터는 GUI 들이 가로로 생성됩니다.
        GUILayout.FlexibleSpace(); // 고정된 여백을 넣습니다. ( 버튼이 가운데 오기 위함)
                                   //버튼을 만듭니다 . GUILayout.Button("버튼이름" , 가로크기, 세로크기)

        if (GUILayout.Button("데미지 주기", GUILayout.Width(120), GUILayout.Height(30)))
        {

            //ItemEffectTrigger 클래스에서 버튼 누를시 해당 명령을 구현해줍니다.
            damageable.hitTest(50);
        }
        GUILayout.FlexibleSpace();  // 고정된 여백을 넣습니다.
        EditorGUILayout.EndHorizontal();  // 가로 생성 끝


    }
}
public class BossTriggerEditor : Editor //Monobehaviour 대신 Editor를 넣습니다.
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        //ItemEffectTrigger.cs 의 객체를 받아옵니다 => 이래야 버튼시 명령을 내릴수 잇습니다
        Boss_Controller boss_Controller = (Boss_Controller)target;

        EditorGUILayout.BeginHorizontal();  //BeginHorizontal() 이후 부터는 GUI 들이 가로로 생성됩니다.
        GUILayout.FlexibleSpace(); // 고정된 여백을 넣습니다. ( 버튼이 가운데 오기 위함)
                                   //버튼을 만듭니다 . GUILayout.Button("버튼이름" , 가로크기, 세로크기)

        if (GUILayout.Button("대쉬", GUILayout.Width(120), GUILayout.Height(30)))
        {

            //ItemEffectTrigger 클래스에서 버튼 누를시 해당 명령을 구현해줍니다.
            //boss_Controller.rush();
        }
        GUILayout.FlexibleSpace();  // 고정된 여백을 넣습니다.
        EditorGUILayout.EndHorizontal();  // 가로 생성 끝


    }
}
