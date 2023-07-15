using UnityEngine;

public class Particle : MonoBehaviour
{
    public int speed;
    public AudioSource sound;

    void Update()
    {
        transform.Rotate(Vector3.forward * speed * Time.deltaTime);
    }

    public void PlayParticle(Vector3 position)
    {
        sound.Play();
        transform.position = position;
        GetComponent<ParticleSystem>().Play();
    }
}
