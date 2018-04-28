using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLauncherBase : MonoBehaviour
{
    [SerializeField] ParticleSystem particleLauncher;
    [SerializeField] Gradient particleColorGradient;

    SplatParticleDecalPool splatDecalPool;
    ParticleSystem splatterParticles;
    List<ParticleCollisionEvent> collisionEvents;

    protected virtual void Start()
    {
        SplatterParticle splatterParticle = FindObjectOfType<SplatterParticle>();
        splatterParticles = splatterParticle.GetComponent<ParticleSystem>();

        splatDecalPool = FindObjectOfType<SplatParticleDecalPool>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    protected virtual void ShootParticle()
    {
        ParticleSystem.MainModule psMain = particleLauncher.main;
        psMain.startColor = particleColorGradient.Evaluate(Random.Range(0f, 1f));
        particleLauncher.Emit(1);
    }

    void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(particleLauncher, other, collisionEvents);

        for (int i = 0; i < collisionEvents.Count; i++)
        {
            splatDecalPool.ParticleHit(collisionEvents[i], particleColorGradient);
            EmitAtLocation(collisionEvents[i]);
        }
    }

    void EmitAtLocation(ParticleCollisionEvent particleCollisionEvent)
    {
        splatterParticles.transform.position = particleCollisionEvent.intersection;
        splatterParticles.transform.rotation = Quaternion.LookRotation(particleCollisionEvent.normal);
        ParticleSystem.MainModule psMain = splatterParticles.main;
        psMain.startColor = particleColorGradient.Evaluate(Random.Range(0f, 1f));

        splatterParticles.Emit(1);
    }

}
