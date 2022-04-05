using UnityEngine;

[ExecuteInEditMode]
public class FaceCamera : MonoBehaviour
{
    // Start is called before the first frame update

    Transform cam;
    Vector3 targetAngle = Vector3.zero;

    void Start()
    {
        cam = Camera.main.transform;
    }

    void Update()
    {
        // targetAngle = new Vector3(0, cam.position.y, 0);
        // transform.LookAt(targetAngle);
    }
}
