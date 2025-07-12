using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    
    [Header("Particle Prefabs")]
    public GameObject particleA;
    public GameObject particleB;

    [Header("Path Points")]
    public Transform startPoint;
    public Transform endPoint;
    public Transform controlPoint;

    [Header("Settings")]
    public float travelTime = 2.0f;

    public void Play()
    {
        StartCoroutine(BezierMove());
    }

    private IEnumerator BezierMove()
    {
        GameObject a = Instantiate(particleA, startPoint.position, Quaternion.identity);
        float elapsed = 0f;

        while (elapsed < travelTime)
        {
            float t = elapsed / travelTime;

            Vector3 p0 = startPoint.position;
            Vector3 p1 = controlPoint.position;
            Vector3 p2 = endPoint.position;

            Vector3 bezierPos = Mathf.Pow(1 - t, 2) * p0 +
                                2 * (1 - t) * t * p1 +
                                Mathf.Pow(t, 2) * p2;

            Vector3 dir = bezierPos - a.transform.position;

            a.transform.position = bezierPos;

            if (dir != Vector3.zero)
            {
                Quaternion rot = Quaternion.LookRotation(dir);
                a.transform.rotation = Quaternion.Euler(rot.eulerAngles.x, 0f, 0f);
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(a);

        Instantiate(particleB, endPoint.position, Quaternion.identity);
    }
}
