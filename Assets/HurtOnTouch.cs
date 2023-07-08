using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtOnTouch : MonoBehaviour
{
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Health health = other.GetComponent<Health>();
        if (health)
        {
            health.Hurt(damage);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("hurt collision");
        Health health = other.gameObject.GetComponent<Health>();
        if (health)
        {
            health.Hurt(damage);
        }
    }
}
