using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUp : MonoBehaviour
{
    public float speed;
    private Vector3 startpos;

    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, transform.position + (Vector3.up * speed), Time.deltaTime);
    }

    public void Reset()
    {
        transform.position = startpos;
    }
}
