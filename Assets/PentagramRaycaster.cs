using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PentagramRaycaster : MonoBehaviour
{
    public delegate void EventHandler();
    public event EventHandler OnPentagramDrawn;

    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float distance = 5f;

    List<PentagrammonPoint> uncheckedPentagrammonPoints;

    Camera _camera;

    private bool pentagramDrawn = false;

    private void Start()
    {
        _camera = GetComponent<Camera>();

        uncheckedPentagrammonPoints = new List<PentagrammonPoint>(FindObjectsOfType<PentagrammonPoint>());
        StartCoroutine(RaycastPentagrammonPointRoutine());
    }

    private IEnumerator RaycastPentagrammonPointRoutine()
    {
        while (!pentagramDrawn)
        {
            RaycastPentagrammonPoint();
            yield return null;
        }
    }

    private void RaycastPentagrammonPoint()
    {
        //Vector3 dir = _camera.transform.forward;

        //Ray ray = new Ray
        //{
        //    origin = _camera.transform.position,
        //    direction = dir
        //};

        Ray ray;

        if (_camera == null)
        {
            ray = new Ray
            {
                origin = transform.position,
                direction = transform.forward
            };
        }
        else
        {
             ray = _camera.ScreenPointToRay(Input.mousePosition);
        }

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distance, layerMask))
        {
            PentagrammonPoint pentagrammonPoint = hit.collider.GetComponent<PentagrammonPoint>();
            if (pentagrammonPoint != null) RemovePoint(pentagrammonPoint);
        }
    }

    private void RemovePoint(PentagrammonPoint pentagrammonPoint)
    {

        uncheckedPentagrammonPoints.Remove(pentagrammonPoint);
        pentagrammonPoint.GetComponent<MeshRenderer>().enabled = true;

        if(uncheckedPentagrammonPoints.Count == 0)
        {
            pentagramDrawn = true;
            if (OnPentagramDrawn != null) OnPentagramDrawn.Invoke();
        }
    }

}
