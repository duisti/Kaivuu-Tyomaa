using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CameraFollowTarget : MonoBehaviour {
	float z = -10f;
	public Transform TrackedTarget;
	[HideInInspector]
	public NPCHealth playersHealth;
	[HideInInspector]
	public Upgrades playersUpgrades;
	[HideInInspector]
	public PlayerInventory playersInventory;
	[HideInInspector]
	public PlayerId playersId;

	Camera cam;
	public RectTransform AllScaler;

	public List<GameObject> ShopElements;
	public int CurrentOpenedShop = 100;
	//int IDs;
	//0 = Huoltoasema
	//1 = OreDump
	//2 = UpgradeStation
	//3 = Accessories
	//4 = MiniHuoltsikka
	//5 = MiniDump

	// Use this for initialization
	void Start () {
		Init();
	}
	public void Init() {
		if (TrackedTarget == null) return;
		playersHealth = TrackedTarget.GetComponent<NPCHealth>();
		playersUpgrades = TrackedTarget.GetComponent<Upgrades>();
		playersInventory = TrackedTarget.GetComponent<PlayerInventory>();
		playersId = TrackedTarget.GetComponent<PlayerId>();
		playersId.CameraScript = this;
		cam = GetComponent<Camera>();
	}
	void Update() {
		if (TrackedTarget == null) CurrentOpenedShop = 100;
		if (ShopElements.Count == 0) return;
		bool b = false;
		for (int i = 0; i < ShopElements.Count; i++) {
			if (i == CurrentOpenedShop) {
				ShopElements[i].SetActive(true);
			}
			else ShopElements[i].SetActive(false);
			if (ShopElements[i].activeSelf) {
				b = true;
			}
		}
		playersId.Shopping = b;
		float x = 1f - Mathf.Abs(cam.rect.x);
		float y = 1f - Mathf.Abs(cam.rect.y);
		AllScaler.localScale = new Vector3(Mathf.Min(x, y), Mathf.Min(x, y), AllScaler.localScale.z);
	}
	// Update is called once per frame
	void LateUpdate () {
		if (TrackedTarget != null) {
			Vector3 pos = new Vector3(TrackedTarget.transform.position.x, TrackedTarget.transform.position.y, z);
			transform.position = pos;
		}
	}

	public void SetTarget(Transform t) {
		TrackedTarget = t;
	}
}
