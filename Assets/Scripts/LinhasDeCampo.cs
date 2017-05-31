using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinhasDeCampo : MonoBehaviour {

	[SerializeField] private GameObject linhasDeCampo;
	[SerializeField] private int comprimentoX = 10;
	[SerializeField] private int comprimentoY = 10;
	[SerializeField] private float rangeX = 1f;
	[SerializeField] private float rangeY = 1f;

	void Start () {
		criarLinhasDeCampo ();
	}

	private void criarLinhasDeCampo(){
		GameObject obj;
		for (int i = -comprimentoX/2; i <= comprimentoX/2; i++)
			for (int j = -comprimentoY/2; j <= comprimentoY/2; j++) {
				obj = Instantiate (linhasDeCampo, this.transform);
				obj.transform.localPosition = new Vector2 (this.transform.localPosition.x + rangeX * i, this.transform.localPosition.y + rangeY * j);
			}
	
	}
}
