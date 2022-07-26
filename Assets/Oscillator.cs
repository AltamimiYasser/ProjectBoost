using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPos;
    [SerializeField] Vector3 movementVector;
    [SerializeField][Range(0, 1)] float movementFactor;

    void Start()
    {
        startingPos = transform.position;
    }


    void Update()
    {
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos - offset;
    }
}
