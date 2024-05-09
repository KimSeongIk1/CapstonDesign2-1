using System.Collections;
using System.Collections.Generic;
using TMPro;
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