using UnityEngine;
using System.Collections;

public class Carta : MonoBehaviour {
	private bool _interactiva = true;

    public int idCarta = 0;
    public float tiempoDelay = 2;
    public bool Mostrando;
	public bool playable = false;    
    public bool Interactiva
    {
        get { return _interactiva; }
        set { _interactiva = value; }
    }    

	public GameObject  crearCartas;
	public GameObject  interfazUsuario;	
	public Texture2D  texturaReverso;
	public Texture2D texturaAnverso;

    public AudioClip m_flushSound1;
    public AudioClip m_flushSound2;
    public AudioSource flushSound;

    public Vector3  posicionOriginal;
	void Awake(){
		crearCartas = GameObject.Find ("Scripts");
		interfazUsuario = GameObject.Find ("Scripts");
		flushSound = GameObject.Find("Flushes").GetComponent<AudioSource>();

    }
	public void Start(){
        InterfazUsuario interfazUsuario = crearCartas.GetComponent<InterfazUsuario>();
        if (interfazUsuario != null)        {
            interfazUsuario.AgregarCarta(this);
        }
        EsconderCarta (); //Se esconden las cartas y se empieza el juego		
	}
	void OnMouseDown(){
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
		Debug.Log(flushSound.clip.name);
        flushSound.Play();
    }
	public void MostrarCarta(){
		if (Interactiva && !Mostrando && crearCartas.GetComponent<CrearCartas>().sePuedeMostrar) {			
			Mostrando = true;
			GetComponent<MeshRenderer> ().material.mainTexture = texturaAnverso;
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
		GetComponent<MeshRenderer> ().material.mainTexture  = texturaReverso; 
		Mostrando = false;
		crearCartas.GetComponent<CrearCartas> ().sePuedeMostrar = true;
		PlayFlush(false);
	}
    public void ForceMostrar()
    {		
        Debug.Log("Mostrando Carta Forzado");
        //Mostrando = true;
        GetComponent<MeshRenderer>().material.mainTexture = texturaAnverso;
        //Invoke ("EsconderCarta", tiempoDelay);
        //crearCartas.GetComponent<CrearCartas>().HacerClick(this);
        Debug.Log("Fin Mostrar Carta Forzado");
    }
    public void ForceEsconder()
	{
        GetComponent<MeshRenderer>().material.mainTexture = texturaReverso;
    }    
}