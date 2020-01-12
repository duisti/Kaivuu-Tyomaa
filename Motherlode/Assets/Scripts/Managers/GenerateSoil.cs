using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateSoil : MonoBehaviour {

	public float SegmentLength = -41.44f;
	public int Segments = 73;
	int currentSegment = 0;
	public GameObject SoilPrefab;
	GameObject soilParent;
	public List<GameObject> GeneratedSegments = new List<GameObject>();
	public List<GameObject> GeneratedWalls = new List<GameObject>();

	public GameObject WallPrefab;
	public float WallSegmentLength = -20.48f; //-10.48f
	float targetY;
	int currentWallSeg = 0;
	GameObject wall1;
	GameObject wall2;

	bool segmentsDone = false;
	bool finished = false;
	// Use this for initialization
	void Start () {
		soilParent = new GameObject("SoilParent");
		soilParent.transform.position = transform.position + new Vector3(0f, Mathf.Abs(SegmentLength) / 2f, 0f);
		soilParent.transform.rotation = Quaternion.identity;
		soilParent.transform.parent = transform;
		wall1 = transform.Find("Wall1").gameObject;
		wall2 = transform.Find("Wall2").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (finished) return;
		if (currentSegment < Segments) {
			for (int i = 0; i < 2; i++) {
				float slashed = SegmentLength / 2f;
				slashed = i == 0 ? slashed * -1f : slashed * 1f;
				Vector3 vect = new Vector3(slashed, soilParent.transform.position.y, 0f);
				GameObject go = Instantiate(SoilPrefab, new Vector3(0, SegmentLength * currentSegment, 0) + vect, Quaternion.identity, soilParent.transform) as GameObject;
				GeneratedSegments.Add(go);
			}
			currentSegment++;
		}
		else segmentsDone = true;
		if (segmentsDone) {
			targetY = Mathf.Abs(GeneratedSegments[GeneratedSegments.Count - 1].transform.position.y) + Mathf.Abs(SegmentLength) * 5f;
			float currentY = Mathf.Abs(WallSegmentLength) * currentWallSeg;
			if (currentY > targetY) {
				finished = true;
				return;
			}
			Instantiate(WallPrefab, new Vector3(wall1.transform.position.x, -currentY + wall1.transform.position.y), Quaternion.identity, wall1.transform);
			Instantiate(WallPrefab, new Vector3(wall2.transform.position.x, -currentY + wall1.transform.position.y), Quaternion.identity, wall2.transform);
			currentWallSeg++;
		}
	}

	public void ResetSegments() {
		finished = false;
		currentSegment = 0;
		currentWallSeg = 0;
		for (int i = 0; i < GeneratedSegments.Count; i++) {
			if (GeneratedSegments[i] != null) {
				Destroy(GeneratedSegments[i]);
			}
		}
		for (int i = 0; i < GeneratedWalls.Count; i++) {
			if (GeneratedWalls[i] != null) {
				Destroy(GeneratedWalls[i]);
			}
		}
		GeneratedSegments.Clear();
		GeneratedWalls.Clear();
	}
}
