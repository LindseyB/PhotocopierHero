using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;
using System.Text.RegularExpressions;

public class InputChanger : MonoBehaviour {
	private Sprite[] keySprites;
	private const float DURATION = 3f;
	private float waitTime = 0;
	private KeyCode key;

	void Start() {
		keySprites = Resources.LoadAll<Sprite>("Keys");
	}

	void Update() {
		waitTime -= Time.deltaTime;

		if (waitTime <= 0 || Input.GetKeyDown(key)) {
			waitTime = DURATION;
			int index = Random.Range(0, keySprites.Length);

			Regex keyRegex = new Regex(".*Keyboard_White_(.*).png");
			Match match = keyRegex.Match(AssetDatabase.GetAssetPath(keySprites[index]));
			key = (KeyCode)System.Enum.Parse(typeof(KeyCode), match.Groups[1].Value);

			gameObject.GetComponent<Image>().sprite = keySprites[index];
			gameObject.GetComponent<Image>().CrossFadeAlpha(1,0,false);
			gameObject.GetComponent<RectTransform>().position = new Vector3 (Random.Range (50, 933),
		                                                                	 Random.Range (50, 445),
		                                                                     0);
			gameObject.GetComponent<Image>().CrossFadeAlpha(0, DURATION, false);
		}

	}
}
