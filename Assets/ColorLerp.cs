using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLerp : MonoBehaviour
{
    public Color startColor;
    public Color endColor;
    IEnumerator animate;

    // Start is called before the first frame update
    void Start()
    {
        animate = Animate();
        StartCoroutine(animate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Animate()
    {
        // while (true) {
        //     transform.Rotate(new Vector3(0, 0, Mathf.Sin(Time.time) / 2));
        //     var newScale = originalScale * ((Mathf.Sin(Time.time) / 2) + 1.25f);
        //     transform.localScale = new Vector3(newScale, newScale, newScale);
        //     yield return null;
        // }
        while (true) {
            var lerpedColor = Color.Lerp(startColor, endColor, (Mathf.Sin( Time.time / 2 ) + 1) / 2);
            gameObject.GetComponent<UnityEngine.U2D.SpriteShapeRenderer>().color = lerpedColor;
            yield return null;
        }
    }

    // public IEnumerator ToGreen()
    // {
    //     var startTime = Time.time;
    //     var red = new Color(.925f, .364f, .505f);
    //     var green = new Color(.549f, .890f, .341f);
    //     while (Time.time - startTime < 1)
    //     {
    //         var lerpedColor = Color.Lerp(red, green, Time.time - startTime);
    //         bodySprite.color = lerpedColor;
    //         yield return null;
    //     }
    //     bodySprite.color = green;
    // }
}
