using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	[SerializeField] private GameObject hud;
	[SerializeField] private GameObject prefabCarga;
	[SerializeField] private GameObject prefabMarcador;
	[SerializeField] private Dropdown opcoes;

	private InputField[] inputs;

	void Start(){
		inputs = hud.transform.GetComponentsInChildren<InputField>();
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
		
		obj = Instantiate(prefabCarga, hud.transform);
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
}
