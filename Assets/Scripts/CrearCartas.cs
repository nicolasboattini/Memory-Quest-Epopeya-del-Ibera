using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrearCartas : MonoBehaviour {
    private List<GameObject> cartas = new List<GameObject>();
    public AudioClip m_correctSound = null;
    public AudioClip m_incorrectSound = null;
    public AudioSource resultSound;
    public bool carna;
    public bool hor;
    public bool sePuedeMostrar = true;
    public bool turbo = true;
    public Camera camara;
    public Carta CartaMostrada;
    public GameObject btnClosePanel;
    public GameObject CartaPrefab;
    public GameObject fondo;
    public GameObject tablero;
    public GameObject[] infoPanels;
    public int cols;
    public int contadorClicks = 1;
    public int nivel;
    public int numParejasEncontradas;
    public int rows;
    public InterfazUsuario interfazUsuario;
    public Text textoContadorIntentos;
    public Texture2D[] texturas;
    public Transform CartasParent;
    public void Reiniciar(){
        interfazUsuario.mostrandoCartasInicialmente = false;
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
		//nivel = 2;
        //nivel = 4;
        nivel = hor ? 2 : 4;
    }
    public void SetNivelMedio()
    {
        //nivel = 4;
        //nivel = 6;
        nivel = hor ? 4 : 6;
    }
    public void SetNivelDificil()
    {
        //nivel = 6;
        //nivel = 8;
        nivel = hor ? 6 : 8;
    }

    public void CrearParam() {

        switch (hor)
        {
            case true:
                //SWTICH HORIZONTAL
                switch (nivel)
                {
                    case 2:
                        Camera.main.transform.position = new Vector3(4.4f, 9.51753f, 2.7f);
                        rows = 2;
                        cols = 3;
                        break;
                    case 4:
                        rows = 4;
                        cols = 5;
                        Camera.main.transform.position = new Vector3(4.22f, 8.8f, 3.4f);
                        break;
                    case 6:
                        rows = 5;
                        cols = 6;
                        Camera.main.transform.position = new Vector3(3.36f, 6.68f, 2.61f);
                        break;
                }
                break;

            case false:
                //SWITCH VERTICAL                
                switch (nivel)
                {
                    case 4:
                        rows = 3;
                        cols = 2;
                        Camera.main.transform.position = new Vector3(1.0f, 6.30000019f, 2.52999997f);
                        break;

                    case 6:
                        rows = 6;
                        cols = 3;
                        if (carna)
                        {
                            Camera.main.transform.position = new Vector3(1.54f, 7.78000021f, 3.77000003f);
                        } else
                        {
                            Camera.main.transform.position = new Vector3(1.28999996f, 7.78000021f, 3.97000003f);
                        }
                        
                        break;

                    case 8:
                        rows = 6;
                        cols = 4;
                        Camera.main.transform.position = new Vector3(1.38999999f, 5.88000011f, 2.88000011f);
                        break;
                }
                        break;
                }                
                int cont = 0;
        for (int i = 0; i < rows; i++){
            for (int x = 0; x < cols; x++){
                float factor = 9.0f / nivel;
                float alter = (float)(carna ? 0.05 : -0.2); 
                Vector3 posicionTemp = new Vector3((float)(x * (factor + alter)), 0, (float)(i * (factor - 0.2)));
                GameObject cartaTemp = Instantiate(CartaPrefab, posicionTemp,
                Quaternion.Euler(new Vector3(0, 180, 0)));
                cartaTemp.transform.localScale *= factor;
                cartas.Add(cartaTemp);
                cartaTemp.GetComponent<Carta>().posicionOriginal = posicionTemp;                
                cartaTemp.transform.parent = CartasParent;
                cont++;
            }
        }
        AsignarTexturas();
        Barajar();		
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
        if(!interfazUsuario.mostrandoCartasInicialmente)
        {
            if (CartaMostrada == null)
            {
                CartaMostrada = _carta;
            }
            else
            {
                //contadorClicks++; Contador de Intentos
                //ActualizarUI (); 
                if (CompararCartas(_carta.gameObject, CartaMostrada.gameObject))
                {
                    print("Enhorabuena! Has encontrado una pareja!");
                    if (resultSound.isPlaying)
                    {
                        resultSound.Stop();
                    }
                    resultSound.clip = m_correctSound;
                    resultSound.Play();
                    numParejasEncontradas++;
                    if (numParejasEncontradas == cartas.Count / 2)
                    {
                        print("Enhorabuena! Has encontrado todas las parejas!");
                        interfazUsuario.MostrarMenuGanador();
                        cartas.Clear();
                        GameObject[] cartasEli = GameObject.FindGameObjectsWithTag("Carta");
                        for (int i = 0; i < cartasEli.Length; i++)
                        {
                            Destroy(cartasEli[i]);
                        }
                    }
                    else
                    {
                        //Mostrar info de carta y pausar
                        Debug.Log(_carta.GetComponent<Carta>().texturaAnverso.name);
                        string panelName = _carta.GetComponent<Carta>().texturaAnverso.name;
                        for (int i = 0; i < infoPanels.Length; i++)
                        {
                            if (infoPanels[i].name == panelName && turbo)
                            {
                                interfazUsuario.PausarCronometro();
                                infoPanels[i].gameObject.SetActive(true);
                                btnClosePanel.gameObject.SetActive(true);
                            }
                        }

                    }

                }
                else
                {
                    if (resultSound.isPlaying)
                    {
                        resultSound.Stop();
                    }
                    resultSound.clip = m_incorrectSound;
                    resultSound.Play();
                    _carta.EsconderCarta();
                    CartaMostrada.EsconderCarta();
                    contadorClicks++; //Contador de errores
                    ActualizarUI();
                    print("La Pareja No Coincide");
                }
                CartaMostrada = null;

            }
        }

        

	}
    public void TurboSwap()
    {
        turbo = !turbo;
    }


    public void CloseInfoPanel()
    {
        for (int i = 0;i < infoPanels.Length;i++)
        {
            if (infoPanels[i].activeSelf)
            {
                infoPanels[i].SetActive(false);
                btnClosePanel.SetActive(false);
                interfazUsuario.ActivarCronometro ();
            }
        }
    }

	public bool CompararCartas(GameObject carta1, GameObject carta2){
		bool resultado;
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

		
}
