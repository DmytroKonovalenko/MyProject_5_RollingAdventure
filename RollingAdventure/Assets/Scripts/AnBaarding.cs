using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class TutorialPopup : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject popup;
    public Image tutorialImage;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public Button nextButton;
    public Button prevButton;

    [Header("Tutorial Data")]
    public Sprite[] tutorialSprites;
    public string[] tutorialTitles;
    public string[] tutorialDescriptions;

    private int currentSlide = 0;
    private bool isFirstTime;

    private void Start()
    {
        isFirstTime = PlayerPrefs.GetInt("TutorialShown", 0) == 0;

        if (isFirstTime)
        {
            ShowPopup();
            PlayerPrefs.SetInt("TutorialShown", 1);
            PlayerPrefs.Save();
        }
        else
        {
            popup.SetActive(false);
        }
    }

    public void ShowPopup()
    {
        popup.SetActive(true);
        popup.transform.localScale = Vector3.zero;
        popup.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);

        currentSlide = 0;
        UpdateSlide();
    }

    private void UpdateSlide()
    {
        tutorialImage.DOFade(0f, 0.2f).OnComplete(() =>
        {
            tutorialImage.sprite = tutorialSprites[currentSlide];
            tutorialImage.DOFade(1f, 0.2f);
        });

        titleText.DOFade(0f, 0.2f).OnComplete(() =>
        {
            titleText.text = tutorialTitles[currentSlide];
            titleText.DOFade(1f, 0.2f);
        });

        descriptionText.DOFade(0f, 0.2f).OnComplete(() =>
        {
            descriptionText.text = tutorialDescriptions[currentSlide];
            descriptionText.DOFade(1f, 0.2f);
        });

        prevButton.interactable = currentSlide > 0;
    }

    public void NextSlide()
    {
        if (currentSlide < tutorialSprites.Length - 1)
        {
            currentSlide++;
            UpdateSlide();
        }
        else
        {
            ClosePopup();
        }
    }

    public void PrevSlide()
    {
        if (currentSlide > 0)
        {
            currentSlide--;
            UpdateSlide();
        }
    }

    public void ClosePopup()
    {
        popup.transform.DOScale(0f, 0.5f).SetEase(Ease.InBack).OnComplete(() => popup.SetActive(false));
    }
}
