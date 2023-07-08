using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class DepthLayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SortSprites();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // set sprite render order based on Z axis position
    public void SortSprites() {
        Debug.Log("sprite order sorted");
        int defaultLayerValue = SortingLayer.GetLayerValueFromName("Default");
        
        SpriteRenderer[] sprites = FindObjectsOfType<SpriteRenderer>();
        SpriteRenderer[] sortedSprites = sprites.OrderBy((s) => s.gameObject.transform.position.z).ToArray();
        foreach (SpriteRenderer sprite in sprites) {
            float z = sprite.gameObject.transform.position.z;
            sprite.sortingOrder = (int) Math.Round(z * -100, 0);
        }

        UnityEngine.U2D.SpriteShapeRenderer[] sprites2 = FindObjectsOfType<UnityEngine.U2D.SpriteShapeRenderer>();
        UnityEngine.U2D.SpriteShapeRenderer[] sortedSprites2 = sprites2.OrderBy((s) => s.gameObject.transform.position.z).ToArray();
        foreach (UnityEngine.U2D.SpriteShapeRenderer sprite in sprites2) {
            float z = sprite.gameObject.transform.position.z;
            sprite.sortingOrder = (int) Math.Round(z * -100, 0);
        }
    }
}
