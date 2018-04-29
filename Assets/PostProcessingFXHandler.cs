using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class PostProcessingFXHandler : MonoBehaviour
{

    [SerializeField] private float fadeTime = 1f;

    private PostProcessingBehaviour _postProcessingBehaviour;
    PostProcessingProfile _postProcessingProfile;
    private VignetteModel.Settings _vignetteSettings;

    private void Start()
    {
        _postProcessingBehaviour = GetComponent<PostProcessingBehaviour>();
        _postProcessingProfile = _postProcessingBehaviour.profile;
        _vignetteSettings = _postProcessingProfile.vignette.settings;

        UpdateEnd(0f);
        DoctorControl.Instance.OnPlayerStabbed += Instance_OnPlayerStabbed;
    }

    private void Instance_OnPlayerStabbed()
    {
        StartCoroutine(EndRoutine());
    }

    private IEnumerator EndRoutine()
    {
        float t = 0f;

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
        _postProcessingProfile.vignette.settings = _vignetteSettings;
    }
}
