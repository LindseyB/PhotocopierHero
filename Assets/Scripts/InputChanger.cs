using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InputChanger : MonoBehaviour {
	private Sprite[] keySprites;
	private const float DURATION = 3f;
	private float waitTime = 0;
	
	void Start() {
		keySprites = Resources.LoadAll<Sprite>("Keys");
	}

	void Update() {
		waitTime -= Time.deltaTime;

		if (waitTime <= 0) {
			waitTime = DURATION;
			int index = Random.Range(0, keySprites.Length);
			gameObject.GetComponent<Image>().sprite = keySprites[index];

			gameObject.GetComponent<RectTransform>().position = new Vector3 (Random.Range (50, 933),
		                                                                	 Random.Range (50, 445),
		                                                                     0);
		}

	}
}
