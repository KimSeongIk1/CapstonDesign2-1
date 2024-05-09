using UnityEngine;
using UnityEngine.UI;

public class Boss_HP_Bar : MonoBehaviour
{
    public Image healthBarImage; // UI 이미지

    private Damageable bossDamageable; // Damageable 스크립트 참조

    void Start()
    {
        // 보스 오브젝트에서 Damageable 컴포넌트 찾기
        bossDamageable = GameObject.FindGameObjectWithTag("Boss").GetComponent<Damageable>();

        if (bossDamageable != null)
        {
            // 보스의 체력 변경 이벤트에 대한 구독 추가
            bossDamageable.healthChanged.AddListener(UpdateHealthBar);
        }
        else
        {
            Debug.LogError("보스의 Damageable 컴포넌트를 찾을 수 없습니다.");
        }

        // 체력바 초기화
        UpdateHealthBar(bossDamageable.Health, bossDamageable.MaxHealth);
    }

    // 체력바 업데이트 함수
    void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        if (maxHealth > 0)
        {
            float healthPercentage = (float)currentHealth / maxHealth;
            healthBarImage.fillAmount = healthPercentage;
        }
        else
        {
            Debug.LogWarning("최대 체력이 0보다 작거나 같습니다.");
        }
    }
}