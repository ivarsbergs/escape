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

    public static float DEFAULT_WALK_SPEED = 1.5f;
    public static float DEFAULT_RUN_SPEED = 2.8f;

    public static float WALK_SOUND_INTERVAL = 0.42f;
    public static float RUN_SOUND_INTERVAL = 0.4f;
    private Vector3 SPAWNING_POS = new Vector3(-4.7f, 2.83f, 1.93f);

    public GameObject doctorPrefab;
    public Transform doctorContainer;

    public Animator doorAnimator;

    private bool firstSpawned = false;

    public int doctorNumber = 0;
    public Doctor currentDoctor;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        CallNextSpawn();
        SoundsControl.Instance.PlaySound(SoundsControl.Sounds.SPEAKER_1);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void WaitForNextDispatcher()
    {
        if (DoctorControl.Instance.doctorNumber == 1)
        {
            Invoke("CallNextDispatcher", 3.5f);
        }
        else if (DoctorControl.Instance.doctorNumber == 2)
        {
            Invoke("CallNextDispatcher", 3.5f);
        }
        else
        {
            CallNextSpawn();
        }
    }

    public void CallNextDispatcher()
    {

        if (DoctorControl.Instance.doctorNumber == 1)
        {
            SoundsControl.Instance.PlaySound(SoundsControl.Sounds.SPEAKER_2);
            Invoke("SpawnDoctor", 9f);
        }
        else if (DoctorControl.Instance.doctorNumber == 2)
        {
            SoundsControl.Instance.PlaySound(SoundsControl.Sounds.SPEAKER_3);
            Invoke("SpawnDoctor", 6f);
        }

    }

    public void CallNextSpawn()
    {
        Invoke("SpawnDoctor", 5f);
    }

    public void SpawnDoctor()
    {
        GameObject go = Instantiate(doctorPrefab, doctorContainer);
        go.transform.localPosition = SPAWNING_POS;

        if (!firstSpawned && InfoArrow.Instance != null)
        {
            InfoArrow.Instance.transform.localPosition = new Vector3(4, 4, 5);
            InfoArrow.Instance.Target = go.transform;
            InfoArrow.Instance.Enable(true);
        }


        Doctor d = go.GetComponent<Doctor>();
        d.SetMovingSpeed(firstSpawned ? DEFAULT_RUN_SPEED : DEFAULT_WALK_SPEED);
        currentDoctor = d;

        doctorNumber++;
        firstSpawned = true;
    }

    public void EndGame()
    {
        if (!ReferenceEquals(currentDoctor, null))
        {
            currentDoctor.StopMovingForward(false);
        }
    }
}
