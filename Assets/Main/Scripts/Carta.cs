using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carta : MonoBehaviour {
	
	public int idCarta=0;
	public Vector3  posicionOriginal;
	public Texture2D texturaAnverso;
	public Texture2D  texturaReverso;

	public float tiempoDelay = 2;
	public GameObject  crearCartas;
	public bool Mostrando;

	public GameObject  interfazUsuario;
	private bool _interactiva = true;
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
        }
		EsconderCarta ();
	}


	void OnMouseDown(){
		if (!interfazUsuario.GetComponent<InterfazUsuario>().menuMostrado) {
			MostrarCarta ();
		}
	}

	public void AsignarTextura(Texture2D  _textura){
		texturaAnverso  = _textura;

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

}
