using UnityEngine;
public class RotateCube : MonoBehaviour
{
    public GameObject CameraTarget;

    [SerializeField, Range(0.1f, 1f)] private float rotateSpeed = 1f;
    private Vector3 preMousePosition;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            preMousePosition = Input.mousePosition;
        }
        MouseDrag(Input.mousePosition);

        return;
    }

    private void MouseDrag(Vector3 mousePosition)
    {
        Vector3 diff = mousePosition - preMousePosition;
        if (diff.magnitude < Vector3.kEpsilon)
        {
            return;
        }

        float d = distance();
        if (Input.GetMouseButton(0))//回転移動
        {
            transform.Translate(-diff * Time.deltaTime * rotateSpeed * d);
            LookCameraTarget();
            transform.position += transform.forward * ((transform.position - CameraTarget.transform.position).magnitude - d);//直線移動と曲線移動の誤差修正
        }

        preMousePosition = mousePosition;
        return;
    }

    private float distance()
    {
        return (transform.position - CameraTarget.transform.position).magnitude;
    }

    private void LookCameraTarget()
    {
        transform.LookAt(CameraTarget.transform);
        return;
    }
}