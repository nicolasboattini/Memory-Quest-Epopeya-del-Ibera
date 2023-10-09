﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrearCartas : MonoBehaviour {

    public InterfazUsuario interfazUsuario;
    private List<GameObject> cartas = new List<GameObject>();
    public GameObject CartaPrefab;
    public GameObject fondo;
    public GameObject tablero;
    public Carta CartaMostrada;
    public Transform CartasParent;
    public Camera camara;
    public Material[] materiales;
    public Texture2D[] texturas;    
    public int nivel;
    public int rows;
    public int cols;
    public int numParejasEncontradas;
    public int contadorClicks = 1;
    public bool sePuedeMostrar = true;
    public Text textoContadorIntentos;
    public AudioSource resultSound;
    public AudioClip m_correctSound = null;
    public AudioClip m_incorrectSound = null;

    public void Reiniciar(){

        if (nivel == 0)
        {
            print("Seleccione dificultad");
            interfazUsuario.swapErrorPanel();
        }
        else
        {            
            cartas.Clear();
            GameObject[] cartasEli = GameObject.FindGameObjectsWithTag("Carta");
            for (int i = 0; i < cartasEli.Length; i++)
            {
                Destroy(cartasEli[i]);
            }

            contadorClicks = 0;
            textoContadorIntentos.text = "";
            CartaMostrada = null;
            sePuedeMostrar = true;
            numParejasEncontradas = 0;
            interfazUsuario.ReiniciarCronometro();
            interfazUsuario.ActivarCronometro();
            interfazUsuario.Inicio();
            CrearParam();
        }
	}

    public void SetNivelFacil()
	{
		nivel = 4;
	}
    public void SetNivelMedio()
    {
        nivel = 6;
    }
    public void SetNivelDificil()
    {
        nivel = 8;
    }    
    public void CrearParam()
    {
            switch(nivel){
                case 4:
                    rows = 3;
                    cols = 2;
                    Camera.main.transform.position = new Vector3(1f, 7.61f, 3.01f);
                break;

                case 6:
                    rows= 6;
                    cols= 3;
                    Camera.main.transform.position = new Vector3(1.21f, 9.22f, 4.33f);
                break;

                case 8:
                    rows=6;
                    cols=4;
                    Camera.main.transform.position = new Vector3(1.34f, 6.71f, 3.1f);
                break;
            }
            int cont = 0;

            for (int i = 0; i < rows; i++)
            {
                for (int x = 0; x < cols; x++)
                {
                    float factor = 9.0f / nivel;
                    Vector3 posicionTemp = new Vector3((float)(x * (factor-0.2)), 0, (float)(i * (factor-0.2)));

                    GameObject cartaTemp = Instantiate(CartaPrefab, posicionTemp,
                        Quaternion.Euler(new Vector3(0, 180, 0)));

                    cartaTemp.transform.localScale *= factor;

                    cartas.Add(cartaTemp);

                    cartaTemp.GetComponent<Carta>().posicionOriginal = posicionTemp;
                    //cartaTemp.GetComponent<Carta> ().idCarta = cont;

                    cartaTemp.transform.parent = CartasParent;

                    cont++;
                }
            }
            AsignarTexturas();
            Barajar();
        //}
		
    }

    void AsignarTexturas(){

		int[] arrayTemp =new int[texturas.Length];

		for (int i = 0; i <= texturas.Length-1; i++) {
			arrayTemp [i] = i;
		}


		for (int t = 0; t < arrayTemp.Length; t++ )
		{
			int tmp = arrayTemp[t];
			int r = Random.Range(t, arrayTemp.Length);
			arrayTemp[t] = arrayTemp[r];
			arrayTemp[r] = tmp;
		}

		//int[] arrayDefinitivo = new int[ancho*ancho];
		int[] arrayDefinitivo = new int[(rows*cols)/2];

		for (int i = 0; i < arrayDefinitivo.Length ; i++) {
			arrayDefinitivo [i] = arrayTemp [i];
		}


		for(int i=0;i<cartas.Count ;i++){
			cartas[i].GetComponent<Carta> ().AsignarTextura (texturas[(arrayDefinitivo[i/2])] );
			cartas [i].GetComponent<Carta> ().idCarta = i / 2;
		}
	}

	void Barajar(){
		int aleatorio;

		for (int i = 0; i < cartas.Count; i++) {
			aleatorio = Random.Range (i, cartas.Count);

			cartas [i].transform.position = cartas [aleatorio].transform.position  ;
			cartas [aleatorio].transform.position  = cartas [i].GetComponent<Carta>().posicionOriginal;

			cartas [i].GetComponent<Carta> ().posicionOriginal = cartas [i].transform.position;
			cartas [aleatorio].GetComponent<Carta> ().posicionOriginal = cartas [aleatorio].transform.position;
		}
	}

	public void HacerClick(Carta _carta){
		if (CartaMostrada == null) {
			CartaMostrada = _carta;
		} else {
			//contadorClicks++; Contador de Intentos
			//ActualizarUI (); 
			if (CompararCartas (_carta.gameObject, CartaMostrada.gameObject)) {
				print ("Enhorabuena! Has encontrado una pareja!");
                if (resultSound.isPlaying)
                {
                    resultSound.Stop();
                }
                resultSound.clip = m_correctSound;
                resultSound.Play();
                numParejasEncontradas++;
				if (numParejasEncontradas == cartas.Count / 2) {
					print ("Enhorabuena! Has encontrado todas las parejas!");
					interfazUsuario.MostrarMenuGanador ();
				}

			} else {
				if (resultSound.isPlaying)
                {
                    resultSound.Stop();
                }
                resultSound.clip = m_incorrectSound;
                resultSound.Play();
				_carta.EsconderCarta ();
				CartaMostrada.EsconderCarta();
                contadorClicks++; //Contador de errores
                ActualizarUI();
                print("La Pareja No Coincide");
            }
			CartaMostrada = null;

		}

	}

	public bool CompararCartas(GameObject carta1, GameObject carta2){
		bool resultado;

//		if (carta1.GetComponent<MeshRenderer> ().material.mainTexture ==
//		    carta2.GetComponent<MeshRenderer> ().material.mainTexture) {
		if (carta1.GetComponent<Carta> ().idCarta  ==
			carta2.GetComponent<Carta> ().idCarta) {
			resultado = true;
		} else {
			resultado = false;
		}
		return resultado;
	}

	public void ActualizarUI(){
		textoContadorIntentos.text = "" + contadorClicks;
	}

		/*public void ProporcionarCartas(){
			if (numCartas % 2 == 0) {
				if (numCartas % 6 == 0) {
					ancho = 6;
					alto=numCartas / 6;
				}
	
				Crear ();
	
			} else {
				print ("Numero de cartas debe ser par");
			}
	
		}*/
}
