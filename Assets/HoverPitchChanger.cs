using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverPitchChanger : MonoBehaviour
{
    public AudioSource hoverSound;
    float startingPitch;
    public float lowSpeed;
    public float highSpeed;
    public int lowPitch;
    public int highPitch;
    
    // Start is called before the first frame update
    void Start()
    {
        startingPitch = hoverSound.pitch;
    }

    // Update is called once per frame
    void Update()
    {
        float speed = gameObject.GetComponent<Rigidbody2D>().velocity.magnitude;
        // float speedRatio = Mathf.Lerp(lowSpeed, highSpeed, speed);
        float newPitch = Mathf.Lerp((float) lowPitch, (float) highPitch, (speed - lowSpeed) / highSpeed);
        hoverSound.pitch = newPitch;
    }
}
