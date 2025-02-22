using UnityEngine;
using DG.Tweening;

public class LoadingWin : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        Invoke(nameof(CloseLoadingWindow), 2f);
    }

    private void CloseLoadingWindow()
    {
        canvasGroup.DOFade(0f, 0.5f).OnComplete(() => gameObject.SetActive(false));
    }
}