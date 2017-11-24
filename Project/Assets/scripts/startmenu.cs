using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startmenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void StartApp()
    {
        Application.LoadLevel(1);
    }
    public void CloseApp()
    {
        Application.Quit();
    }
}