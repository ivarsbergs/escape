using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unicorn : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CallEnd()
    {
        Debug.Log("Call end");
        EndLevelControl.Instance.StartFadeToWhite();
    }
}
