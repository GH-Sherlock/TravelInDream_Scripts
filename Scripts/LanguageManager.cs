using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using System;
using System.Xml;



[Serializable]
public class ScriptData
{
    public List<ScriptInfo> scripts;
}
public enum CC_LANGUAGE_TYPE
{
    KOR,
    ENG
}

public class LanguageManager : Singleton<LanguageManager>
{
    public CC_LANGUAGE_TYPE ccLanguage = CC_LANGUAGE_TYPE.KOR;


    [SerializeField] private string trackName;

    [Header("SubTitle")]
    private XmlDocument subTitleXMLDocument;
    private ScriptData subtitle;

    [SerializeField] private int fontSize;
    [SerializeField] private Color HansolColor;
    [SerializeField] private Color MolinaColor;
    [SerializeField] private Color JaewooColor;
    [SerializeField] private Color JaewooMomColor;
    [SerializeField] private Color JaewooDadColor;
    [SerializeField] private Color JaewooOldColor;
    [SerializeField] private Color HansolDadColor;
    [SerializeField] private Color WithColor;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        subTitleXMLDocument = new XmlDocument();
    }

    public void LoadText(string jsontxt, string xmltxt)
    {
        //Load subtitle xml
        subTitleXMLDocument.LoadXml(xmltxt);

        //Load Scripts Json
        subtitle = JsonUtility.FromJson<ScriptData>(jsontxt);

        PlayableDirector timeline = FindObjectOfType<PlayableDirector>();

        if (timeline == null)
            return;

        timeline.Stop();
        PlayableAsset asset = timeline.playableAsset;
        TimelineAsset timelineTrack = asset as TimelineAsset;


        foreach (var track in timelineTrack.GetOutputTracks())
        {
            if (track.name == trackName)
            {
                int count = 0;

                foreach (var clip in track.GetClips())
                {
                    TextSwitcherClip textClip = clip.asset as TextSwitcherClip;
                    textClip.template.text = ParseJSON(count);
                    textClip.template.color = SetTextColor(count);
                    textClip.template.fontSize = fontSize;
                    count++;
                }

            }
        }

        timeline.Play();
    }

    private string ParseJSON(int i)
    {
        if (i >= subtitle.scripts.Count)
            return "";

        XmlNode subtitleNode = subTitleXMLDocument.SelectSingleNode("subtitle/" + subtitle.scripts[i].subtitleKey);

        if (subtitleNode == null)
        {
            return "";
        }
        else
        {
            return subtitleNode.InnerText;
        }
    }

    private Color SetTextColor(int i)
    {
        if (i >= subtitle.scripts.Count)
            return Color.black;

        string name = subtitle.scripts[i].name;

        switch (name)
        {
            case "Hansol": return HansolColor;
            case "Molina": return MolinaColor;
            case "Jaewoo": return JaewooColor;
            case "JaewooMom": return JaewooMomColor;
            case "JaewooDad": return JaewooDadColor;
            case "JaewooOld": return JaewooOldColor;
            case "HansolDad": return HansolDadColor;
            case "With": return WithColor;
            default: return Color.black;
        }
    }

    public void LanguageActivate()
    { 
        PlayableDirector timeline = FindObjectOfType<PlayableDirector>();

        if (timeline == null)
            return;

        
        PlayableAsset asset = timeline.playableAsset;
        TimelineAsset timelineTrack = asset as TimelineAsset;


        foreach (var track in timelineTrack.GetOutputTracks())
        {
            if (track.name == trackName)
            {
                //track.muted = false;

                foreach(var clip in track.GetClips())
                {
                    TextSwitcherClip textClip = clip.asset as TextSwitcherClip;
                    Debug.Log(textClip.template.text);
                }

            }
        }

    }

    public CC_LANGUAGE_TYPE LanguageType
    {
        get { return ccLanguage; }
        set { ccLanguage = value; }
    }
}
