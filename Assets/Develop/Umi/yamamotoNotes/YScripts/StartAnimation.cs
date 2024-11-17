using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary> ゲームスタート時にジャケットとReadyGoを表示する </summary>
public class StartAnimation : MonoBehaviour
{
    [SerializeField] private Image _jacket;
    [SerializeField] private Sprite _ready;
    [SerializeField] private Sprite _go;

    private Image _sign;
    
    private IEnumerator Start()
    {
        if (gameObject.transform.GetChild(0).TryGetComponent<Image>(out var image)) _sign = image;
        
        if (_sign)
        {
            yield return new WaitForSeconds(1);
            _jacket.gameObject.SetActive(true);
            yield return FadeOut(3);

            _sign.gameObject.SetActive(true);
            _sign.sprite = _ready;
            yield return new WaitForSeconds(1);

            _sign.sprite = _go;
            yield return new WaitForSeconds(1);
            
            gameObject.SetActive(false);
        }
    }

    private IEnumerator FadeOut(float time)
    {
        Graphic[] graphics = _jacket.GetComponentsInChildren<Graphic>();
        yield return new WaitForSeconds(1);

        var color = _jacket.color;

        for (var i = 0f; i < time; i += Time.deltaTime)
        {
            color.a = Mathf.MoveTowards(color.a, 0, i / time);
            _jacket.color = color;
            foreach (var graphic in graphics) SetAlpha(graphic, color.a); 
            yield return null;
        }
    }
    
    private void SetAlpha(Graphic graphic, float alpha)
    {
        if (graphic == null) return;

        var color = graphic.color;
        color.a = alpha;
        graphic.color = color;
    }
}
