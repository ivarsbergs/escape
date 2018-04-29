using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public Renderer fadeToColorRenderer;

    public bool gameEnded = false;
    public bool gameWon = false;
    private bool _doorOpened = false;
    private bool _startFading = false;
    private float _lerpLightingTime = 0;
    private float _lerpFadeToColorTime = 0;

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
        gameWon = true;
        gameEnded = true;
    }

    // Use this for initialization
    void Start () {
        PentagrammonManager.Instance.OnPentagramDrawn += StartOpeningUnicornDoor;

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

        if (_startFading)
        {
            Debug.Log(_lerpFadeToColorTime);

            Color c = (gameWon ? Color.white : Color.black);
            fadeToColorRenderer.material.color = new Color(c.r, c.g, c.b, Mathf.Lerp(0, 1, _lerpFadeToColorTime));
            _lerpFadeToColorTime += Time.deltaTime / 5;
        }
    }

    void StartMovingUnicorn()
    {
        unicornAnimator.Play("MoveUnicorn");
        Invoke("PlayUnicornSound", 5f);
    }

    void PlayUnicornSound()
    {
        SoundsControl.Instance.PlaySound(SoundsControl.Sounds.UNICORN);
    }

    public void StartFadeToWhite()
    {
        _startFading = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
