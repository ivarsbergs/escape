using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PentagrammonGlow : MonoBehaviour
{
    [SerializeField] private Color targetColor = Color.red;
    [SerializeField] private float lerpTime = 1f;

    private Renderer _renderer;

    private Color MatColor
    {
        get
        {
            return _renderer.material.GetColor("_TintColor");
        }
        set
        {
            _renderer.material.SetColor("_TintColor", value);
        }
    }

    private IEnumerator Start()
    {
        _renderer = GetComponent<Renderer>();
        Color originColor = MatColor;

        while (true)
        {
            float t = 0f;


            while (t <= 1f)
            {
                t += Time.deltaTime / lerpTime;
                MatColor = Color.Lerp(originColor, targetColor, t);
                yield return null;
            }

            while (t >= 0f)
            {
                t -= Time.deltaTime / lerpTime;
                MatColor = Color.Lerp(originColor, targetColor, t);
                yield return null;
            }
        }
    }

    private void SetColor()
    {

    }


}
