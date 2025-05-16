using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingCreditScroll : MonoBehaviour
{
    private ScrollRect _scrollRect;

    public float _duration;

    void Start()
    {
        _scrollRect = this.GetComponent<ScrollRect>();
        StartCoroutine(AutoScroll());
    }

    private IEnumerator AutoScroll()
    {
        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime / _duration;
            _scrollRect.verticalNormalizedPosition = Mathf.Lerp(1f, 0f, t);
            yield return null;
        }
    }
}
