using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrustVelocity = 1000f;
    [SerializeField] float rotateThrustVelocity = 100f;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
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
