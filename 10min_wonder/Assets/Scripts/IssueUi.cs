using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IssueUi : MonoBehaviour
{
    public GameObject issueUi;
    public Transform uiBox;
    public float transitionDuration;
    public AnimationCurve scaleCurve;

    private Coroutine transitionCoroutine;

    public void OnEnable()
    {
        if (uiBox != null)
        {
            // Ensure that the scale is initially set to 0.5
            uiBox.localScale = Vector3.one * 0.9f;

            // Start the transition coroutine
            if (transitionCoroutine != null)
                StopCoroutine(transitionCoroutine);
            transitionCoroutine = StartCoroutine(ScaleTransition());
        }
    }

    IEnumerator ScaleTransition()
    {
        float elapsedTime = 0f;
        Vector3 initialScale = Vector3.one * 0.9f;
        Vector3 targetScale = Vector3.one * 1.1f;

        // Scale up
        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;
            uiBox.localScale = Vector3.Lerp(initialScale, targetScale, scaleCurve.Evaluate(t));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure that the scale is exactly at the target scale to avoid floating point errors
        uiBox.localScale = targetScale;

        // Scale down
        elapsedTime = 0f;
        initialScale = Vector3.one * 1.1f;
        targetScale = Vector3.one;

        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;
            uiBox.localScale = Vector3.Lerp(initialScale, targetScale, scaleCurve.Evaluate(t));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure that the scale is exactly at the target scale to avoid floating point errors
        uiBox.localScale = targetScale;
        issueUi.SetActive(false);
    }
}
