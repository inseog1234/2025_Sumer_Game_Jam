using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Camera mainCam;
    public float defaultSize = 5f;
    public float zoomedSize = 3f;
    public float zoomSpeed = 5f;
    public float moveSpeed = 5f;

    private Coroutine currentRoutine;

    public void ZoomToTarget(Transform target, float zoomSize)
    {
        if (currentRoutine != null) StopCoroutine(currentRoutine);
        currentRoutine = StartCoroutine(ZoomAndMove(target, zoomSize));
    }

    public void ResetZoom(Vector3 originalPosition)
    {
        if (currentRoutine != null) StopCoroutine(currentRoutine);
        currentRoutine = StartCoroutine(ZoomAndMoveBack(originalPosition));
    }

    IEnumerator ZoomAndMove(Transform target, float zoomSize)
    {
        while (Mathf.Abs(mainCam.orthographicSize - zoomSize) > 0.01f ||
               Vector3.Distance(transform.position, target.position) > 0.01f)
        {
            mainCam.orthographicSize = Mathf.Lerp(mainCam.orthographicSize, zoomSize, Time.deltaTime * zoomSpeed);

            // 카메라 z축은 유지 (예: -10)
            Vector3 targetPos = new Vector3(target.position.x, target.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveSpeed);

            yield return null;
        }

        mainCam.orthographicSize = zoomSize;
    }

    IEnumerator ZoomAndMoveBack(Vector3 originalPosition)
    {
        while (Mathf.Abs(mainCam.orthographicSize - defaultSize) > 0.01f ||
               Vector3.Distance(transform.position, originalPosition) > 0.01f)
        {
            mainCam.orthographicSize = Mathf.Lerp(mainCam.orthographicSize, defaultSize, Time.deltaTime * zoomSpeed);
            Vector3 pos = new Vector3(originalPosition.x, originalPosition.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * moveSpeed);
            yield return null;
        }

        mainCam.orthographicSize = defaultSize;
    }
}
