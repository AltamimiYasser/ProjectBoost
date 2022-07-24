using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrustVelocity = 1000f;
    [SerializeField] float rotateThrustVelocity = 100f;
    [SerializeField] AudioClip mainThrustAudio;
    [SerializeField] ParticleSystem mainThrustParticles;
    [SerializeField] ParticleSystem rightSideThrustParticles;
    [SerializeField] ParticleSystem leftSideThrustParticles;

    Rigidbody rb;
    AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotate();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();

        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotate()
    {
        if (Input.GetKey(KeyCode.A)) RotateLeft();
        else if (Input.GetKey(KeyCode.D)) RotateRight();
        else StopSideParticles();
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrustVelocity * Time.deltaTime);

        if (!audioSource.isPlaying) audioSource.PlayOneShot(mainThrustAudio);
        if (!mainThrustParticles.isPlaying) mainThrustParticles.Play();
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        mainThrustParticles.Stop();
    }

    private void RotateLeft()
    {
        ApplyRotation(Vector3.forward);
        if (!rightSideThrustParticles.isPlaying) rightSideThrustParticles.Play();
    }

    private void RotateRight()
    {
        ApplyRotation(Vector3.back);
        if (!leftSideThrustParticles.isPlaying) leftSideThrustParticles.Play();
    }

    private void StopSideParticles()
    {
        rightSideThrustParticles.Stop();
        leftSideThrustParticles.Stop();
    }

    void ApplyRotation(Vector3 rotation)
    {
        rb.freezeRotation = true; // so when we hit an obstacle, it doesn't force the rotation
        transform.Rotate(rotation * rotateThrustVelocity * Time.deltaTime);
        rb.freezeRotation = false;
        rb.constraints =
         RigidbodyConstraints.FreezeRotationX |
          RigidbodyConstraints.FreezeRotationY |
          RigidbodyConstraints.FreezePositionZ;
    }
}
