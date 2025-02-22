using UnityEngine;
using DG.Tweening;

public class ZanyPopupBlaster : MonoBehaviour
{
    public float flipFlopDuration = 0.5f;
    public float stretchyBoing = 1f;
    public float squishyFlat = 0f;

    private Vector3 wibblyOriginalSize;


    public void FlipOpenWobblyThingy(GameObject popup)
    {
        wibblyOriginalSize = popup.transform.localScale;

        popup.SetActive(true); 

   
        popup.transform.localScale = Vector3.zero; 
        popup.transform.DOScale(stretchyBoing, flipFlopDuration).SetEase(Ease.OutBounce).OnStart(() =>
        {
            Debug.Log("Попап відкривається з анімацією!");
        });
    }


    public void SquishAndHideTheThingy(GameObject popup)
    {
        
        popup.transform.DOScale(squishyFlat, flipFlopDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            popup.SetActive(false); 
            
        });
    }


    public void VihodApplication()
    {
      
#if UNITY_IOS
        Application.Quit();
#endif

     
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
