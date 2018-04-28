using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorControl : MonoBehaviour
{
    private static DoctorControl instance = null;

    public static DoctorControl Instance
    {
        get
        {
            return instance;
        }
    }

    private const float DEFAULT_WALK_SPEED = 1.5f;
    private const float DEFAULT_RUN_SPEED = 2.8f;
    private Vector3 SPAWNING_POS = new Vector3(-4.7f, 2.83f, 1.93f);

    public GameObject doctorPrefab;
    public Transform doctorContainer;

    public Animator doorAnimator;

    private bool firstSpawned = false;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {
        StartSpawningDoctors();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void StartSpawningDoctors()
    {
        Invoke("SpawnDoctor", 2f);
    }

    public void SpawnDoctor()
    {
        GameObject go = Instantiate(doctorPrefab, doctorContainer);
        go.transform.localPosition = SPAWNING_POS;

        Doctor d = go.GetComponent<Doctor>();
        d.SetMovingSpeed(firstSpawned ? DEFAULT_RUN_SPEED : DEFAULT_WALK_SPEED);

        firstSpawned = true;
    }
}
