using UnityEngine;

public class Particle : MonoBehaviour
{
    public int speed;
    public AudioSource sound;
    public Transform target;

    void Update()
    {
        transform.Rotate(Vector3.forward * speed * Time.deltaTime);

        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    public void PlayParticle(Vector3 position)
    {
        sound.Play();
        transform.position = position;
        GetComponent<ParticleSystem>().Play();
    }

    public void SetParticleTransform(Vector3 position)
    {
        transform.position = position;
    }
}
