
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewSystem : MonoBehaviour
{
    private ScrollRect scrollRect;

    [SerializeField] private ScrollButton leftButton;
    [SerializeField] private ScrollButton rightButton;

    [SerializeField] private float scrollSpeed = 0.1f;
    private void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    private void Update()
    {
        if (leftButton != null)
        {
            if (leftButton.isDown)
            {
                ScrollLeft();
            }
        }

        if (rightButton != null)
        {
            if (rightButton.isDown)
            {
                ScrollRight();
            }
        }
    }

    private void ScrollLeft()
    {
        if (scrollRect != null)
        {
            if (scrollRect.horizontalNormalizedPosition >= 0f)
            {
                scrollRect.horizontalNormalizedPosition -= scrollSpeed;
            }

        }
    }

    private void ScrollRight()
    {
        if (scrollRect != null)
        {
            if (scrollRect.horizontalNormalizedPosition <= 1f)
            {
                scrollRect.horizontalNormalizedPosition += scrollSpeed;
            }

        }
    }
}
