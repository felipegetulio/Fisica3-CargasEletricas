using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	[SerializeField] private GameObject conjuntoLinha, conjuntoCirculo;
	[SerializeField] private GameObject hud;
	[SerializeField] private GameObject prefabCarga;
	[SerializeField] private GameObject prefabMarcador;
	[SerializeField] private Dropdown opcoes;
	[SerializeField] private GameObject conjuntoCargasPinos;
	[SerializeField] private GameObject conjuntoLinhasDeCampo;
	[SerializeField] private InputField[] inputs;

	[SerializeField] private InputField linhaCargaTotal, linhaQntCargas, linhaComprimento;
	[SerializeField] private InputField circuloCargaTotal, circuloQntCargas, circuloRaio;

	private bool magnetudeForcasAtivada = false;
	private bool sliderAtivados = false;

	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape))
			sair ();
	}

	public void inserirCargas(){
		float x = 0f, y = 0f, valorCarga = 10;
		numeroValido (ref x, inputs [0].text);
		numeroValido (ref y, inputs [1].text);
		numeroValido (ref valorCarga, inputs [2].text);
		instanciarCarga (x, y, valorCarga, conjuntoCargasPinos.transform);
	}

	public void marcarPosicao(){
		float x = 0f, y = 0f;
		numeroValido (ref x, inputs [3].text);
		numeroValido (ref y, inputs [4].text);
		instanciarMarcacao (x, y);
	}

	public void mostrarOcultarTextCargas(){
		GameObject[] cargas = GameObject.FindGameObjectsWithTag("Carga");
		GameObject textCarga;
		magnetudeForcasAtivada ^= true;
		for (int i = 0; i < cargas.Length; i++){
			textCarga = cargas[i].transform.GetChild (0).gameObject;
			textCarga.SetActive (!textCarga.activeSelf);
		}
	}

	public void mostrarOcultarSliders(){
		GameObject[] cargas = GameObject.FindGameObjectsWithTag("Carga");
		GameObject slider;
		sliderAtivados ^= true;
		for (int i = 0; i < cargas.Length; i++){
			slider = cargas[i].transform.GetChild (1).gameObject;
			slider.SetActive (!slider.activeSelf);
		}
	}

	public void mostrarOcultarLinhasDeCampo(){
		conjuntoLinhasDeCampo.SetActive (!conjuntoLinhasDeCampo.activeSelf);
	}

	public void sair(){
		Application.Quit ();
	}

	public void criarLinhaDeCarga(){
		float cargaTotal=0, comprimento=0, x=0, espacoEntreCargas=0;
		int qntCargas=0;

		if(numeroValido(ref cargaTotal, linhaCargaTotal.text) && numeroValido(ref comprimento, linhaComprimento.text) && numeroValido(ref qntCargas, linhaQntCargas.text)){
			if(qntCargas == 1){
				instanciarCarga (0, 0, cargaTotal, conjuntoLinha.transform);
				return;
			}
			x = -comprimento/2;
			espacoEntreCargas = comprimento/(qntCargas-1);
			for(int i = 0; i<qntCargas; i++){
				instanciarCarga (x, 0, cargaTotal/qntCargas, conjuntoLinha.transform);
				x += espacoEntreCargas;
			}
		}
	}

	public void criarCirculoDeCarga(){
		float cargaTotal=0, raio=0, anguloEntreCargas=0, theta=0;
		int qntCargas=0;

		if(numeroValido(ref cargaTotal, circuloCargaTotal.text) && numeroValido(ref raio, circuloRaio.text) && numeroValido(ref qntCargas, circuloQntCargas.text)){
			if(qntCargas == 1){
				instanciarCarga (0, 0, cargaTotal, conjuntoLinha.transform);
				return;
			}
			anguloEntreCargas = 2*Mathf.PI/qntCargas;
			for(int i = 0; i<qntCargas; i++){
				instanciarCarga (raio*Mathf.Cos(theta), raio*Mathf.Sin(theta), cargaTotal/qntCargas, conjuntoCirculo.transform);
				theta += anguloEntreCargas;
			}
		}
	}


	private void instanciarCarga(float x, float y, float valorCarga, Transform pai){
		GameObject obj, textCarga, slider;
		obj = Instantiate(prefabCarga, pai);
		obj.transform.localPosition = new Vector2(x, y);
		obj.transform.GetChild(1).GetComponent<Slider>().value = Mathf.Clamp(valorCarga, -1000f, 1000f);
		textCarga = obj.transform.GetChild (0).gameObject;
		slider = obj.transform.GetChild (1).gameObject;
		textCarga.SetActive (magnetudeForcasAtivada);
		slider.SetActive (sliderAtivados);

	}

	private void instanciarMarcacao(float x, float y){
		GameObject obj;
		obj = Instantiate (prefabMarcador, conjuntoCargasPinos.transform);
		obj.GetComponent<Pino> ().opcoes = opcoes;
		obj.transform.localPosition = new Vector2(x, y);
		obj.transform.GetChild (0).GetComponent<Text> ().text = "(" + x.ToString () + ", " + y.ToString () + ")";
	}

	public void destruirCargasDeLinha(){
		for (int i = 0; i < conjuntoLinha.transform.childCount; i++)
			Destroy(conjuntoLinha.transform.GetChild(i).gameObject);
	}

	public void destruirCargasDeCirculo(){
		for (int i = 0; i < conjuntoCirculo.transform.childCount; i++)
			Destroy(conjuntoCirculo.transform.GetChild(i).gameObject);
	}

	bool numeroValido(ref float n, string s){
		try{
			n = float.Parse(s);
			return true;
		}
		catch{
			return false;
		}
	}

	bool numeroValido(ref int n, string s){
		try{
			n = int.Parse(s);
			return true;
		}
		catch{
			return false;
		}
	}

}
