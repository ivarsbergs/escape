using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BansheeGz.BGSpline.Components;
using BansheeGz.BGSpline.Curve;

public class Doctor : MonoBehaviour
{

    public const int POINTS_UNTIL_END = 4;
    public const int POINTS_UNTIL_DOOR_OPEN = 1;
    public const float STABBING_DISTANCE = 0.8f;

    public GameObject doctorObj;

    public List<Animator> animators;
    private int _pointCounter;

    public BGCcCursor pathCursor;
    public BGCurve pathCurve;
    public BGCcCursorChangeLinear patchCursorChangeLinear;

    public GameObject doctorBothArms;
    public GameObject doctorOneArm;

    public GameObject syringeObj;

    public AudioSource mouthAudioSource;
    public AudioSource armAudioSource;
    public AudioSource feetAudioSource;

    private bool isAlive = true;


    private bool _startedStabbing = false;

    // Use this for initialization
    void Start()
    {
        //test = GetComponent<BGCcCursor>();
        float interval;
        if (DoctorControl.Instance.doctorNumber == 1)
            interval = DoctorControl.WALK_SOUND_INTERVAL;
        else
            interval = DoctorControl.RUN_SOUND_INTERVAL;

        InvokeRepeating("PlayStepSound", 0.01f, interval);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_startedStabbing && Vector3.Distance(doctorObj.transform.position, pathCurve[POINTS_UNTIL_END].PositionWorld) < STABBING_DISTANCE)
        {
            _startedStabbing = true;
            StopMovingForward(true);

            foreach (Animator animator in animators)
                animator.SetBool("PlayerReached", true);
        }

        pathCurve[POINTS_UNTIL_END].PositionWorld = Camera.main.transform.position;

        //if (Input.GetKey(KeyCode.A))
        //{
        //    pathCurve[2].PositionWorld = new Vector3(pathCurve[2].PositionWorld.x - Time.deltaTime * 10, pathCurve[2].PositionWorld.y, pathCurve[2].PositionWorld.z);
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    pathCurve[2].PositionWorld = new Vector3(pathCurve[2].PositionWorld.x + Time.deltaTime * 10, pathCurve[2].PositionWorld.y, pathCurve[2].PositionWorld.z);
        //}
    }

    public void SetMovingSpeed(float speed)
    {
        patchCursorChangeLinear.Speed = speed;
        foreach (Animator animator in animators)
            animator.SetFloat("Speed", speed);
    }

    public void RipOffRand()
    {
        CancelInvoke("PlaySyringeStab");
        CancelInvoke("CallPlayerStabbed");
        isAlive = false;
        StopMovingForward(false);

        doctorBothArms.SetActive(false);
        doctorOneArm.SetActive(true);

        foreach (Animator animator in animators)
            animator.Play("Dead");

        SoundsControl.Instance.PlaySound(SoundsControl.Sounds.TEARING_ARM);
        SoundsControl.Instance.PlaySound(SoundsControl.Sounds.DOCTOR_YELL);

        DoctorControl.Instance.WaitForNextDispatcher();
    }

    public void StopMovingForward(bool keepLooking)
    {
        pathCursor.GetComponent<BGCcCursorObjectTranslate>().ObjectToManipulate = null;
        if (!keepLooking)
            pathCursor.GetComponent<BGCcCursorObjectRotate>().ObjectToManipulate = null;

        CancelInvoke("PlayStepSound");
        //foreach (Transform child in testMath.transform)
        //{
        //    GameObject.Destroy(child.gameObject);
        //}
        //Destroy(testMath);
    }

    public void ReachPoint()
    {
        if (!EndLevelControl.Instance.gameEnded && isAlive)
        {
            _pointCounter++;

            if (_pointCounter == POINTS_UNTIL_DOOR_OPEN)
            {
                Debug.Log("Open door");
                Invoke("CallOpenDoor", 3.4f - patchCursorChangeLinear.Speed);
                Invoke("CallOpenDoorSound", 2.2f - patchCursorChangeLinear.Speed);
            }

            if (_pointCounter >= POINTS_UNTIL_END)
            {
                foreach (Animator animator in animators)
                    animator.SetBool("PlayerReached", true);

                if (DoctorControl.Instance.doctorNumber == 1)
                {
                    SoundsControl.Instance.PlaySound(SoundsControl.Sounds.DOCTOR_TALK);
                }
                else
                {
                    syringeObj.SetActive(true);
                    Invoke("PlaySyringeStab", 0.4f);
                    Invoke("CallPlayerStabbed", 0.4f);
                }

            }
        }
    }

    public void PlaySyringeStab()
    {
        SoundsControl.Instance.PlaySound(SoundsControl.Sounds.SYRINGE_STAB);
    }

    public void CallOpenDoorSound()
    {
        if (!EndLevelControl.Instance.gameEnded)
        {
            SoundsControl.Instance.PlaySound(SoundsControl.Sounds.DOOR);
        }
    }

    public void CallOpenDoor()
    {
        if (!EndLevelControl.Instance.gameEnded)
        {
            DoctorControl.Instance.doorAnimator.Play("OpenDoor");
        }
    }

    public void CallPlayerStabbed()
    {
        if (!EndLevelControl.Instance.gameEnded && isAlive)
        {
            EndLevelControl.Instance.gameWon = false;
            EndLevelControl.Instance.StartFadeToWhite();
            Debug.Log("STAB!");
            Invoke("CallRestart", 2f);
        }
    }

    void CallRestart()
    {
        EndLevelControl.Instance.RestartGame();
    }

    public void PlayStepSound()
    {
        SoundsControl.Instance.PlaySound(SoundsControl.Sounds.FOOTSTEP);
    }
}
