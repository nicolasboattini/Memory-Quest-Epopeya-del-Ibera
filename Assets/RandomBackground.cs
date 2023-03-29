using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomBackground : MonoBehaviour {
    public Sprite[] mySprites;
    public SpriteRenderer spriteRenderer;    	
	public void ChangeSprite(int i)
    {
        spriteRenderer.sprite = mySprites[i];
    }

	public void Change()
	{
		ChangeSprite(Random.Range(0, mySprites.Length));
		
	}

   }
