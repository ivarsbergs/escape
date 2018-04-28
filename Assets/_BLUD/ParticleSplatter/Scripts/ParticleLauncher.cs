using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLauncher : ParticleLauncherBase
{

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            ShootParticle();
        }

    }
}