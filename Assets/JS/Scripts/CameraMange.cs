using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMange : MonoBehaviour
{
    [SerializeField] private List<CinemachineVirtualCamera> _cameras;

    public void DoShake(float time)
    {
        CinemachineBasicMultiChannelPerlin perlin =
            _cameras[0].GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_AmplitudeGain = 5;
        perlin.m_FrequencyGain = 5;
        Invoke("StopShake", time);
    }
    public void StopShake()
    {
        CinemachineBasicMultiChannelPerlin perlin =
            _cameras[0].GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_AmplitudeGain = 0;
        perlin.m_FrequencyGain = 0;
    }
    //public float shakeAmount = 3.0f;
    //public float shakeTime = 1.0f;

    //private void Start()
    //{
    //    StartCoroutine(Shake(shakeAmount, shakeTime));
    //}

    //IEnumerator Shake(float ShakeAmount, float ShakeTime)
    //{
    //    float timer = 0;
    //    while (timer <= ShakeTime)
    //    {
    //        Camera.main.transform.position =
    //            (Vector3)UnityEngine.Random.insideUnitCircle * ShakeAmount;
    //        timer += Time.deltaTime;
    //        yield return null;
    //    }
    //    Camera.main.transform.position = new Vector3(0f, 0f, 0f);
    //}
    //카메라 흔들기
    //public float ShakeAmount;
    //float ShakeTime;
    //Vector3 initialPosition;
    //public void VibrateForTime(float time)
    //{
    //    ShakeTime = time;
    //}

    //private void Update()
    //{
    //    initialPosition = GameObject.FindWithTag("MainCamera").transform.position;//카메라 흔들릴 위치값
    //    if (ShakeTime > 0)
    //    {
    //        transform.position = Random.insideUnitSphere * ShakeAmount + initialPosition;
    //        ShakeTime -= Time.deltaTime;
    //    }
    //    else
    //    {
    //        ShakeTime = 0.0f;
    //        transform.position = initialPosition;
    //    }
    //}
    //// 카메라 흔들기
    //public float ShakeAmount;
    ////public Canvas canvas;
    //float ShakeTime;
    //Vector3 initialPosition;
    //private void Start()
    //{
    //    initialPosition = new Vector3(0f, 0f, -5f);
    //}
    //private void Update()

    //{
    //    if (ShakeTime > 0)
    //    {
    //        transform.position = Random.insideUnitSphere * ShakeAmount + initialPosition;
    //        ShakeTime -= Time.deltaTime;
    //    }
    //    else
    //    {
    //        ShakeTime = 0.0f;

    //        transform.position = initialPosition;

    //        //canvas.renderMode = RenderMode.ScreenSpaceCamera;

    //    }
    //}
    //public void VibrateForTime(float time)
    //{
    //    ShakeTime = time;
    //    //canvas.renderMode = RenderMode.ScreenSpaceCamera;
    //    //canvas.renderMode = RenderMode.WorldSpace;

}
