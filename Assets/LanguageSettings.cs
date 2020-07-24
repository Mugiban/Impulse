using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LanguageSettings : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI languageText;
    public enum Languages
    {
        EN,
        ES,
    }

    public Languages currentLanguage;
    private int currentIndex;

    public List<Languages> allLanguages;

    private void Awake()
    {
        allLanguages = new List<Languages>
        {
            Languages.EN, Languages.ES,
        };
    }

    public void ChangeLanguageText(Languages language)
    {
        currentLanguage = language;
        languageText.text = currentLanguage.ToString();
    }

    public void MoveLeft()
    {
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = allLanguages.Count - 1;
        }
        SetCurrentLanguage();
    }

    public void MoveRight()
    {
        currentIndex++;
        if (currentIndex > allLanguages.Count - 1)
        {
            currentIndex = 0;
        }

        SetCurrentLanguage();
    }

    void SetCurrentLanguage()
    {
        currentLanguage = allLanguages[currentIndex];
        //LanguageManager.ChangeLanguage(currentLanguage);
        Debug.Log("Language changed to : "+currentLanguage);
        ChangeLanguageText(currentLanguage);
    }
}
