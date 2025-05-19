using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAnimation : MonoBehaviour
{
    public Vector3 targetPosition;
    public float duration = 0.5f;
    public float startYOffset = -6f;

    private Quaternion startRotation;
    private Quaternion endRotation;

    public void AnimateEntrance()
    {
        targetPosition = transform.position;

        transform.position = new Vector3(targetPosition.x, targetPosition.y + startYOffset, targetPosition.z);

        startRotation = Quaternion.Euler(0f, 180f, 0f);
        endRotation = Quaternion.Euler(0f, 0f, 0f);
        transform.rotation = startRotation;

        StartCoroutine(SlideInAndFlip());
    }

    private IEnumerator SlideInAndFlip()
    {
        float elapsed = 0f;
        Vector3 startingPosition = transform.position;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            transform.position = Vector3.Lerp(startingPosition, targetPosition, Mathf.SmoothStep(0, 1, t));

            transform.rotation = Quaternion.Slerp(startRotation, endRotation, Mathf.SmoothStep(0, 1, t));

            yield return null;
        }

        transform.position = targetPosition;
        transform.rotation = endRotation;
    }
}
