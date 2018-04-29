using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sacristan.Utils;

public class PentagramRaycaster : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float distance = 5f;


    Camera _camera;


    private void Start()
    {
        _camera = GetComponent<Camera>();

        StartCoroutine(RaycastPentagrammonPointRoutine());
    }

    private IEnumerator RaycastPentagrammonPointRoutine()
    {
        while (!PentagrammonManager.Instance.PentagramDrawn)
        {
            yield return null;
            RaycastPentagrammonPoint();
        }
    }

    private void RaycastPentagrammonPoint()
    {
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
            if (pentagrammonPoint != null) PentagrammonManager.Instance.PointDrawn(pentagrammonPoint);
        }
    }

}
