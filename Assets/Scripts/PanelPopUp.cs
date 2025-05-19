using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelPopUp : MonoBehaviour
{
    public float duration = 1f;
    public float overshootScale = 1.1f;

    void Start()
    {
        transform.localScale = Vector3.zero;

        StartCoroutine(PopUp());
    }

    IEnumerator PopUp()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float scale = Mathf.Lerp(0, overshootScale, t);
            transform.localScale = new Vector3(scale, scale, 1);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = new Vector3(overshootScale, overshootScale, 1);

        elapsedTime = 0f;
        float bounceDuration = 0.2f;

        while (elapsedTime < bounceDuration)
        {
            float t = elapsedTime / bounceDuration;
            float scale = Mathf.Lerp(overshootScale, 1f, t);
            transform.localScale = new Vector3(scale, scale, 1);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = Vector3.one;
    }
}
