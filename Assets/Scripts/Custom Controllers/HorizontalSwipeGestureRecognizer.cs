using System;
using UnityEngine;

public class HorizontalSwipeGestureRecognizer : MonoBehaviour
{
    public enum SwipeDirection
    {
        Left,
        Right
    }

    private Vector2 mouseDownPosition;
    private bool isMouseDown;

    public float minDistanceThreshold = 20f;

    public Action<SwipeDirection> SwipeCallback; 

    private void Update()
    {
        if (SwipeCallback == null) { return; }

        if (Input.GetMouseButtonDown(0))
        {
            mouseDownPosition = Input.mousePosition;
            isMouseDown = true;
        }

        if (Input.GetMouseButtonUp(0) && isMouseDown)
        {
            isMouseDown = false;

            Vector2 endPosition = Input.mousePosition;

            var line = endPosition - mouseDownPosition;

            if (line.magnitude < minDistanceThreshold) { return; }

            var angle = (Mathf.Atan2(line.y, line.x) * Mathf.Rad2Deg + 360) % 360;

            if (angle < 90 || angle > 270)
            {
                SwipeCallback(SwipeDirection.Left);
            }
            else
            {
                SwipeCallback(SwipeDirection.Right);
            }
        }
    }
}
