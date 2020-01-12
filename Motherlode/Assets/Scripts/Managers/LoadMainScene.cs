using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(LoadScene("MainGame"));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator LoadScene(string scenename) {
		yield return SceneManager.LoadSceneAsync(scenename, LoadSceneMode.Additive);
		yield return null;
		SceneManager.SetActiveScene(SceneManager.GetSceneByName(scenename));
	}
}
