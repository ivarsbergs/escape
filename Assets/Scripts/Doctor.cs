using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BansheeGz.BGSpline.Components;
using BansheeGz.BGSpline.Curve;

public class Doctor : MonoBehaviour
{

    public const int POINTS_UNTIL_END = 4;
    public const int POINTS_UNTIL_DOOR_OPEN = 1;
    public const float STABBING_DISTANCE = 1f;

    public GameObject doctorObj;

    public Animator animator;
    private int _pointCounter;

    public BGCcCursor pathCursor;
    public BGCurve pathCurve;
    public BGCcCursorChangeLinear patchCursorChangeLinear;

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
            StopMovingForward();
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
        animator.SetFloat("Speed", speed);
    }

    public void RipOffRand()
    {
        StopMovingForward();

        animator.Play("Dead");


        //var meshRenerers = GetComponentsInChildren<SkinnedMeshRenderer>();
        //foreach (SkinnedMeshRenderer mr in meshRenerers)
        //{
        //    Debug.Log("Set alpha");
        //    var mrAlpha = mr.material.color;
        //    mrAlpha.a = 0;
        //    mr.material.color = new Color(mr.material.color.r, mr.material.color.g, mr.material.color.b, 0);
        //}
    }

    public void StopMovingForward()
    {
        pathCursor.GetComponent<BGCcCursorObjectTranslate>().ObjectToManipulate = null;
        pathCursor.GetComponent<BGCcCursorObjectRotate>().ObjectToManipulate = null;
        //foreach (Transform child in testMath.transform)
        //{
        //    GameObject.Destroy(child.gameObject);
        //}
        //Destroy(testMath);
    }

    public void ReachPoint()
    {
        _pointCounter++;

        if (_pointCounter == POINTS_UNTIL_DOOR_OPEN)
        {
            Debug.Log("Open door");
            Invoke("CallOpenDoor", 3.4f - patchCursorChangeLinear.Speed);
        }

        if (_pointCounter >= POINTS_UNTIL_END)
        {
            animator.SetBool("PlayerReached", true);
        }
    }

    public void CallOpenDoor()
    {
        DoctorControl.Instance.doorAnimator.Play("OpenDoor");
        SoundsControl.Instance.PlaySound(SoundsControl.Sounds.DOOR);
    }
}
