using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Statistics : MonoBehaviour
{
    public float time;
    public float height;
    private float startTime;
    public TMP_Text heightText;
    public TMP_Text timeText;

    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
        startTime = Time.time;
        height = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        height = Math.Max(transform.position.y, height);
        time = Time.time - startTime;

        heightText.text = height.ToString("0.00");
        timeText.text = time.ToString("0.00");
    }

}
