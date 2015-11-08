using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine("BlinkText");
	}

	void Update () {
		if(Input.GetKeyDown(KeyCode.Return)){
			Application.LoadLevel(1);
		}
	}


	IEnumerator BlinkText() {
		Text text = gameObject.GetComponent<Text>();
		while (true) {
			text.CrossFadeAlpha (0f, 0.5f, false);
			yield return new WaitForSeconds (1.0f);
			text.CrossFadeAlpha (1f, 0.5f, false);
			yield return new WaitForSeconds (1.0f);
		}
	}
}
