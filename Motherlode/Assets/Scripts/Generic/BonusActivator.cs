using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusActivator : MonoBehaviour {

	public List<GameObject> TiberiumQuest;
	public List<GameObject> TacitusQuest;
	public List<GameObject> Bonuses;
	[HideInInspector]
	public List<float> Steps = new List<float>();
	// Use this for initialization
	void Start () {
		GameMaster.instance.Bonus = this;
		Steps.Add(500f);
		Steps.Add(700f);
		Steps.Add(1500f);
		Steps.Add(3000f);
		Steps.Add(5000f);
		Steps.Add(6100f);
		Steps.Add(7000f);
		Steps.Add(7400f);
		Steps.Add(7630f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ActivateBonus(int i) {
		if (Bonuses[i] == null) return;
		Bonuses[i].SetActive(true);
		Bonuses[i] = null;
	}
}
