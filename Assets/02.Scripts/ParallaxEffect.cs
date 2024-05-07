using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour// parallax = 시차
{

    // 카메라
    public Camera cam;
    // 추적 대상 변형 (캐릭터 등)
    public Transform followTarget;

    // 시작 위치 (파랄랙스 게임 오브젝트)
    Vector2 startingPosition;
    // 시작 Z 값 (파랄랙스 게임 오브젝트)
    float startingZ;

    // 카메라가 파랄랙스 게임 오브젝트의 시작 위치에서 얼마나 이동했는지
    Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startingPosition;

    // 타겟과의 Z 거리
    float zDistanceFromTarget => transform.position.z - followTarget.transform.position.z;

    // 오브젝트가 타겟 앞에 있으면 near clip plane 사용, 뒤에 있으면 farClipPlane 사용
    float clippingPlane => (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));

    // 파랄랙스 효과 속도 계산 (플레이어와의 거리, 클리핑 평면 고려)
    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        // 타겟이 이동하면 파랄랙스 게임 오브젝트도 같은 거리만큼 이동 (계수 적용)
        Vector2 newPosition = startingPosition + camMoveSinceStart * parallaxFactor;

        // X/Y 위치는 타겟 이동 속도 * 파랄랙스 계수, Z는 시작 Z 값 유지
        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}
