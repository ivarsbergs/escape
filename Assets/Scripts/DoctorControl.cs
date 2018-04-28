﻿using System.Collections;
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
    private Vector3 SPAWNING_POS = new Vector3(0, -0.32f, 0);

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
        SpawnDoctor();
        InvokeRepeating("SpawnDoctor", 10f, 6f);
    }

    public void SpawnDoctor()
    {
        GameObject go = Instantiate(doctorPrefab, doctorContainer);
        go.transform.position = SPAWNING_POS;

        Doctor d = go.GetComponent<Doctor>();
        d.SetMovingSpeed(firstSpawned ? DEFAULT_RUN_SPEED : DEFAULT_WALK_SPEED);

        firstSpawned = true;
    }
}
