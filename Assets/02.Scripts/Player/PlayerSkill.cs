using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

// 작성자 : 김장후
public class PlayerSkill : MonoBehaviour
{
    public SkillData skillData; // 스킬 데이터(스크립터블 오브젝트) 받아오기

    private void Update() {
        if (skillData.skillFire == true && GetComponent<SpriteRenderer>().flipX == true) // 왼쪽을 바라보고 있으면
        {
            this.transform.Translate(Vector3.left * (skillData.skillSpeed * 0.01f)); // 왼쪽으로 스피드*0.01f만큼 이동
        }
        else if(skillData.skillFire == true && GetComponent<SpriteRenderer>().flipX == false) // 오른쪽을 바라보고 있으면
        {
            this.transform.Translate(Vector3.right * (skillData.skillSpeed * 0.01f));// 오른쪽으로 스피드*0.01f만큼 이동
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {   // 콜라이더2D에 감지되면

        if (collision.tag.Equals("Enemy") || collision.tag.Equals("Boss"))  // 태그값이 보스나 적일 경우
        {
            Damageable damageable = collision.GetComponent<Damageable>();   // 해당 적의 Damageable스크립트 접근

            if (damageable != null)
            {
                bool gotHit = damageable.Hit(skillData.damage, skillData.knockback);
                
                if (gotHit)
                {
                    Debug.Log(collision.name + " hit for " + skillData.damage);
                }
            }
        }
    }
}
