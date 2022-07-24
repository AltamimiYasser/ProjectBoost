using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrustVelocity = 1000f;
    [SerializeField] float rotateThrustVelocity = 100f;
    [SerializeField] AudioClip mainThrustAudio;

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
            rb.AddRelativeForce(Vector3.up * mainThrustVelocity * Time.deltaTime);

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainThrustAudio);
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    void ProcessRotate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(Vector3.back);
        }
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
