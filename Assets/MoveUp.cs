using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUp : MonoBehaviour
{
    public float speed;
    private Vector3 startpos;
    public Rigidbody2D rb;
    public float xTrackingReduction;

    // Start is called before the first frame update
    public void Start()
    {
        startpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        // Vector3 target = new Vector3(rb.position.x, transform.position.y + 1f, startpos.z);
        Vector3 target = new Vector3(transform.position.x, transform.position.y + 1f, startpos.z);
        Vector3 newPos = Vector2.MoveTowards(transform.position, target, Time.deltaTime * speed);
        newPos.x = transform.position.x + ( (rb.position.x - transform.position.x) / xTrackingReduction );
        // transform.position = newPos;
        gameObject.GetComponentInChildren<Rigidbody2D>().MovePosition(newPos);
    }

    public void Reset()
    {
        transform.position = startpos;
    }
}
