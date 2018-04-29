using System.Collections;
using System.Collections.Generic;
using Sacristan.Utils;
using UnityEngine;

public class PentagrammonManager : Singleton<PentagrammonManager>
{
    public delegate void EventHandler();
    public event EventHandler OnPentagramDrawn;

    List<PentagrammonPoint> uncheckedPentagrammonPoints;

    private bool pentagramDrawn = false;
    private bool isReady = false;

    public bool PentagramDrawn { get { return pentagramDrawn; } }

    private IEnumerator Start()
    {
        uncheckedPentagrammonPoints = new List<PentagrammonPoint>(FindObjectsOfType<PentagrammonPoint>());
        yield return null;
        isReady = true;
    }

    public void PointDrawn(PentagrammonPoint pentagrammonPoint)
    {
        if (!isReady) return;
        uncheckedPentagrammonPoints.Remove(pentagrammonPoint);
        pentagrammonPoint.GetComponent<MeshRenderer>().enabled = true;

        if (uncheckedPentagrammonPoints.Count == 0)
        {
            pentagramDrawn = true;
            if (OnPentagramDrawn != null) OnPentagramDrawn.Invoke();
        }
    }

}
