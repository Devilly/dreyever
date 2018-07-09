using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _Continue : MonoBehaviour
{
    public string targetScene;

	void Start () {
        StartCoroutine(Do());
	}
	
	IEnumerator Do () {
		yield return new WaitForSeconds(1);

        SceneManager.LoadSceneAsync(targetScene);
	}
}
