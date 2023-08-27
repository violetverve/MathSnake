using UnityEngine;

public class Particle : MonoBehaviour
{
    public int speed;
    public Transform target;
    public Vector3 offset;

    void Update()
    {
        transform.Rotate(Vector3.forward * speed * Time.deltaTime);

        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position - offset, speed * Time.deltaTime);
        }
    }

    public void PlayParticle()
    {
        transform.position = target.position - offset;
        GetComponent<ParticleSystem>().Play();
    }

    public void SetParticleTransform(Vector3 position)
    {
        transform.position = position;
    }
}
