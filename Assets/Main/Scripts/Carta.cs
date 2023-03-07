using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carta : MonoBehaviour {
	
	public int idCarta=0;
	public Vector3  posicionOriginal;
	public Texture2D texturaAnverso;
	public Texture2D  texturaReverso;

	public float tiempoDelay;
	public GameObject  crearCartas;
	public bool Mostrando;

	public GameObject  interfazUsuario;


	void Awake(){
		crearCartas = GameObject.Find ("Scripts");
		interfazUsuario= GameObject.Find ("Scripts");
	}

	void Start(){
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
		if (!Mostrando && crearCartas.GetComponent<CrearCartas>().sePuedeMostrar) {
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
