using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngulacaoLinhaDeCampo : MonoBehaviour {

	public const float k = 8.987552f;
	
	// Update is called once per frame
	void FixedUpdate(){
		GameObject[] cargas = GameObject.FindGameObjectsWithTag ("Carga");
		if (cargas.Length == 0) {
			this.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
			return;
		}

		Vector2 campoRes = Vector2.zero;

		for (int i = 0; i < cargas.Length; i++) {
			Vector2 dist = cargas [i].transform.localPosition - this.transform.localPosition;
			Vector2 campo = (k * cargas [i].GetComponent<CargaEletrica> ().valorCarga /(dist.magnitude*dist.magnitude)) * dist.normalized;
			campoRes += campo;
		}
			
		this.transform.rotation = Quaternion.LookRotation (campoRes);
	}
}
