using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;
using DG.Tweening;

// create a serializable dictionary which lookup key is the UI's name and it values is a refernece to the UI panel;s CanvasGroup component
[System.Serializable]
public class UIpanelDictionary :
SerializableDictionaryBase<string, CanvasGroup>
{ }

// declare as singleton instead of monobehaviour class.
public class UIController : Singleton<UIController>
{
    [SerializeField] UIpanelDictionary uiPanels;

    CanvasGroup currentPanel;

    void Awake()
    {
        base.Awake();
        ResetAllUI();
    }

    void ResetAllUI()
    {
        foreach (CanvasGroup panel in uiPanels.Values)
        {
            panel.gameObject.SetActive(false);
        }
    }

    public static void ShowUI(string name)
    {
        Instance?._ShowUI(name);

    }

    void _ShowUI(string name)
    {
        CanvasGroup panel;
        if (uiPanels.TryGetValue(name, out panel))
        {
            ChangeUI(uiPanels[name]);
        }
        else
        {
            ScreenLog.Log("Undefined ui panel " + name);
        }
    }

    void ChangeUI(CanvasGroup panel)
    {
        if (panel == currentPanel)
        {
            return;
        }
        if (currentPanel)
        {
            FadeOut(currentPanel);
            //currentPanel.gameObject.SetActive(false);
        }
        currentPanel = panel;
        if (panel)
        {
            FadeIn(panel);
            //panel.gameObject.SetActive(false);
        }
    }

    void FadeIn(CanvasGroup panel)
    {
        panel.gameObject.SetActive(true);
        panel.DOFade(1f, 0.5f);
    }

    void FadeOut(CanvasGroup panel)
    {
        panel.DOFade(0f, 0.5f).OnComplete(() => panel.gameObject.SetActive(false));
    }

}
