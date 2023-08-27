using UnityEngine;
using System.Collections.Generic;

public class ParticleManager : MonoBehaviour
{
    public GameObject mathParticle;
    public GameObject hitParticle;
    public GameObject scoreParticle;
    public GameObject recordParticle;

    private Dictionary<string, Particle> _particles;

    private void Awake()
    {
        _particles = new Dictionary<string, Particle>();

        AddParticle("math", mathParticle);
        AddParticle("hit", hitParticle);
        AddParticle("score", scoreParticle);
        AddParticle("record", recordParticle);
    }

    private void AddParticle(string particleName, GameObject particleGameObject)
    {
        Particle particle = particleGameObject.GetComponent<Particle>();
        _particles.Add(particleName, particle);
    }

    public void PlayParticle(string particleName)
    {
        _particles[particleName].PlayParticle();
    }
}
