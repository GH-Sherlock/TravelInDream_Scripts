using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CineFadeScreen : MonoBehaviour
{
    public float _fadeTime = 3f;

    private Image _fadeImage;
    

    // Start is called before the first frame update
    void Start()
    {
        _fadeImage = this.GetComponent<Image>();
    }

    private IEnumerator ScreenFadeIn(Color fadeColor)
    {
        float elapsedTime = 0f;
        float value = 1f;
        Color color = fadeColor;

        color.a = value;
        _fadeImage.color = fadeColor;
        

        while(elapsedTime < _fadeTime)
        {
            elapsedTime += Time.deltaTime;

             value = Mathf.Lerp(1f, 0f, elapsedTime / _fadeTime);
            color.a = value;
            _fadeImage.color = color;


            yield return null;
        }

        color.a = 0f;
        _fadeImage.color = color;
    }

    private IEnumerator ScreenFadeOut(Color fadeColor)
    {
        float elapsedTime = 0f;
        float value = 0f;
        Color color = fadeColor;

        color.a = value;
        _fadeImage.color = fadeColor;


        while (elapsedTime < _fadeTime)
        {
            elapsedTime += Time.deltaTime;

            value = Mathf.Lerp(0f, 1f, elapsedTime / _fadeTime);
            color.a = value;
            _fadeImage.color = color;


            yield return null;
        }

        color.a = 1f;
        _fadeImage.color = color;
    }


    public void WhiteFadeIn()
    {
        StartCoroutine(ScreenFadeIn(Color.white));
    }

    public void WhiteFadeOut()
    {
        StartCoroutine(ScreenFadeOut(Color.white));
    }

    public void BlackFadeIn()
    {
        StartCoroutine(ScreenFadeIn(Color.black));
    }

    public void BlackFadeOut()
    {
        StartCoroutine(ScreenFadeOut(Color.black));
    }
}
