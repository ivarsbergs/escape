using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ControllerBehaviour : MonoBehaviour
{
    public CharacterJoint grabJoint;
    protected SteamVR_TrackedObject trackedObj;
    public SteamVR_Controller.Device device
    {
        get
        {
            return SteamVR_Controller.Input((int)trackedObj.index);
        }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
    void Start()
    {
    }

    void Update()
    {
    }

    void OnTriggerStay(Collider other)
    {
        if (this.device.GetPressDown(EVRButtonId.k_EButton_SteamVR_Trigger))
        {
            grabJoint.connectedBody = other.GetComponent<Rigidbody>();
        }
    }
}
