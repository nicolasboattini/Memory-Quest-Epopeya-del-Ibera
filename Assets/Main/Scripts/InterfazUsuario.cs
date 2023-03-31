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

	public int SegundosCronometro;
	public Text cronometro;
	private TimeSpan tiempo;
	private string temp;

    public Sprite[] mySprites;
    public SpriteRenderer spriteRenderer;

    private bool pausado = false;
    private float tiempoPausa = 0;

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
        pausado = false;
		ActualizarCronometro ();
	}

	public void ReiniciarCronometro(){
		SegundosCronometro = 0;
		CancelInvoke ("ActualizarCronometro");
        pausado = false;
        tiempoPausa = 0;

	}

	public void ActualizarCronometro(){
        /*if (!pausado)
        {
            SegundosCronometro++;
            tiempo = new TimeSpan(0, 0, SegundosCronometro);
            //string temp = string.Format("{00}:{01}", (int)tiempo.TotalMinutes, tiempo.Seconds);
            string temp = string.Format("{0}:{1:00}", //Convertir a formato MM:SS
            (int)tiempo.TotalMinutes,
            tiempo.Seconds);
            cronometro.text = temp;
        }
        
		Invoke ("ActualizarCronometro", 1.0f);*/

        if (!pausado)
        { // Comprobar si el cronómetro está en pausa
            if (tiempoPausa != 0)
            { // Si el cronómetro ha estado en pausa
                SegundosCronometro += (int)(Time.time - tiempoPausa); // Sumar el tiempo que ha transcurrido desde la pausa
                tiempoPausa = 0; // Reiniciar la variable tiempoPausa
            }
            else
            {
                SegundosCronometro++; // Si no está en pausa, actualizar los segundos del cronómetro normalmente
            }
            tiempo = new TimeSpan(0, 0, SegundosCronometro);
            string temp = string.Format("{0}:{1:00}", //Convertir a formato MM:SS
                (int)tiempo.TotalMinutes,
                tiempo.Seconds);
            cronometro.text = temp;
        }
        Invoke("ActualizarCronometro", 1.0f);
    }

    public void PausarCronometro()
    {
        pausado = true;
        tiempoPausa = Time.time;
        CancelInvoke("ActualizarCronometro");
    }

    public void ChangeSprite(int i)
    {
        spriteRenderer.sprite = mySprites[i];
    }

    public void Change()
    {
        ChangeSprite(UnityEngine.Random.Range(0, mySprites.Length));

    }


}
