using UnityEngine.EventSystems;
using UnityEngine;

public static class RectTransformExtension
{
    private static readonly Vector3[] _corners = new Vector3[4];

    public static Rect GetScreenRect(this RectTransform self, PointerEventData data)
    {
        return self.GetScreenRect(data.pressEventCamera);
    }

    public static Rect GetScreenRect(this RectTransform self)
    {
        var canvas = self.GetComponentInParent<Canvas>();
        return self.GetScreenRect(canvas.worldCamera);
    }

    public static Rect GetScreenRect(this RectTransform self, Camera camera)
    {
        self.GetWorldCorners(_corners);
        if (camera != null)
        {
            _corners[0] = RectTransformUtility.WorldToScreenPoint(camera, _corners[0]);
            _corners[2] = RectTransformUtility.WorldToScreenPoint(camera, _corners[2]);
        }

        var rect = new Rect
        {
            x = _corners[0].x,
            y = _corners[0].y
        };
        rect.width = _corners[2].x - rect.x;
        rect.height = _corners[2].y - rect.y;
        return rect;
    }

    /// <summary>
    /// Converts RectTransform.rect's local coordinates to world space
    /// Usage example RectTransformExt.GetWorldRect(myRect, Vector2.one);
    /// </summary>
    /// <returns>The world rect.</returns>
    /// <param name="rt">RectangleTransform we want to convert to world coordinates.</param>
    /// <param name="scale">Optional scale pulled from the CanvasScaler. Default to using Vector2.one.</param>
    static public Rect GetWorldRect(this RectTransform rt, Vector2 scale)
    {
        // Convert the rectangle to world corners and grab the top left
        Vector3[] corners = new Vector3[4];
        rt.GetWorldCorners(corners);
        Vector3 topLeft = corners[0];

        // Rescale the size appropriately based on the current Canvas scale
        Vector2 scaledSize = new Vector2(scale.x * rt.rect.size.x, scale.y * rt.rect.size.y);

        return new Rect(topLeft, scaledSize);
    }
}