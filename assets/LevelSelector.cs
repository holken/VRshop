using UnityEngine;
using System.Collections;

public class LevelSelector : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void startnewLevel(string levelName)
    {
        Application.LoadLevel(levelName);
    }
}
