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

    public AudioSource mouthAudioSource;
    public AudioSource armAudioSource;


    private bool _startedStabbing = false;

    // Use this for initialization
    void Start()
    {
        //test = GetComponent<BGCcCursor>();
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
        CancelInvoke("CallPlayerStabbed");
        StopMovingForward(false);
        foreach (Animator animator in animators)
            animator.Play("Dead");

        SoundsControl.Instance.PlaySound(SoundsControl.Sounds.TEARING_ARM);
        SoundsControl.Instance.PlaySound(SoundsControl.Sounds.DOCTOR_YELL);

        DoctorControl.Instance.SpawnDoctor();
    }

    public void StopMovingForward(bool keepLooking)
    {
        pathCursor.GetComponent<BGCcCursorObjectTranslate>().ObjectToManipulate = null;
        if (!keepLooking)
            pathCursor.GetComponent<BGCcCursorObjectRotate>().ObjectToManipulate = null;
        //foreach (Transform child in testMath.transform)
        //{
        //    GameObject.Destroy(child.gameObject);
        //}
        //Destroy(testMath);
    }

    public void ReachPoint()
    {
        if (!EndLevelControl.Instance.gameEnded)
        {
            _pointCounter++;

            if (_pointCounter == POINTS_UNTIL_DOOR_OPEN)
            {
                Debug.Log("Open door");
                Invoke("CallOpenDoor", 3.4f - patchCursorChangeLinear.Speed);
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
                    Invoke("PlaySyringeStab", 0.5f);
                }

                Invoke("CallPlayerStabbed", 1f);
            }
        }
    }

    public void PlaySyringeStab()
    {
        SoundsControl.Instance.PlaySound(SoundsControl.Sounds.SYRINGE_STAB);
    }

    public void CallOpenDoor()
    {
        if (!EndLevelControl.Instance.gameEnded)
        {
            DoctorControl.Instance.doorAnimator.Play("OpenDoor");
            SoundsControl.Instance.PlaySound(SoundsControl.Sounds.DOOR);
        }
    }

    public void CallPlayerStabbed()
    {
        if (!EndLevelControl.Instance.gameEnded)
        {
            Debug.Log("STAB!");
        }
    }
}
