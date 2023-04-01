using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour {
    public AudioSource musicSource;
    public Button muteButton;
    public bool isMuted = false;
    public Sprite[] muteSwapAr;
    public Image muteRender;
    // Use this for initialization
    void Start () {
        // Agrega un listener al botón para llamar a la función MuteMusic() cuando se hace clic
        muteButton.onClick.AddListener(MuteMusic);
    }
	
	// Update is called once per frame
	void MuteMusic () {
        isMuted = !isMuted; // Invierte el valor de isMuted
        musicSource.mute = isMuted; // Cambia la propiedad mute del objeto AudioSource según el valor de isMuted
        muteSwap();
    }

    public void muteSwap()
    {        
        if (isMuted)
        {
            muteRender.sprite = muteSwapAr[1];
        }
        else
        {
            muteRender.sprite = muteSwapAr[0];
        }
    }
}
