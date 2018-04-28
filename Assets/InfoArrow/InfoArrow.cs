#define DEBUG_INFO_ARROW
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sacristan.Utils;

public class InfoArrow : Singleton<InfoArrow>
{
    [SerializeField] private Transform meshRoot;
    [SerializeField] private float intensity = 0.5f;

#if DEBUG_INFO_ARROW
    [SerializeField]
#endif
    private Transform target;


    private bool isEnabled = false;

    public Transform Target
    {
        get { return target; }
        set { target = value; }
    }

    protected override void Awake()
    {
        base.Awake();
        Enable(false);
    }

#if DEBUG_INFO_ARROW
    private void Start()
    {
        Enable(true);
    }
#endif

    private IEnumerator ArrowRoutine()
    {
        while (isEnabled)
        {
            if (target != null)
            {
                transform.LookAt(target);
                meshRoot.transform.localPosition = Vector3.forward * Mathf.PingPong(Time.time, intensity);
            }

            yield return null;
        }
    }

    public void Enable(bool flag)
    {
        isEnabled = flag;
        meshRoot.gameObject.SetActive(isEnabled);
        if (isEnabled) StartCoroutine(ArrowRoutine());
    }

}
