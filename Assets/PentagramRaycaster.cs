using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PentagramRaycaster : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float distance = 5f;

    List<PentagrammonPoint> uncheckedPentagrammonPoints;

    Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        uncheckedPentagrammonPoints = new List<PentagrammonPoint>(GetComponentsInChildren<PentagrammonPoint>());
    }

    private void Update()
    {
        //if (Input.GetKey(KeyCode.F)) RaycastPentagrammonPoint();
        RaycastPentagrammonPoint();
    }

    private void RaycastPentagrammonPoint()
    {
        //Vector3 dir = _camera.transform.forward;

        //Ray ray = new Ray
        //{
        //    origin = _camera.transform.position,
        //    direction = dir
        //};

        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
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
            Debug.Log("Done!!!");
        }
    }

}
