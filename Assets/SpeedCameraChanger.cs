using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SpeedCameraChanger : MonoBehaviour
{
    [field: SerializeField]
    public CinemachineVirtualCamera PlayerCamera { get; set; }
    [field: SerializeField]
    public CharacterController2D Character { get; set; }
    float startingFov;
    public float lowSpeed;
    public float highSpeed;
    public float lowFov;
    public float highFov;
    
    // Start is called before the first frame update
    void Start()
    {
        startingFov = PlayerCamera.m_Lens.FieldOfView;
        Debug.Log($"StartingFov = {startingFov}");
        lowFov = startingFov;
        lowSpeed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float speed = gameObject.GetComponent<Rigidbody2D>().velocity.magnitude;
        float speedRatio = speed/highSpeed;
        float newFov = Mathf.Lerp((float) lowFov, (float) highFov, speedRatio);
        Debug.Log($"Speed Magnitude: {speed}, SpeedRatio {speedRatio}, NewCameraFov: {newFov}");
    }
}
