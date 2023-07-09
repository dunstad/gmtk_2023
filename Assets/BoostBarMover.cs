using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostBarMover : MonoBehaviour
{
    [SerializeField] private Boost boost;
    private float boostBarWidth;

    // Start is called before the first frame update
    void Start()
    {
        boostBarWidth = gameObject.GetComponent<RectTransform>().rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        float boostPercent = boost.currentBoost <= 0 ? 0f : (float) boost.currentBoost / boost.maxBoost;
        gameObject.transform.localPosition = new Vector3(-boostBarWidth + (boostBarWidth * boostPercent), 0f, 0f);
    }
}
