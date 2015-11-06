using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InputChanger : MonoBehaviour {
	private Sprite[] keySprites;
	
	void Start() {
		keySprites = Resources.LoadAll<Sprite>("Keys");
	}

	void Update() {
		int index = Random.Range(0, keySprites.Length);
		gameObject.GetComponent<Image>().sprite = keySprites[index];

		int x = Random.Range (50, 933);
		int y = Random.Range (50, 445);

		gameObject.GetComponent<RectTransform>().position = new Vector3(x,
		                                                                y,
		                                                                0);

		Debug.Log ("x: " + x + " y: " + y);

	}
}
