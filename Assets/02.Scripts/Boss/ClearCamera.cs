using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCamera : MonoBehaviour
{

    [SerializeField] private Transform targetPosition;
    [SerializeField] private float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] private bool isActive = false;


    private void Start()
    {
        isActive = true;
    }

    private void Update()
    {
        if (isActive)
        {
            Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, targetPosition.position, ref velocity, smoothTime);

            if (Vector3.Distance(targetPosition.position, Camera.main.transform.position) < 0.1f)
            {
                isActive = false;
            }
        }
    }
}
