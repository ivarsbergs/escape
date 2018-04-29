using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BleedFX : ParticleLauncherBase
{
    [SerializeField] private float spawnParticlePossibility = 0.25f;
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
            if (Random.value < spawnParticlePossibility)
            {
                for (int i = 0; i < intensity; ++i) ShootParticle();
            }

            yield return null;
        }
    }
}
