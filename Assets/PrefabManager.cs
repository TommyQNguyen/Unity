using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public enum Global
    {
        Barrel,
        Bomb,
        Bullet,
        MonsterGreen,
        MonsterBlue,
        MonsterRed,
        PickupBomb,
        PickupHealth,
        Player,
        Spawner,

        Count
    }

    public enum Vfx
    {
        Explosion,
        ExplosionBig,
        ExplosionSmall,

        Count
    }

    public GameObject[] GlobalGameObjects;
    public GameObject[] VfxGameObjects;

    private void Awake()
    {
        GlobalGameObjects = Resources.LoadAll<GameObject>("shooter/prefabs/global");
        Debug.Assert((int)Global.Count == GlobalGameObjects.Length,
            "PrefabManager : Global enum length (" + (int)Global.Count + ") does not match Resources folder (" + GlobalGameObjects.Length + ")");

        VfxGameObjects = Resources.LoadAll<GameObject>("shooter/prefabs/vfx");
        Debug.Assert((int)Vfx.Count == VfxGameObjects.Length, "PrefabManager : Vfx enum length (" + (int)Vfx.Count + ") does not match Resources folder (" + VfxGameObjects.Length + ")");
    }

    public GameObject Spawn(Global global, Transform parent)
    {
        return Instantiate(GlobalGameObjects[(int)global], parent);
    }

    public GameObject Spawn(Global global, Vector3 position)
    {
        return Instantiate(GlobalGameObjects[(int)global], position, Quaternion.identity);
    }

    public GameObject Spawn(Global global, Vector3 position, Vector3 rotation)
    {
        return Instantiate(GlobalGameObjects[(int)global], position, Quaternion.Euler(rotation));
    }

    public GameObject Spawn(Global global, Vector3 position, Quaternion rotation)
    {
        return Instantiate(GlobalGameObjects[(int)global], position, rotation);
    }

    public GameObject Spawn(Vfx vfx, Transform parent)
    {
        return Instantiate(VfxGameObjects[(int)vfx], parent);
    }

    public GameObject Spawn(Vfx vfx, Vector3 position)
    {
        return Instantiate(VfxGameObjects[(int)vfx], position, Quaternion.identity);
    }

    public GameObject Spawn(Vfx vfx, Vector3 position, Vector3 rotation)
    {
        return Instantiate(VfxGameObjects[(int)vfx], position, Quaternion.Euler(rotation));
    }

    public GameObject Spawn(Vfx vfx, Vector3 position, Quaternion rotation)
    {
        return Instantiate(VfxGameObjects[(int)vfx], position, rotation);
    }
}
