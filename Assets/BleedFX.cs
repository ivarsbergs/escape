using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BleedFX : ParticleLauncherBase
{
    [SerializeField] private int framesInterval = 1;
    [SerializeField] private int intensity = 1;


    protected override void Start()
    {
        base.Start();
        StartCoroutine(BleedRoutine());
    }

    private IEnumerator BleedRoutine()
    {
        while (true)
        {
            for (int i = 0; i < intensity; ++i) ShootParticle();
            for (int i = 0; i < framesInterval; ++i) yield return null;
        }
    }
}
