using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;
using System;

public class InterfazUsuario : MonoBehaviour {
	public GameObject menu;
	public GameObject menuGanador;
	public Text textoMenuGanador;
	public Slider sliderDif;
	public Text textoDificultad;

	public bool menuMostrado;
	public bool menuMostradoGanador;
	public int dificultad;

	public int SegundosCronometro;
	public Text cronometro;
	private TimeSpan tiempo;
	private string temp;
	void Start(){
		CambiarDificultad ();
	}

	public void MostrarMenu(){
		menu.SetActive (true);
		menuMostrado = true;
	}

	public void EsconderMenu(){
		menu.SetActive (false);
		menuMostrado = false;
	}

	public void MostrarMenuGanador(){
		menuGanador.SetActive (true);
		menuMostradoGanador = true;
        string temp = string.Format("{0}:{1:00}", //Convertir a formato MM:SS
        (int)tiempo.TotalMinutes,
        tiempo.Seconds);
        textoMenuGanador.text = "" + temp;
	}

	public void EsconderMenuGanador(){
		menuGanador.SetActive (false);
		menuMostradoGanador = false;
	}


	public void CambiarDificultad(){
		dificultad = (int) sliderDif.value*2;
		textoDificultad.text  = "Dificultad: " + dificultad ;
	}


	public void ActivarCronometro(){
		ActualizarCronometro ();
	}

	public void ReiniciarCronometro(){
		SegundosCronometro = 0;
		CancelInvoke ("ActualizarCronometro");

	}

	public void PausarCronometro(){

	}

	public void ActualizarCronometro(){
		SegundosCronometro++;
		tiempo = new TimeSpan(0,0,  SegundosCronometro);
        //string temp = string.Format("{00}:{01}", (int)tiempo.TotalMinutes, tiempo.Seconds);
        string temp = string.Format("{0}:{1:00}", //Convertir a formato MM:SS
        (int)tiempo.TotalMinutes,
        tiempo.Seconds);
        cronometro.text = temp;
		Invoke ("ActualizarCronometro", 1.0f);
	}


}
