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
    public float fovStep;
    
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
        float targetFov = Mathf.Lerp((float) lowFov, (float) highFov, speedRatio);
        //Debug.Log($"Speed Magnitude: {speed}, SpeedRatio {speedRatio}, NewCameraFov: {newFov}");
        float currentFov = PlayerCamera.m_Lens.FieldOfView;
        float updatedFov = currentFov;
        if(currentFov < targetFov - fovStep)
        {
            updatedFov = currentFov + fovStep;
            Debug.Log($"CurrentFov: {currentFov}, TargetFov: {targetFov}, UpdatedFov: {updatedFov}");
        }
        if(currentFov > targetFov + fovStep)
        {
            updatedFov = currentFov - fovStep;
            Debug.Log($"CurrentFov: {currentFov}, TargetFov: {targetFov}, UpdatedFov: {updatedFov}");
        }
        PlayerCamera.m_Lens.FieldOfView = updatedFov;
    }
}
