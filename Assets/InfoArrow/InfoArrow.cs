using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sacristan.Utils;

public class InfoArrow : Singleton<InfoArrow>
{
    [SerializeField] private Transform target;

    public Transform Target
    {
        get { return target; }
        set { target = value; }
    }

    private IEnumerator Start()
    {
        while (true)
        {
            if (target != null)
            {
                transform.LookAt(target);
            }

            yield return null;
        }
    }

}
