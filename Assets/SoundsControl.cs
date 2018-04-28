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
        UNICORN
    }

    public List<AudioClip> soundClips;

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
        
    }
}
