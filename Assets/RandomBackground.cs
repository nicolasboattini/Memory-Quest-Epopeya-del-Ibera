using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomBackground : MonoBehaviour {
    public Sprite[] mySprites;
    public SpriteRenderer spriteRenderer;    
	public int alter;
	void ChangeSprite(int i)
    {
        spriteRenderer.sprite = mySprites[i];
    }

	void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			alter = Random.Range (0, 9);
			ChangeSprite(alter);
		}
	}

   }
