using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraFollow : MonoBehaviour
{
    Transform target;
    public CameraSettings settings;

    private void Start()
    {
        target = FindFirstObjectByType<PlayerManager>().transform;
    }

    private void FixedUpdate()
    {

        Vector3 desiredPos = target.position + settings.offset + ((Vector3)target.GetComponent<Rigidbody2D>().velocity / 8);
        transform.position = Vector3.Lerp(transform.position, desiredPos, settings.lerpSpeed * Time.deltaTime);
    }
}
