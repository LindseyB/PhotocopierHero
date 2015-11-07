using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;
using System.Text.RegularExpressions;

public class InputChanger : MonoBehaviour {
	[SerializeField] GameObject missText;
	[SerializeField] GameObject smoke;
	[SerializeField] GameObject fire;
	[SerializeField] GameObject explodeyBits;
	[SerializeField] GameObject copier;

	private Sprite[] keySprites;
	private float duration = 3f;
	private const float MISS_DURATION = 1f;
	private float waitTime = 0;
	private int missCount = 0;
	private int hitCount = 0;

	public float radius = 10.0F;
	public float power = 10.0F;

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
				} else if(missCount == 3){
					Explode();
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

	void Explode() {
		copier.SetActive (false);
		smoke.SetActive(false);
		fire.SetActive(false);

		float mult1, mult2, mult3;

		explodeyBits.SetActive(true);
		foreach (Rigidbody rb in GameObject.FindObjectsOfType<Rigidbody>()) {
			// get signs randomly
			mult1 = Random.value > 0.5f ? -1 : 1;
			mult2 = Random.value > 0.5f ? -1 : 1;
			mult3 = Random.value > 0.5f ? -1 : 1;

			rb.velocity = new Vector3(mult1*1000,mult2*1000,mult3*1000);
		}
	}
}
