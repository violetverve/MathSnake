using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

    public GameObject mathParticle;
    public GameObject hitParticle;


    void Update()
    {

    }

    public void PlayHitParticle(Vector3 position)
    {
        hitParticle.transform.position = position;
        hitParticle.GetComponent<ParticleSystem>().Play();
    }

    public void PlayMathParticle(Vector3 position)
    {
        mathParticle.transform.position = position - new Vector3(0.5f, 0.5f, 0);
        mathParticle.GetComponent<ParticleSystem>().Play();
    }
}
