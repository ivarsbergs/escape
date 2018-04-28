using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PentagrammonGlow : MonoBehaviour
{
    [SerializeField] private Color targetColor = Color.red;
    [SerializeField] private float lerpTime = 1f;

    private IEnumerator Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        Color originColor = renderer.material.color;


        while (true)
        {
            float t = 0f;


            while (t <= 1f)
            {
                t += Time.deltaTime / lerpTime;
                renderer.material.color = Color.Lerp(originColor, targetColor, t);
                yield return null;
            }

            while (t >= 0f)
            {
                t += Time.deltaTime / lerpTime;
                renderer.material.color = Color.Lerp(originColor, targetColor, t);
                yield return null;
            }
        }
    }


}
