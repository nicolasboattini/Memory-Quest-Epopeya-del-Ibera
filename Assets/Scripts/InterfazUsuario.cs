using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InterfazUsuario : MonoBehaviour {
    private bool inicioPresionado = false;    
    private bool pausado = false;
    public bool errorShown = false;
    public bool hor;
    public bool menuMostrado;
    public bool menuMostradoGanador;
    public bool menuMostradoPerdedor;

    private float tiempoInicio = 0f;   
    private float tiempoPausa = 0f;
    public float timeDelay = 2;

    private int SegundosCronometro = 0;

    private TimeSpan tiempo;
    
    public CrearCartas crearCartas;    
    public GameObject errorPanel;
	public GameObject menu;
	public GameObject menuGanador;
    public GameObject menuPerdedor;
    public List<Carta> cartas;
    public Sprite[] fondos;
    public SpriteRenderer fondoRender;
    public Text cronometro;
	public Text textoMenuGanador;	
    void Start(){		
        
	}
    public void MostrarCartas()
    {
        StartCoroutine(mostrarTodasCartasRutina());
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
        cartas.Clear();
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
        cartas.Clear ();
	}    
    IEnumerator mostrarTodasCartasRutina()
    {
        PausarCronometro ();
        yield return new WaitForSeconds(1);
        cartas[1].PlayFlush(true);
        Debug.Log("Mostrando Cartas...");
        // Mostrar todas las cartas simultáneamente
        foreach (Carta carta in cartas)
        {
            carta.ForceMostrar();
            carta.Interactiva = false;
        }

        // Esconder las cartas después de un breve periodo de tiempo
        
        yield return new WaitForSeconds(timeDelay);
        cartas[1].PlayFlush(false);
        Debug.Log("Escondiendo cartas...");
        foreach (Carta carta in cartas)
        {
            carta.ForceEsconder();
            carta.Interactiva = true;
        }
        ActivarCronometro();
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

            switch (hor)
            {
                case true:
                    //SWITCH HORIZONTAL
                    switch (tempLevel)
                    {
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
                    break;

                case false:
                    //SWITCH VERTICAL                    
                    switch (tempLevel)
                    {
                        case 4:
                            tiempoRestante = 30 - SegundosCronometro;
                            if (tiempoRestante <= 0)
                            {
                                tiempoRestante = 0;
                                MostrarMenuPerdedor();
                            }
                            break;
                        case 6:
                            tiempoRestante = 45 - SegundosCronometro;
                            if (tiempoRestante <= 0)
                            {
                                tiempoRestante = 0;
                                MostrarMenuPerdedor();
                            }
                            break;
                        case 8:
                            tiempoLimite = (1 * 60 + 15); // 1 minuto y 15 segundos
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
        SceneManager.LoadScene("Menu");
        /*#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif*/
    }
    public void backMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}