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
        startpos = gameObject.GetComponentInChildren<Rigidbody2D>().position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Rigidbody2D myRB = gameObject.GetComponentInChildren<Rigidbody2D>();
        // Vector3 target = new Vector3(rb.position.x, transform.position.y + 1f, startpos.z);
        Vector3 target = new Vector3(myRB.position.x, myRB.position.y + (1f * speed), startpos.z);
        Vector3 newPos = Vector2.MoveTowards(myRB.position, target, Time.deltaTime * speed);
        newPos.x = transform.position.x + ( (rb.position.x - myRB.position.x) / xTrackingReduction );
        // transform.position = newPos;
        myRB.MovePosition(newPos);
    }

    public void Reset()
    {
        transform.position = startpos;
    }
}
