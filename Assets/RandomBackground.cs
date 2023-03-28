using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBackground : MonoBehaviour {
    public Sprite[] mySprites;
    public SpriteRenderer spriteRenderer;    

    void ChangeSprite()
    {
        spriteRenderer.sprite = mySprites[0];
    }

    private void Start()
    {
        ChangeSprite();
    }
}
