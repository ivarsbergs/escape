using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PentagrammonGlow : MonoBehaviour
{
    [SerializeField] private Color targetColor = Color.red;
    [SerializeField] private Gradient pentagrammonDrawnGradient;
    [SerializeField] private float lerpTime = 1f;
    [SerializeField] private float maxZ = 0.01f;

    private Renderer _renderer;
    private Color _originColor;

    Coroutine glowRoutine;

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

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        Color originColor = MatColor;

        glowRoutine = StartCoroutine(GlowBase());
        PentagrammonManager.Instance.OnPentagramDrawn += Instance_OnPentagramDrawn;
    }

    private void Instance_OnPentagramDrawn()
    {
        StopCoroutine(glowRoutine);
        StartCoroutine(GlowDrawn());
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        float t = 0f;
        float originalZ = transform.localPosition.z;

        while (t <= 1f)
        {
            t += Time.deltaTime / lerpTime;

            float z = Mathf.Lerp(originalZ, maxZ, t);
            Vector3 newVec = transform.localPosition;
            newVec.z = z;

            transform.localPosition = newVec;

            yield return null;
        }
    }

    private IEnumerator GlowDrawn()
    {
        while (true)
        {
            float t = 0f;

            while (t <= 1f)
            {
                t += Time.deltaTime / lerpTime;
                MatColor = pentagrammonDrawnGradient.Evaluate(t);
                yield return null;
            }

            while (t >= 0f)
            {
                t -= Time.deltaTime / lerpTime;
                MatColor = pentagrammonDrawnGradient.Evaluate(t);
                yield return null;
            }
        }
    }

    private IEnumerator GlowBase()
    {
        while (true)
        {
            float t = 0f;

            while (t <= 1f)
            {
                t += Time.deltaTime / lerpTime;
                MatColor = Color.Lerp(_originColor, targetColor, t);
                yield return null;
            }

            while (t >= 0f)
            {
                t -= Time.deltaTime / lerpTime;
                MatColor = Color.Lerp(_originColor, targetColor, t);
                yield return null;
            }
        }
    }

}
