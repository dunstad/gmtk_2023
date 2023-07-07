using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarMover : MonoBehaviour
{
    [SerializeField] private Health health;
    private float healthBarWidth;

    // Start is called before the first frame update
    void Start()
    {
        healthBarWidth = gameObject.GetComponent<RectTransform>().rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        float healthPercent = health.currentHealth <= 0 ? 0f : (float) health.currentHealth / health.maxHealth;
        gameObject.transform.localPosition = new Vector3(-healthBarWidth + (healthBarWidth * healthPercent), 0f, 0f);
    }
}
