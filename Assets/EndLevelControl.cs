using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Animator unicornAnimator;

    public CanvasGroup fadeToWhite;

    private bool _doorOpened = false;
    private bool _unicornArrived = false;
    private float _lerpLightingTime = 0;
    private float _lerpFadeToWhiteTime = 0;

    private void Awake()
    {
        instance = this;
    }

    public void StartOpeningUnicornDoor()
    {
        _doorOpened = true;
        //unicornDoor.SetActive(false);
        unicornDoor.GetComponent<Animator>().Play("UnicornDoorAnimation");

        StartMovingUnicorn();
    }

    // Use this for initialization
    void Start () {
        PentagramRaycaster.Instance.OnPentagramDrawn += StartOpeningUnicornDoor;

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (_doorOpened)
        {
            foreach (Light l in lights)
            {
                l.intensity = Mathf.Lerp(0, 20, _lerpLightingTime);
            }
            _lerpLightingTime += Time.deltaTime / 25;
        }

        if (_unicornArrived)
        {
            Debug.Log(_lerpFadeToWhiteTime);

            fadeToWhite.alpha = Mathf.Lerp(0, 1, _lerpFadeToWhiteTime);
            _lerpFadeToWhiteTime += Time.deltaTime / 3;
        }
    }

    void StartMovingUnicorn()
    {
        unicornAnimator.Play("MoveUnicorn");
    }

    public void StartFadeToWhite()
    {
        _unicornArrived = true;
    }
}
