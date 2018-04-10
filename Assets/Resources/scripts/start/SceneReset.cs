using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneReset : MonoBehaviour {
    
	void Start () {
        SceneManager.sceneLoaded += (Scene scene, LoadSceneMode mode) =>
        {
            Time.timeScale = 1;
        };
	}
}
