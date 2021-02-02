using UnityEngine;

static public class Vector3Extensions
{
    public static Quaternion ToQuaternion(this Vector3 vector)
    {
        var quaternion = Quaternion.Euler(vector);
        return quaternion;
    }
}