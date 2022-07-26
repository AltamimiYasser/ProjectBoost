using UnityEngine;

public class Oscillator : MonoBehaviour
{
    const float tau = Mathf.PI * 2;

    [SerializeField] Vector3 movementVector; // offset from point A
    [SerializeField] float period = 2f; // Duration it takes to reach full cycle

    Vector3 startingPos; // point A
    float movementFactor;

    void Start()
    {
        startingPos = transform.position;
    }

    void Update()
    {
        if (period <= Mathf.Epsilon) return; // to not compare floats, (Epsilon is a very tiny number)

        float cycles = Time.time / period;
        float rawSinWave = Mathf.Sin(cycles * tau);
        movementFactor = (rawSinWave + 1f) / 2f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos - offset;
    }
}
