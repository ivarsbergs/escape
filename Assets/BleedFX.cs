using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BleedFX : ParticleLauncherBase
{
    [SerializeField] private int frameRate = 1;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(BleedRoutine());
    }

    private IEnumerator BleedRoutine()
    {
        while (true)
        {
            ShootParticle();
            for (int i = 0; i < frameRate; ++i) yield return null;
        }
    }
}
