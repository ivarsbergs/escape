using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelControl : MonoBehaviour {

    private static EndLevelControl instance = null;

    public static EndLevelControl Instance
    {
        get
        {
            return instance;
        }
    }

    public List<Light> lights;
    public GameObject unicornDoor;

    private bool _doorOpened = false;
    private float _lerpTime = 0;

    private void Awake()
    {
        instance = this;
    }

    public void StartOpeningUnicornDoor()
    {
        _doorOpened = true;
        //unicornDoor.SetActive(false);
        unicornDoor.GetComponent<Animator>().Play("UnicornDoorAnimation");
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (_doorOpened)
        {
            Debug.Log(_lerpTime);
            foreach (Light l in lights)
            {
                l.intensity = Mathf.Lerp(0, 20, _lerpTime);
            }
            _lerpTime += Time.deltaTime / 30;
        }
    }
}
