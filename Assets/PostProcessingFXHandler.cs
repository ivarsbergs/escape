using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class PostProcessingFXHandler : MonoBehaviour
{

    [SerializeField] private float fadeTime = 1f;

    private PostProcessingBehaviour _postProcessingBehaviour;
    private VignetteModel.Settings _vignetteSettings;

    private void Start()
    {
        _postProcessingBehaviour = GetComponent<PostProcessingBehaviour>();
        UpdateEnd(0f);
        StartCoroutine(EndRoutine());
    }

    private IEnumerator EndRoutine()
    {
        float t = 0f;

        PostProcessingProfile postProcessingProfile = _postProcessingBehaviour.profile;
        _vignetteSettings = postProcessingProfile.vignette.settings;

        while (t <= 1f)
        {
            t += Time.deltaTime / fadeTime;
            UpdateEnd(t);
            yield return null;
        }
    }

    private void UpdateEnd(float perc)
    {
        _vignetteSettings.intensity = Mathf.Lerp(0f, 1f, perc);
    }
}
