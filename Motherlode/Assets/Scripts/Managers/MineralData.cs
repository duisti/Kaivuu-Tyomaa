using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineralData : MonoBehaviour {

	public string Name = "";
	
	[Tooltip("This is an approx. Not necessarily this many are spawned, and the amount spawned might be also a bit higher than stated (because of float rounding).")]
	public int AmoutOfObjects = 600;

	public float MinSpawnHeight = 5f;
	public float MaxSpawnHeight = 300f;

	public List<float> steps = new List<float>();
	[SerializeField]
	List<int> amountOfObjectsPerStep = new List<int>();
	int segments = 0;

	int currentStep = 0;
	int currentSubStep = 0;

	int maxCalcsPerStep = 10;


	float boundsX = 19f;
	//GENERATING
	public GameObject SpawnedGameObject;
	[Tooltip("In case we want to start the spread from a different list position. 5 without preinitialized list means we're at the middle point of steps.")]
	public int StartingStep = 0;
	int currentInt = 0;
	[SerializeField]
	bool finished = false;
	bool initialized = false;
	// Use this for initialization
	void Awake() {
		Init();
	}

	void Init() {
		if (steps.Count == 0) {
			steps.Add(0.01f);
			steps.Add(0.02f);
			steps.Add(0.03f);
			steps.Add(0.06f);
			steps.Add(0.15f);
			steps.Add(0.23f);
			steps.Add(0.23f);
			steps.Add(0.15f);
			steps.Add(0.06f);
			steps.Add(0.03f);
			steps.Add(0.02f);
			steps.Add(0.01f);
		}
		if (StartingStep != 0) {
			for (int i = 0; i < StartingStep; i++) {
				if (steps.Count != 0)
					steps.RemoveAt(0);
			}
		}
		if (steps.Count == 0) {
			print("Failed to initialize!");
			return;
		}
		segments = steps.Count;
		int c = steps.Count;
		for (int i = 0; i < c; i++) {
			float f = steps[i] * (float)AmoutOfObjects;
			f = Mathf.RoundToInt(f);
			amountOfObjectsPerStep.Add(Mathf.RoundToInt(f));
		}
		if (Name == "") {
			Name = gameObject.name;
		}
		print("First step: " + steps[0]);
		initialized = true;
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!initialized) return;
		if (finished) return;
		for (int i = 0; i < maxCalcsPerStep; i++) {
			if (finished) return;
			DoStep();
		}

	}

	void DoStep() {
		if (currentStep >= amountOfObjectsPerStep.Count) {
			finished = true;
			print("Finished spawning: " + Name + " (Total amount spawned: " + currentInt + ").");
			return;
		}
		if (currentSubStep >= amountOfObjectsPerStep[currentStep]) {
			currentStep++;
			currentSubStep = 0;
			return;
		}
		float stepLength = Mathf.Abs(MaxSpawnHeight - MinSpawnHeight) / amountOfObjectsPerStep.Count;
		float currentStepPos = stepLength * currentStep + MinSpawnHeight;
		Vector3 pos = randomPos(currentStepPos, stepLength);
		GameObject go = Instantiate(SpawnedGameObject, pos, Quaternion.identity, transform) as GameObject;
		go.name = Name + "(Mineral)";
		currentInt++;
		currentSubStep++;
	}

	Vector3 randomPos(float current, float length) {
		Vector3 p = new Vector3(Random.Range(-boundsX, boundsX), Mathf.Abs(Random.Range(current, current + length)) * -1f);
		return p;
	}
}
