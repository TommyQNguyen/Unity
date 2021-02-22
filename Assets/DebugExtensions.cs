using UnityEngine;

public static class DebugExtensions
{
    public static void DrawBox(Bounds bounds, Color color)
    {
        Debug.DrawRay(bounds.center + new Vector3(-bounds.extents.x, bounds.extents.y), Vector2.down * bounds.extents.y * 2, color);
        Debug.DrawRay(bounds.center + new Vector3(bounds.extents.x, bounds.extents.y), Vector2.down * bounds.extents.y * 2, color);
        Debug.DrawRay(bounds.center + new Vector3(-bounds.extents.x, bounds.extents.y), Vector2.right * bounds.extents.x * 2, color);
        Debug.DrawRay(bounds.center + new Vector3(-bounds.extents.x, -bounds.extents.y), Vector2.right * bounds.extents.x * 2, color);
    }
}
