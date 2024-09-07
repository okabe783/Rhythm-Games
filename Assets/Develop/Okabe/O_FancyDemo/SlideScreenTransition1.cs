using FancyScrollView;
using UnityEngine;
using UnityEngine.UI;

public class SlideScreenTransition : MonoBehaviour
{
    [SerializeField] private RectTransform _targetTransform;
    [SerializeField] private GraphicRaycaster _graphicRayCaster;

    private const float Duration = 0.3f;

    private bool shouldAnimate, isOutAnimation;
    private float timer, startX, endX;

    public void In(MovementDirection direction) => Animate(direction, false);

    public void Out(MovementDirection direction) => Animate(direction, true);

    void Animate(MovementDirection direction, bool isOut)
    {
        if (shouldAnimate)
        {
            return;
        }

        timer = Duration;
        isOutAnimation = isOut;
        shouldAnimate = true;
        _graphicRayCaster.enabled = false;

        if (!isOutAnimation)
        {
            gameObject.SetActive(true);
        }

        switch (direction)
        {
            case MovementDirection.Left:
                endX = -_targetTransform.rect.width;
                break;

            case MovementDirection.Right:
                endX = _targetTransform.rect.width;
                break;

            default:
                Debug.LogWarning("Example only support horizontal direction.");
                break;
        }

        startX = isOutAnimation ? 0 : -endX;
        endX = isOutAnimation ? endX : 0;

        UpdatePosition(0f);
    }

    void Update()
    {
        if (!shouldAnimate)
        {
            return;
        }

        timer -= Time.deltaTime;

        if (timer > 0)
        {
            UpdatePosition(1f - timer / Duration);
            return;
        }

        shouldAnimate = false;
        _graphicRayCaster.enabled = true;

        if (isOutAnimation)
        {
            gameObject.SetActive(false);
        }

        UpdatePosition(1f);
    }

    void UpdatePosition(float position)
    {
        var x = Mathf.Lerp(startX, endX, position);
        _targetTransform.anchoredPosition = new Vector2(x, _targetTransform.anchoredPosition.y);
    }
}
