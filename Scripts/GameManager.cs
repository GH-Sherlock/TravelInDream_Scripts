using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private AsyncOperation _asyncOperation;
    [SerializeField] private string _loadSceneName;
    [SerializeField] private Color _fadeScreenColor = Color.black;
    [SerializeField] private float _loadDelayTime;

    [Header("SubTitle")]
    [SerializeField] private TextAsset subtitleJsonFile;
    [SerializeField] private TextAsset subTitleXMLFile_KR;
    [SerializeField] private TextAsset subTitleXMLFile_EN;

    private void Start()
    {
        if (subtitleJsonFile != null)
            LanguageManager.instance.LoadText(subtitleJsonFile.text, GetXMLFile());

        Application.backgroundLoadingPriority = ThreadPriority.Low;
        StartCoroutine(LoadSceneAsyncProcess());
    }

    private IEnumerator LoadSceneAsyncProcess()
    {
        if (!string.IsNullOrEmpty(_loadSceneName))
        {
            _asyncOperation = SceneManager.LoadSceneAsync(_loadSceneName);
            _asyncOperation.allowSceneActivation = false;

            while (!_asyncOperation.isDone)
            {
                //Debug.Log(_loadSceneName + " [Load Progress] : " + _asyncOperation.progress);

                yield return null;
            }
        }
    }

    public void LoadScene()
    {
        if (_loadSceneName == null)
            return;

        //XRScreenFade.instance.fadeColor = _fadeScreenColor;
        //XRScreenFade.instance.fadeTime = _loadDelayTime;
        //XRScreenFade.instance.FadeOut();

        OVRScreenFade.instance.fadeColor = _fadeScreenColor;
        OVRScreenFade.instance.fadeTime = _loadDelayTime;
        OVRScreenFade.instance.FadeOut();

        StartCoroutine(SceneLoading());
    }

    public void FadeOut()
    {
        OVRScreenFade.instance.fadeColor = _fadeScreenColor;
        OVRScreenFade.instance.fadeTime = _loadDelayTime;
        OVRScreenFade.instance.FadeOut();
    }

    private IEnumerator SceneLoading()
    {
        yield return new WaitForSeconds(OVRScreenFade.instance.fadeTime + 1f);


        //SceneManager.LoadScene(_loadSceneName);
        _asyncOperation.allowSceneActivation = true;
    }

    private string GetXMLFile()
    {
        string str = null;

        switch (LanguageManager.instance.LanguageType)
        {
            case CC_LANGUAGE_TYPE.KOR:
                str = subTitleXMLFile_KR.text;
                break;
            case CC_LANGUAGE_TYPE.ENG:
                str = subTitleXMLFile_EN.text;
                break;
        }

        return str;
    }
}
