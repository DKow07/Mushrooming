using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{

    public Transform target;



    public Vector3 offset;
    public float zoomSpeed = 4f;
    public float minZoom = 5f;
    public float maxZoom = 15f;

    private float currentZoom = 10f;
    public float pitch = 2f;

    public float yaw = 0f;
    private float yawSpeed = 100f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void LateUpdate()
    {
        transform.position = target.position - offset * currentZoom;
        transform.LookAt(target.position + Vector3.up * pitch); 

        transform.RotateAround(target.position, Vector3.up, yaw);
    }

    void Update()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        yaw -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;


    }
}
