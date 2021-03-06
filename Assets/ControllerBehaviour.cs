﻿using System.Collections;
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

    public GameObject ArmSpawnerMarker;
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
        this.liveHand = null;
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
        if (this.liveHand != null && !this.device.GetPress(EVRButtonId.k_EButton_SteamVR_Trigger))
        {
            this.liveHand = null;
            BleedFX bleedFX = liveHand.GetComponentInChildren<BleedFX>();
            if (bleedFX != null) bleedFX.Dropped();
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
            if (this.device.GetPressDown(EVRButtonId.k_EButton_SteamVR_Trigger) && this.liveHand == null)
            {
                this.liveHand = other.gameObject;

                BleedFX bleedFX = liveHand.GetComponentInChildren<BleedFX>();
                if (bleedFX != null) bleedFX.PickedUp();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Exited " + other.tag);
        if (this.liveHand == other.gameObject)
        {

            other.gameObject.GetComponent<LiveHandBehaviour>().doctor.RipOffRand();
            Debug.Log("Ripped hand " + this.gameObject.name);
            GameObject hand = Instantiate(HandPrefab);
            hand.transform.position = this.ArmSpawnerMarker.transform.position;
            hand.transform.rotation = this.ArmSpawnerMarker.transform.rotation;
            //hand.transform.position += new Vector3(0, 0, 1f);
            hand.transform.parent = this.HandParent.transform;
            this.grabJoint.connectedBody = hand.GetComponent<HandBehaviour>().holdableRigidbody;

            BleedFX bleedFX = liveHand.GetComponentInChildren<BleedFX>();
            if (bleedFX != null) bleedFX.Dropped();

            this.liveHand = null;
            StartCoroutine(LongVibration(1, 3999));
            //SteamVR_Controller.Input(4).TriggerHapticPulse(3000);

            //GetComponent<SteamVR_TrackedObject>().index.get
        }
    }

    IEnumerator LongVibration(float length, ushort strength)
    {
        for (float i = 0; i < length; i += Time.deltaTime)
        {
            device.TriggerHapticPulse(strength);
            yield return null; //every single frame for the duration of "length" you will vibrate at "strength" amount
        }
    }

}
