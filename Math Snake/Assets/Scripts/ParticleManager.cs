using UnityEngine;

public class ParticleManager : MonoBehaviour
{

    public GameObject mathParticle;
    public GameObject hitParticle;
    public GameObject scoreParticle;

    private Particle _mathParticle;
    private Particle _hitParticle;
    private Particle _scoreParticle;

    private void Awake()
    {
        _mathParticle = mathParticle.GetComponent<Particle>();
        _hitParticle = hitParticle.GetComponent<Particle>();
        _scoreParticle = scoreParticle.GetComponent<Particle>();
    }

    public void PlayHitParticle(Vector3 position)
    {
        _hitParticle.PlayParticle(position);
    }

    public void PlayMathParticle(Vector3 position)
    {
        _mathParticle.PlayParticle(position - new Vector3(0.5f, 0.5f, 0));
    }

    public void PlayScoreParticle(Vector3 position)
    {
        _scoreParticle.PlayParticle(position);
    }
}
