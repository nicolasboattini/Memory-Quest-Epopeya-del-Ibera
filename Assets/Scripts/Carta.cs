using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Carta : MonoBehaviour {

    public int idCarta = 0;
    public float tiempoDelay = 2;
    public bool Mostrando;

	public GameObject  crearCartas;
	public GameObject  interfazUsuario;	
	
	public Texture2D texturaAnverso;
	public Button ActionBtn;
    public AudioClip m_flushSound1;
    public AudioClip m_flushSound2;
    public AudioSource flushSound;

    public Vector3  posicionOriginal;
	void Awake(){
		crearCartas = GameObject.Find ("Scripts");
		interfazUsuario = GameObject.Find ("Scripts");
		flushSound = GameObject.Find("Flushes").GetComponent<AudioSource>();
        GetComponent<RawImage>().texture = interfazUsuario.GetComponent<InterfazUsuario>().texturaReverso;

    }    
    public void Start(){
        InterfazUsuario interfazUsuario = crearCartas.GetComponent<InterfazUsuario>();
        if (interfazUsuario != null)        {
            interfazUsuario.AgregarCarta(this);
        }
        EsconderCarta (); //Se esconden las cartas y se empieza el juego		
		
	}
	public void OnClick (){
		Debug.Log("Haciendo click");
		if (!interfazUsuario.GetComponent<InterfazUsuario>().menuMostrado) {
			Debug.Log("Menu mostrado: " + !interfazUsuario.GetComponent<InterfazUsuario>().menuMostrado + " Entrando a mostarCarta");
			MostrarCarta ();
		}		
	}
	public void AsignarTextura(Texture2D  _textura){
		texturaAnverso  = _textura;
	}
	public void PlayFlush(bool minimi)
	{		
        if (flushSound.isPlaying) flushSound.Stop();
        flushSound.clip = minimi ? flushSound.clip = m_flushSound1 : flushSound.clip = m_flushSound2;
        flushSound.Play();
    }
	public void MostrarCarta(){
		if (ActionBtn.IsInteractable() && !Mostrando && crearCartas.GetComponent<CrearCartas>().sePuedeMostrar) {			
			Mostrando = true;
			GetComponent<RawImage> ().texture = texturaAnverso;
			PlayFlush(true);
			//Invoke ("EsconderCarta", tiempoDelay);
			crearCartas.GetComponent<CrearCartas> ().HacerClick (this); 
		}
	}
	public void EsconderCarta(){
		Invoke ("Esconder", tiempoDelay);
		crearCartas.GetComponent<CrearCartas> ().sePuedeMostrar = false;
	}
	void Esconder(){
        GetComponent<RawImage>().texture = interfazUsuario.GetComponent<InterfazUsuario>().texturaReverso; 
		Mostrando = false;
		crearCartas.GetComponent<CrearCartas> ().sePuedeMostrar = true;
		PlayFlush(false);
	}
	public void ForceFlip(bool flip)
	{
        GetComponent<RawImage>().texture = flip ? GetComponent<RawImage>().texture = texturaAnverso : GetComponent<RawImage>().texture = interfazUsuario.GetComponent<InterfazUsuario>().texturaReverso;
    }
}