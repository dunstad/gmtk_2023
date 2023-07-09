using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Boost : MonoBehaviour
{
    [SerializeField] public int maxBoost;
    [NonSerialized] public int currentBoost;
    [SerializeField] public int chargeRate;
    [field: SerializeField]
	public Hoverboard Hoverboard { get; set; }
    public bool vulnerable;
    public UnityEvent onHurt;
    public UnityEvent onDeath;

    // Start is called before the first frame update
    void Start()
    {
        currentBoost = maxBoost;
        vulnerable = true;
    }

    public void Heal(int boost)
    {
        currentBoost += boost;
        if (currentBoost > maxBoost)
        {
            currentBoost = maxBoost;
        }
    }

    public void Hurt(int damage)
    {
        if (vulnerable)
        {
            currentBoost -= damage;
            onHurt.Invoke();
            if (currentBoost <= 0)
            {
                // Debug.Log("dead");
                onDeath.Invoke();
                // Destroy(gameObject);
            }
        }
    }

    void FixedUpdate()
    {
        if(Hoverboard.IsAttachedToSurface())
        {
            Heal(chargeRate);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
