using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	[SerializeField] private GameObject hud;
	[SerializeField] private GameObject prefabCarga;
	[SerializeField] private GameObject prefabMarcador;
	[SerializeField] private Dropdown opcoes;
	[SerializeField] private GameObject conjuntoCargas;
	[SerializeField] private GameObject conjuntoLinhasDeCampo;

	private InputField[] inputs;

	void Start(){
		inputs = hud.transform.GetComponentsInChildren<InputField>();
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape))
			sair ();
	}

	public void inserirCargas(){
		float x = 0f, y = 0f, valorCarga = 10;
		GameObject obj;
		try{
			x = float.Parse(inputs[0].text);
		}
		catch{
		}

		try{
			y = float.Parse(inputs[1].text);
		}
		catch{
		}

		try{
			valorCarga = float.Parse(inputs[2].text);
		}
		catch{
		}
		
		obj = Instantiate(prefabCarga, conjuntoCargas.transform);
		obj.transform.localPosition = new Vector3(x, y, 0);
		obj.transform.GetChild(1).GetComponent<Slider>().value = Mathf.Clamp(valorCarga, -100f, 100f);
	}

	public void marcarPosicao(){
		float x = 0f, y = 0f;
		GameObject obj;

		try{
			x = float.Parse(inputs[3].text);
		}
		catch{
		}

		try{
			y = float.Parse(inputs[4].text);
		}
		catch{
		}

		obj = Instantiate (prefabMarcador, hud.transform);
		obj.GetComponent<Pino> ().opcoes = opcoes;
		obj.transform.localPosition = new Vector3(x, y, 0);
		obj.transform.GetChild (0).GetComponent<Text> ().text = "(" + x.ToString () + ", " + y.ToString () + ")";

	}

	public void mostrarOcultarTextCargas(){
		GameObject textCarga;

		for (int i = 0; i < conjuntoCargas.transform.childCount; i++) {
			textCarga = conjuntoCargas.transform.GetChild (i).GetChild (0).gameObject;
			textCarga.SetActive (!textCarga.activeSelf);
		}
	}

	public void mostrarOcultarLinhasDeCampo(){
		conjuntoLinhasDeCampo.SetActive (!conjuntoLinhasDeCampo.activeSelf);
	
	}

	public void sair(){
		Application.Quit ();
	}

	public void criarLinhaDeCarga(){


	}

}
