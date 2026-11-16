using UnityEngine;

// Original tutorial motion, with the displacement doubled to match this project's 2x cage.
public sealed class BoatRock : MonoBehaviour
{
    [SerializeField] float rockSpeed = 1;
    [SerializeField] float rockDistance = -0.06f;
    [SerializeField] float timeOffset = -0.9f;
    Vector3 originalPosition;

    void Awake() => originalPosition = transform.position;

    void Update()
    {
        float offset = Mathf.Sin(Time.time * rockSpeed + timeOffset) * rockDistance;
        transform.position = originalPosition + Vector3.up * offset;
    }
}
