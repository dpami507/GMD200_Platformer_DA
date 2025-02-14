using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float lerpSpeed;

    private void FixedUpdate()
    {
        Vector3 desiredPos = target.position + offset + ((Vector3)target.GetComponent<Rigidbody2D>().velocity / 8);
        transform.position = Vector3.Lerp(transform.position, desiredPos, lerpSpeed * Time.deltaTime);
    }
}
