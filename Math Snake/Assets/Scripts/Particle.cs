using UnityEngine;

public class Particle : MonoBehaviour
{
    public int speed;

    void Update()
    {
        transform.Rotate(Vector3.forward * speed * Time.deltaTime);
    }

    public void PlayParticle(Vector3 position)
    {
        transform.position = position;
        GetComponent<ParticleSystem>().Play();
    }
}
