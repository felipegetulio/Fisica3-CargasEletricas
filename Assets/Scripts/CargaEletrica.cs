using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CargaEletrica : MonoBehaviour {

	public float valorCarga = 1f;
	[SerializeField] private Text labelValor;
	[SerializeField] private Slider slider;
	[SerializeField] private GameObject setaForca;

	private Dropdown opcoes;
	private Vector3 screenPoint, offset;
	private bool possoSerDestruido;
	private float x, y;

	void Start(){
		opcoes = GameObject.Find ("OpcoesCarga").GetComponent<Dropdown>();
		x = setaForca.transform.localScale.x;
		y = setaForca.transform.localScale.y;
	}

	void FixedUpdate(){
		if (slider != null && labelValor != null) {
			valorCarga = slider.value;
			labelValor.text = valorCarga.ToString ("F2") + " uC\n";
		}

		GameObject[] cargas = GameObject.FindGameObjectsWithTag ("Carga");
		Vector2 dist = Vector2.zero;

		if (opcoes.value == 0) {
			Vector2 forcaRes = Vector2.zero, forca = Vector2.zero;

			for (int i = 0; i < cargas.Length; i++) {
				if (cargas [i].Equals (this.gameObject))
					continue;
				dist = 100*(this.transform.position - cargas [i].transform.position);
				if (dist.Equals (Vector2.zero))
					return;
				forca = (this.valorCarga * AngulacaoLinhaDeCampo.k * cargas [i].GetComponent<CargaEletrica> ().valorCarga / (dist.magnitude * dist.magnitude)) * dist.normalized;
				forcaRes += forca;
			}

			labelValor.text += forcaRes.ToString ("F2") + " mN";
			if (!forcaRes.Equals (Vector2.zero))
				setaForca.transform.rotation = Quaternion.LookRotation (forcaRes);

			setaForca.transform.localScale = new Vector3 (x, y, forcaRes.magnitude);
	
		} else {
			float potencialEletTotal = 0f, potencialElet, energiaPotencial;

			for (int i = 0; i < cargas.Length; i++) {
				if (cargas [i].Equals (this.gameObject))
					continue;
				dist = 100*(this.transform.position - cargas [i].transform.position);
				if (dist.Equals (Vector2.zero))
					return;
				potencialElet = AngulacaoLinhaDeCampo.k * cargas [i].GetComponent<CargaEletrica> ().valorCarga /dist.magnitude;
				potencialEletTotal += potencialElet;
			}

			energiaPotencial = potencialEletTotal * this.valorCarga;
			labelValor.text += energiaPotencial.ToString ("F2") + " mJ";
			setaForca.transform.localScale = new Vector3 (x, y, 0f);
		}


	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag("Lixeira"))
			possoSerDestruido = true;
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.CompareTag("Lixeira"))
			possoSerDestruido = false;
	}

	void OnMouseDown(){

		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		offset = this.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}

	void OnMouseDrag(){
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		this.transform.position = curPosition;
	}

	void OnMouseUp(){
		if (possoSerDestruido)
			Destroy (this.gameObject);
	}

}
