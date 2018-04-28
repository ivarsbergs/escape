using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ControllerBehaviour : MonoBehaviour
{
    public Material DefaultMaterial;
    public Material ActiveMaterial;
    public Material TestingMaterial;
    public GameObject Cursor;
    public GameObject HandPrefab;
    public GameObject HandParent;
    public FixedJoint grabJoint;
    private GameObject liveHand;
    private Renderer cursorRenderer;
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
        this.cursorRenderer = Cursor.GetComponent<Renderer>();
        this.grabJoint.connectedBody = null;
    }
    void Start()
    {
    }

    void Update()
    {
        if (grabJoint.connectedBody != null && !this.device.GetPress(EVRButtonId.k_EButton_SteamVR_Trigger))
        {
            Rigidbody rigidbody = grabJoint.connectedBody;
            rigidbody.velocity = device.velocity;
            rigidbody.angularVelocity = device.angularVelocity;
            grabJoint.connectedBody = null;
        }
        /*if(this.device.GetPress(EVRButtonId.k_EButton_SteamVR_Trigger))
        {
            this.cursorRenderer.material = ActiveMaterial;
        } else
        {
            this.cursorRenderer.material = DefaultMaterial;
        }*/
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Hand")
        {
            this.cursorRenderer.material = TestingMaterial;
            if (this.device.GetPressDown(EVRButtonId.k_EButton_SteamVR_Trigger) && grabJoint.connectedBody == null)
            {
                Debug.Log("3");
                grabJoint.connectedBody = other.GetComponent<Rigidbody>();
            }
        }
        else
        {
            this.cursorRenderer.material = DefaultMaterial;
        }
        if (other.tag == "LiveHand")
        {
            this.cursorRenderer.material = ActiveMaterial;
            if (this.device.GetPressDown(EVRButtonId.k_EButton_SteamVR_Trigger) && grabJoint.connectedBody == null)
            {
                grabJoint.connectedBody = other.GetComponent<Rigidbody>();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == this.liveHand)
        {
            GameObject hand = Instantiate(HandPrefab);
            hand.transform.parent = this.HandParent.transform;
            this.liveHand = null;
        }
    }
}
