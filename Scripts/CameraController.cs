using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [HideInInspector]
    public Camera camera;
    public Transform target;
    public float movementSpeed;
    public float zoomSpeed;

    private Coroutine zooming;

    private void Start()
    {
        camera = GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position - new Vector3(0,0,10), Time.deltaTime * movementSpeed);
    }

    public void ZoomTo(float zoom)
    {
        if (zooming != null)
        {
            StopCoroutine(zooming);
        }
        zooming = StartCoroutine(Zoom(zoom));
    }

    IEnumerator Zoom(float to)
    {
        while(camera.orthographicSize != to)
        {
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, to, zoomSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }
}
