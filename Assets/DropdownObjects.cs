using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropdownObjects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	[SerializeField] private bool isOpen;

	private RectTransform container;

	void Start () {
		container = this.transform.Find ("Container").GetComponent<RectTransform> ();
		isOpen = false;
	}

	void Update () {
		Vector3 scale = container.localScale;
		scale.y = Mathf.Lerp (scale.y, isOpen? 1:0, Time.deltaTime * 12);
		container.localScale = scale;
	}

	#region IPointerEnterHandler implementation

	public void OnPointerEnter (PointerEventData eventData){
		isOpen = true;
	}

	#endregion


	#region IPointerExitHandler implementation

	public void OnPointerExit (PointerEventData eventData){
		isOpen = false;
	}

	#endregion
}
