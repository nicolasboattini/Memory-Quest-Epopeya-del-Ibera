using UnityEngine;
using UnityEngine.UI;
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

    public string temp;

    public Sprite[] mySprites;
    public SpriteRenderer spriteRenderer;

    private bool pausado = false;
    private float tiempoPausa = 0f;
    private float tiempoInicio = 0f;
    private float tiempoActual = 0f;
    private int SegundosCronometro = 0;
    private TimeSpan tiempo;
    private bool inicioPresionado = false;
    
    public Text cronometro;
    public Button botonInicio;    

    void Start(){
		CambiarDificultad ();
        botonInicio.onClick.AddListener(ActivarCronometro);
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


	public void CambiarDificultad(){
		dificultad = (int) sliderDif.value*2;
		textoDificultad.text  = "Dificultad: " + dificultad ;
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

    public void ActualizarCronometro()
    {
        if (inicioPresionado && !pausado) // Comprobar si el cronómetro está en pausa y si el botón de inicio ha sido presionado
        {
            if (tiempoPausa != 0)
            { // Si el cronómetro ha estado en pausa
                SegundosCronometro += (int)(Time.time - tiempoPausa); // Sumar el tiempo que ha transcurrido desde la pausa
                tiempoPausa = 0; // Reiniciar la variable tiempoPausa
            }
            else
            {
                SegundosCronometro = (int)(Time.time - tiempoInicio); // Si no está en pausa, actualizar los segundos del cronómetro normalmente
            }
            tiempo = new TimeSpan(0, 0, SegundosCronometro);
            string temp = string.Format("{0}:{1:00}", //Convertir a formato MM:SS
                (int)tiempo.TotalMinutes,
                tiempo.Seconds);
            cronometro.text = temp;
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
        spriteRenderer.sprite = mySprites[i];
    }
    public void Change()
    {
        ChangeSprite(UnityEngine.Random.Range(0, mySprites.Length));

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
