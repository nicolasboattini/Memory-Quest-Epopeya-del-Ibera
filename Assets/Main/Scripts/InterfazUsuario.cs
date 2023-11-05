using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class InterfazUsuario : MonoBehaviour {
    public CrearCartas crearCartas;    
    public List<Carta> cartas;
	public GameObject menu;
	public GameObject menuGanador;
    public GameObject menuPerdedor;
	public Text textoMenuGanador;	
    public GameObject errorPanel;
    public bool errorShown = false;
	public bool menuMostrado;
	public bool menuMostradoGanador;
    public bool menuMostradoPerdedor;
    public Sprite[] fondos;
    public SpriteRenderer fondoRender;
    private bool pausado = false;
    private float tiempoPausa = 0f;
    private float tiempoInicio = 0f;   
    private int SegundosCronometro = 0;
    private TimeSpan tiempo;
    private bool inicioPresionado = false;    
    public Text cronometro;
    

    void Start(){		
        
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
        PausarCronometro();
	}
	public void EsconderMenuGanador(){
		menuGanador.SetActive (false);
		menuMostradoGanador = false;
	}
    public void MostrarMenuPerdedor(){
        foreach (Carta carta in cartas)
        {
        carta.Interactiva = false;
        }
		menuPerdedor.SetActive (true);
		menuMostradoPerdedor = true;        
        PausarCronometro();
	}
	public void EsconderMenuPerdedor(){
        foreach (Carta carta in cartas)
        {
        carta.Interactiva = true;
        }
		menuPerdedor.SetActive (false);
		menuMostradoPerdedor = false;
	}
    public void AgregarCarta(Carta carta) {
        if (!cartas.Contains(carta)) {
        cartas.Add(carta);
        }
    }       
    public void ActivarCronometro()
    {
        pausado = false;
        tiempoInicio = Time.time - SegundosCronometro; // Inicializar el tiempo de inicio del cronómetro
        
    }

    public void ReiniciarCronometro()
    {   
        SegundosCronometro = 0;
        pausado = false;
        tiempoPausa = 0;
        tiempoInicio = Time.time;
        
    }

    public void ActualizarCronometro(){
        if (inicioPresionado && !pausado){
            if (tiempoPausa != 0)
            {
                SegundosCronometro += (int)(Time.time - tiempoPausa);
                tiempoPausa = 0;
            } else {
                SegundosCronometro = (int)(Time.time - tiempoInicio);
            }
            int tempLevel = crearCartas.nivel;
            int tiempoRestante;
            int tiempoLimite;
            tiempo = new TimeSpan(0, 0, SegundosCronometro);            
            switch (tempLevel){
                case 2:
                    tiempoRestante = 30 - SegundosCronometro;
                    if (tiempoRestante <= 0)
                    {
                        tiempoRestante = 0;
                        MostrarMenuPerdedor();                           
                    }
                    break;
                case 4:
                    tiempoLimite = (1 * 60 + 15); // 1 minuto y 15 segundos
                    tiempoRestante = tiempoLimite - SegundosCronometro;
                    if (tiempoRestante <= 0)
                    {
                        tiempoRestante = 0;
                        MostrarMenuPerdedor();
                    }
                   break;
                case 6:
                    tiempoLimite = (1 * 60 + 45); // 1 minuto y 45 segundos
                    tiempoRestante = tiempoLimite - SegundosCronometro;
                    if (tiempoRestante <= 0)
                    {
                        tiempoRestante = 0;
                        MostrarMenuPerdedor();
                    }
                    break;
                default:
                    tiempoRestante = 0;
                    break;
            }
            // Convertir el tiempo restante a minutos y segundos
            int minutos = tiempoRestante / 60;
            int segundos = tiempoRestante % 60;
            string tiempoRestanteStr = string.Format("{0}:{1:00}", minutos, segundos);
            cronometro.text = tiempoRestanteStr;
        }
    }
    public void PausarCronometro()
    {
        if (inicioPresionado && !pausado) // Comprobar si el cronómetro está en pausa y si el botón de inicio ha sido presionado
        {
            pausado = true;
            tiempoPausa = Time.time;
        }
    }
    public void Inicio()
    {
        inicioPresionado = true; // Establecer que el botón de inicio ha sido presionado
        ReiniciarCronometro();
        ActivarCronometro();
    }
    void Update()
    {
        ActualizarCronometro();
    }
    public void ChangeSprite(int i)
    {
        fondoRender.sprite = fondos[i];
    }
    public void Change()
    {
        ChangeSprite(UnityEngine.Random.Range(0, fondos.Length));

    }

    public void swapErrorPanel()
    {
        errorShown = !errorShown;
        errorPanel.SetActive(errorShown);
        
    }

    public void GameExit()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
            Application.Quit();
    #endif
    }

}
