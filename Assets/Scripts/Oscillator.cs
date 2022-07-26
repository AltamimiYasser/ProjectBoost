using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector; // Point B
    [SerializeField] float period = 2f; // Duration it takes to reach full cycle

    Vector3 startingPos; // point A
    float movementFactor;

    void Start()
    {
        startingPos = transform.position;
    }


    void Update()
    {
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);
        movementFactor = (rawSinWave + 1f) / 2f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos - offset;
    }
}
