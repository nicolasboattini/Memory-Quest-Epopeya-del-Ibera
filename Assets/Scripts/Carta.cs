using UnityEngine;
using System.Collections;

public class Carta : MonoBehaviour {
	


	private bool _interactiva = true;
	public bool Mostrando;
	public bool playable = false;
	public float tiempoDelay = 2;
	public GameObject  crearCartas;
	public GameObject  interfazUsuario;
	public int idCarta=0;
	public Texture2D  texturaReverso;
	public Texture2D texturaAnverso;
	public Vector3  posicionOriginal;
    public bool Interactiva
    {
        get { return _interactiva; }
        set { _interactiva = value; }
    }


	void Awake(){
		crearCartas = GameObject.Find ("Scripts");
		interfazUsuario = GameObject.Find ("Scripts");
	}

	void Start(){
		InterfazUsuario interfazUsuario = crearCartas.GetComponent<InterfazUsuario>();
        if (interfazUsuario != null)
        {
            interfazUsuario.AgregarCarta(this);
            //Logica para Mostrar las cartas por un breve tiempo 
            Debug.Log("Por enrtar a delay");
            StartCoroutine(Delay());
        }
		
        //StartCoroutine(Delay()); //Delay para mostrar las cartas
        //EsconderCarta (); //Se esconden las cartas y se empieza el juego		
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
	public void ForceMostrarCarta()
	{
        Debug.Log("Mostrando Carta Forzado");
        Mostrando = true;
        GetComponent<MeshRenderer>().material.mainTexture = texturaAnverso;
        //Invoke ("EsconderCarta", tiempoDelay);
        crearCartas.GetComponent<CrearCartas>().HacerClick(this);
    }
	public void MostrarCarta(){
		if (Interactiva && !Mostrando && crearCartas.GetComponent<CrearCartas>().sePuedeMostrar) {			
			Mostrando = true;
			GetComponent<MeshRenderer> ().material.mainTexture = texturaAnverso;
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
	}
    IEnumerator Delay()
    {
        interfazUsuario.GetComponent<InterfazUsuario>().mostrandoCartasInicialmente = true;
        // Espera a que todas las cartas estén listas antes de mostrarlas
        yield return new WaitUntil(() => interfazUsuario.GetComponent<InterfazUsuario>().cartas.Count == interfazUsuario.GetComponent<InterfazUsuario>().crearCartas.nivel);

        // Mostrar todas las cartas simultáneamente
        foreach (Carta carta in interfazUsuario.GetComponent<InterfazUsuario>().cartas)
        {	
            carta.ForceMostrarCarta();
        }

        // Esconder las cartas después de un breve periodo de tiempo
        yield return new WaitForSeconds(tiempoDelay);
        foreach (Carta carta in interfazUsuario.GetComponent<InterfazUsuario>().cartas)
        {
            carta.EsconderCarta();
        }
        interfazUsuario.GetComponent<InterfazUsuario>().mostrandoCartasInicialmente = false;
    }


}
