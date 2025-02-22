using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneZagruzka : MonoBehaviour
{
    [Header("Loading Popup")]
   [SerializeField] private CanvasGroup loadingPopup;
    [SerializeField] private Transform loadingPanel;

 

    public void StartGame()
    {
        

        if (loadingPopup != null && loadingPanel != null)
        {
            StartCoroutine(ShowLoadingAndLoadScene());
        }
        else
        {
            Debug.LogError("Завантажувальні елементи не знайдені! Завантажуємо сцену без анімації...");
            SceneManager.LoadScene(1);
        }
    }

    private IEnumerator ShowLoadingAndLoadScene()
    {
        loadingPopup.gameObject.SetActive(true);
        loadingPopup.alpha = 0;
        loadingPopup.DOFade(1f, 0.5f);
        loadingPanel.localScale = Vector3.zero;
        loadingPanel.DOScale(1f, 0.5f).SetEase(Ease.OutBack);

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(1);
    }
}
