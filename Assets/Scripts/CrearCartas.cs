using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class CrearCartas : MonoBehaviour {    
    public bool carna;
    public bool hor;
    public bool sePuedeMostrar = true;
    public bool turbo;

    public int cols;
    public int contadorClicks = 1;
    public int nivel;
    public int numParejasEncontradas;
    public int rows;

    private List<GameObject> cartas = new List<GameObject>();
    public InterfazUsuario interfazUsuario;
    public Carta CartaMostrada;

    public AudioClip m_correctSound = null;
    public AudioClip m_incorrectSound = null;
    public AudioSource resultSound;
    
    public Camera camara;
    
    public GameObject btnClosePanel;
    public GameObject CartaPrefab;
    public GameObject fondo;
    public GameObject tablero;
    public GameObject[] infoPanels;
    
    
    public Text textoContadorIntentos;
    public Texture2D[] texturas;
    //public Transform CartasParent;
    public RectTransform CartasParent;
    public void Reiniciar(){
        interfazUsuario.cartas.Clear();
        interfazUsuario.cronometro.text = null;
        //interfazUsuario.mostrandoCartasInicialmente = false;
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
                        //Camera.main.transform.position = new Vector3(4.4f, 9.51753f, 2.7f);
                        rows = 2;
                        cols = 3;
                        break;
                    case 4:
                        rows = 4;
                        cols = 5;
                        //Camera.main.transform.position = new Vector3(4.22f, 8.8f, 3.4f);
                        break;
                    case 6:
                        rows = 5;
                        cols = 6;
                       // Camera.main.transform.position = new Vector3(3.36f, 6.68f, 2.61f);
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
                        CartasParent.GetComponent<GridLayoutGroup>().padding.top = 250;
                        CartasParent.GetComponent<GridLayoutGroup>().spacing = new Vector2(390, 600);
                        CartasParent.GetComponent<GridLayoutGroup>().cellSize = new Vector2(300, 300);
                        Debug.Log(Screen.height);
                        Debug.Log(Screen.currentResolution.height);
                        if (Screen.height < 2000)
                        {

                            Debug.Log("Entrando a res <2000");
                            CartasParent.GetComponent<GridLayoutGroup>().padding.top = 140;
                            CartasParent.GetComponent<GridLayoutGroup>().spacing = new Vector2(390, 510);
                        }
                        
                        //Camera.main.transform.position = new Vector3(1.0f, 6.30000019f, 2.52999997f);
                        break;

                    case 6:
                        rows = 6;
                        cols = 3;
                        
                            CartasParent.GetComponent<GridLayoutGroup>().padding.top = 70;
                            CartasParent.GetComponent<GridLayoutGroup>().cellSize = new Vector2(250, 250);
                            CartasParent.GetComponent<GridLayoutGroup>().spacing = carna ? new Vector2(220, 200) : new Vector2(220, 190);
                        if (Screen.height < 2000)
                        {

                            Debug.Log("Entrando a res <2000");
                            CartasParent.GetComponent<GridLayoutGroup>().padding.top = carna ? 110 : 10;
                            CartasParent.GetComponent<GridLayoutGroup>().spacing = carna ? new Vector2(285, 135) : new Vector2(220, 140);
                        }


                        break;

                    case 8:
                        rows = 6;
                        cols = 4;
                        CartasParent.GetComponent<GridLayoutGroup>().padding.top = 10;
                        CartasParent.GetComponent<GridLayoutGroup>().cellSize = new Vector2(275, 275);
                        CartasParent.GetComponent<GridLayoutGroup>().spacing = new Vector2(45, 175);
                        if (Screen.height < 2000)
                        {

                            Debug.Log("Entrando a res <2000");
                            CartasParent.GetComponent<GridLayoutGroup>().padding.top = 0;
                            CartasParent.GetComponent<GridLayoutGroup>().spacing = new Vector2(80, 120);
                        }

                        //Camera.main.transform.position = new Vector3(1.38999999f, 5.88000011f, 2.88000011f);
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
                //GameObject cartaTemp = Instantiate(CartaPrefab, posicionTemp, Quaternion.Euler(new Vector3(0, 180, 0)));
                GameObject cartaTemp = Instantiate(CartaPrefab, CartasParent);
                cartaTemp.transform.localScale *= factor;
                cartas.Add(cartaTemp);
                cartaTemp.GetComponent<Carta>().posicionOriginal = posicionTemp;                
                //cartaTemp.transform.parent = CartasParent;
                cont++;
            }
        }
        AsignarTexturas();
        Mezclar();		
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
	public void HacerClick(Carta _carta){
            if (CartaMostrada == null)
            {
                CartaMostrada = _carta;
            }
            else
            {
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
                        string panelName = _carta.GetComponent<Carta>().texturaAnverso.name;
                        for (int i = 0; i < infoPanels.Length; i++)
                        {
                            if (infoPanels[i].name == panelName && turbo)
                            {
                                foreach(Carta carta in interfazUsuario.cartas)
                                {
                                carta.ActionBtn.interactable = false;
                                }
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

        foreach (Carta carta in interfazUsuario.cartas)
        {
            carta.ActionBtn.interactable = true;
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
    public void Mezclar()
    {
        // Obtener todos los hijos del GridLayout
        List<Transform> children = new List<Transform>();
        foreach (Transform child in CartasParent)
        {
            children.Add(child);
        }

        // Reordenar aleatoriamente los hijos
        Shuffle(children);

        // Reorganizar los elementos en el GridLayout según el nuevo orden aleatorio
        foreach (Transform child in children)
        {
            child.SetSiblingIndex(Random.Range(0, CartasParent.childCount));
        }
    }

    // Función para reordenar aleatoriamente una lista
    private void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            (list[n], list[k]) = (list[k], list[n]);
        }
    }
}
