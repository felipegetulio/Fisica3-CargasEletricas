using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pino : MonoBehaviour {

	public Dropdown opcoes;

	[SerializeField] private GameObject setaForca;
	[SerializeField] private Text valorOpcao;

	private float x, y;

	void Start(){
		x = setaForca.transform.localScale.x;
		y = setaForca.transform.localScale.y;
	}

	void Update () {
		GameObject[] cargas = GameObject.FindGameObjectsWithTag ("Carga");
		Vector2 dist = Vector2.zero;

		if (opcoes.value == 0) { //se é pra calcular o campo:
			Vector2 campoRes = Vector2.zero, campo;

			for (int i = 0; i < cargas.Length; i++) {
				dist = this.transform.localPosition - cargas [i].transform.localPosition;
				campo = 1000f * (AngulacaoLinhaDeCampo.k * cargas [i].GetComponent<CargaEletrica> ().valorCarga / (dist.magnitude * dist.magnitude)) * dist.normalized;
				campoRes += campo;
			}

			if (!campoRes.Equals (Vector2.zero))
				setaForca.transform.rotation = Quaternion.LookRotation (campoRes);

			setaForca.transform.localScale = new Vector3(x, y, campoRes.magnitude/20);
			valorOpcao.text = campoRes.ToString() + " N/C";

		} else if (opcoes.value == 1) { //se é pra caluclar o potencial elétrico
			float potRes = 0, pot;

			for (int i = 0; i < cargas.Length; i++) {
				dist = this.transform.localPosition - cargas[i].transform.localPosition;
				pot = AngulacaoLinhaDeCampo.k * cargas[i].GetComponent<CargaEletrica>().valorCarga/dist.magnitude;
				potRes += pot;
			}

			setaForca.transform.localScale = new Vector3(x, y, 0f);
			valorOpcao.text = potRes.ToString ("F2") + " KV";
		}


	}



}
