using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsControl : MonoBehaviour {

    private static SoundsControl instance = null;

    public static SoundsControl Instance
    {
        get
        {
            return instance;
        }
    }

    public enum Sounds
    {
        BLOOD_SPILL,
        FOOTSTEP,
        TEARING_ARM,
        SCREAM,
        DOOR,
        UNICORN,
        SPEAKER_1,
        SPEAKER_2,
        DOCTOR_TALK,
        DOCTOR_YELL,
        SYRINGE_STAB
    }

    public AudioSource speakerSource;
    public AudioSource doorSource;
    public AudioSource unicornSource;
    public AudioSource behindHeadSource;

    public AudioClip speaker1Clip;
    public AudioClip speaker2Clip;
    public AudioClip doorBuzzClip;
    public AudioClip unicornClip;
    public AudioClip syringeClip;
    public AudioClip doctorYellClip;
    public AudioClip doctorTalkClip;
    public AudioClip armTearClip;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlaySound(Sounds soundSelected)
    {
        switch (soundSelected)
        {
            case Sounds.DOOR:
                doorSource.PlayOneShot(doorBuzzClip);
                break;
            case Sounds.UNICORN:
                unicornSource.PlayOneShot(unicornClip);
                break;
            case Sounds.TEARING_ARM:
                if (DoctorControl.Instance.currentDoctor != null)
                    DoctorControl.Instance.currentDoctor.armAudioSource.PlayOneShot(armTearClip);
                break;
            case Sounds.SPEAKER_1:
                speakerSource.PlayOneShot(speaker1Clip);
                break;
            case Sounds.SPEAKER_2:
                speakerSource.PlayOneShot(speaker2Clip);
                break;
            case Sounds.DOCTOR_TALK:
                if (DoctorControl.Instance.currentDoctor != null)
                    DoctorControl.Instance.currentDoctor.mouthAudioSource.PlayOneShot(doctorTalkClip);
                break;
            case Sounds.DOCTOR_YELL:
                if (DoctorControl.Instance.currentDoctor != null)
                    DoctorControl.Instance.currentDoctor.mouthAudioSource.PlayOneShot(doctorYellClip);
                break;
            case Sounds.SYRINGE_STAB:
                behindHeadSource.PlayOneShot(syringeClip);
                break;
        }
    }
}
