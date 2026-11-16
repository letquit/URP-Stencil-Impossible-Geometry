using UnityEngine;

/// <summary>Inspect both stencil worlds without the third-person character obscuring the cage.</summary>
[RequireComponent(typeof(Camera))]
public sealed class ImpossibleGeometryDemoCamera : MonoBehaviour
{
    [SerializeField] Vector3 focus = new Vector3(0, 2, 0);
    [SerializeField] float distance = 10;
    [SerializeField] float yaw = -45;
    [SerializeField] float pitch = 8;
    [SerializeField] float orbitSensitivity = 3;
    [SerializeField] float moveSpeed = 4;
    bool autoOrbit;

    void OnEnable() => ApplyView();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SetView(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SetView(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SetView(2);
        if (Input.GetKeyDown(KeyCode.Space)) autoOrbit = !autoOrbit;

        if (Input.GetMouseButton(1))
        {
            autoOrbit = false;
            yaw += Input.GetAxisRaw("Mouse X") * orbitSensitivity;
            pitch = Mathf.Clamp(pitch - Input.GetAxisRaw("Mouse Y") * orbitSensitivity, -75, 75);
        }
        if (autoOrbit) yaw += 12 * Time.deltaTime;
        distance = Mathf.Clamp(distance - Input.mouseScrollDelta.y, 3, 22);

        float speed = moveSpeed * (Input.GetKey(KeyCode.LeftShift) ? 3 : 1) * Time.deltaTime;
        Vector3 movement = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) movement += transform.forward;
        if (Input.GetKey(KeyCode.S)) movement -= transform.forward;
        if (Input.GetKey(KeyCode.A)) movement -= transform.right;
        if (Input.GetKey(KeyCode.D)) movement += transform.right;
        if (Input.GetKey(KeyCode.E)) movement += Vector3.up;
        if (Input.GetKey(KeyCode.Q)) movement -= Vector3.up;
        focus += movement * speed;
        ApplyView();
    }

    public void SetView(int view)
    {
        focus = new Vector3(0, 2, 0);
        distance = view == 2 ? 10 : 9;
        yaw = view == 0 ? 0 : view == 1 ? -90 : -45;
        pitch = view == 2 ? 8 : 3;
        autoOrbit = false;
        ApplyView();
    }

    void ApplyView()
    {
        transform.rotation = Quaternion.Euler(pitch, yaw, 0);
        transform.position = focus - transform.forward * distance;
    }
}
