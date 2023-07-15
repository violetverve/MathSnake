using UnityEngine;

public class ParticleManager : MonoBehaviour
{

    public GameObject mathParticle;
    public GameObject hitParticle;

    private Particle _mathParticle;
    private Particle _hitParticle;

    private void Awake()
    {
        _mathParticle = mathParticle.GetComponent<Particle>();
        _hitParticle = hitParticle.GetComponent<Particle>();
    }

    public void PlayHitParticle(Vector3 position)
    {
        _hitParticle.PlayParticle(position);

        // hitParticle.transform.position = position;
        // hitParticle.GetComponent<ParticleSystem>().Play();
    }

    public void PlayMathParticle(Vector3 position)
    {
        _mathParticle.PlayParticle(position - new Vector3(0.5f, 0.5f, 0));
        // mathParticle.transform.position = position - new Vector3(0.5f, 0.5f, 0);
        // mathParticle.GetComponent<ParticleSystem>().Play();
    }
}
