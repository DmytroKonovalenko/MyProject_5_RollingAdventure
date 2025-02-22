using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Bytrom : MonoBehaviour
{
    private Button bonkButton;
    [SerializeField] private float squizzleAmount = 1.05f;
    [SerializeField] private float squishDuration = 0.2f;
    [SerializeField] private float bounceHeight = 10f;
    [SerializeField] private float bounceDuration = 0.3f;

    private Vector3 baseScale;
    private Vector3 basePosition;
    private SoundManager soundMaster;

    private void Awake()
    {
        bonkButton = GetComponent<Button>();
        if (bonkButton == null)
        {
            enabled = false;
            return;
        }

        baseScale = transform.localScale;
        basePosition = transform.localPosition;

        soundMaster = FindObjectOfType<SoundManager>();

        AssignButtonListeners();
    }

    private void AssignButtonListeners()
    {
        bonkButton.onClick.AddListener(PerformButtonAnimation);
    }

    public void PerformButtonAnimation()
    {
        soundMaster?.PlaySoundEffect();

        Sequence animationSequence = DOTween.Sequence();

        animationSequence
            .Append(transform.DOScale(baseScale * squizzleAmount, squishDuration).SetEase(Ease.OutQuad))
            .Append(transform.DOScale(baseScale, squishDuration).SetEase(Ease.OutElastic))
            .Join(transform.DOLocalMoveY(basePosition.y + bounceHeight, bounceDuration / 2).SetEase(Ease.OutQuad))
            .Append(transform.DOLocalMoveY(basePosition.y, bounceDuration / 2).SetEase(Ease.InQuad));
    }

    public void ResetButtonState()
    {
        if (bonkButton != null)
        {
            bonkButton.onClick.RemoveListener(PerformButtonAnimation);
        }
        transform.localScale = baseScale;
        transform.localPosition = basePosition;
    }

    private void OnDestroy()
    {
        ResetButtonState();
    }
}
