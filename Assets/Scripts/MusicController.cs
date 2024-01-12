using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour {
    public AudioSource musicSource;    
    public bool isMuted = false;
    public Sprite[] muteSwapAr;
    public Image muteRender;
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	public void MuteMusic () {
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
