using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;
using System.Text.RegularExpressions;

public class InputChanger : MonoBehaviour {
	[SerializeField] GameObject missText;
	[SerializeField] GameObject smoke;
	[SerializeField] GameObject fire;

	private Sprite[] keySprites;
	private float duration = 3f;
	private const float MISS_DURATION = 1f;
	private float waitTime = 0;
	private int missCount = 0;
	private int hitCount = 0;

	private KeyCode key;


	bool first = true;

	void Start() {
		keySprites = Resources.LoadAll<Sprite>("Keys");
	}

	void Update() {
		waitTime -= Time.deltaTime;

		if (Input.GetKeyDown(key)){ 
			hitCount++;

			if(hitCount % 10 == 0){
				duration -= 0.5f;
			}
		}

		if (waitTime <= 0 || Input.GetKeyDown(key)) {
			if(waitTime <= 0 && !first){
				missCount++;
				StartCoroutine("displayMiss");

				if(missCount == 1){
					smoke.SetActive(true);
				} else if(missCount == 2){
					fire.SetActive(true);
				}
			}

			first = false;
			waitTime = duration;
			int index = Random.Range(0, keySprites.Length);

			Regex keyRegex = new Regex(".*Keyboard_White_(.*).png");
			Match match = keyRegex.Match(AssetDatabase.GetAssetPath(keySprites[index]));
			key = (KeyCode)System.Enum.Parse(typeof(KeyCode), match.Groups[1].Value);

			gameObject.GetComponent<Image>().sprite = keySprites[index];
			gameObject.GetComponent<Image>().CrossFadeAlpha(1,0,false);
			gameObject.GetComponent<RectTransform>().position = new Vector3 (Random.Range (50, 933),
		                                                                	 Random.Range (50, 445),
		                                                                     0);
			gameObject.GetComponent<Image>().CrossFadeAlpha(0, duration, false);
		}
	}

	IEnumerable displayMiss() {
		missText.SetActive(true);

		foreach (Text text in missText.GetComponentsInChildren<Text>()) {
			text.CrossFadeAlpha(1,0,false);
			text.CrossFadeAlpha(0, MISS_DURATION, false);
		}

		return null;
	}
}
