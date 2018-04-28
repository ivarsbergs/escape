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
        if (grabJoint.connectedBody != null && !this.device.GetPressDown(EVRButtonId.k_EButton_SteamVR_Trigger))
        {
            Rigidbody rigidbody = grabJoint.connectedBody;
            rigidbody.velocity = device.velocity;
            rigidbody.angularVelocity = device.angularVelocity;
            grabJoint.connectedBody = null;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Hand")
        {
            if (this.device.GetPressDown(EVRButtonId.k_EButton_SteamVR_Trigger) && grabJoint.connectedBody == null)
            {
                grabJoint.connectedBody = other.GetComponent<Rigidbody>();
            }
        }
    }
}
