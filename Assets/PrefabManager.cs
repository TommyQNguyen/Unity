using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public enum PlatformerGlobal
    {
        Goomba,
        KoopaWings,
        Mario,

        Count,
    }
    public enum ShooterGlobal
    {
        Barrel,
        Bomb,
        Bullet,
        Explosion,
        MonsterGreen,
        //MonsterBlue,
        MonsterRed,
        PickupBomb,
        PickupHealth,
        Player,
        Spawner,

        Count
    }

    //public enum Vfx
    //{
    //    Explosion,
    //    ExplosionBig,
    //    ExplosionSmall,

    //    Count
    //}

    public GameObject[] PlatformerGlobalGameObjects;
    public GameObject[] ShooterGlobalGameObjects;
    //public GameObject[] VfxGameObjects;

    private void Awake()
    {
        PlatformerGlobalGameObjects = Resources.LoadAll<GameObject>("platformer/prefabs/global");
        Debug.Assert((int)PlatformerGlobal.Count == PlatformerGlobalGameObjects.Length,
            "PrefabManager : Global enum length (" + (int)PlatformerGlobal.Count + ") does not match Resources folder (" + PlatformerGlobalGameObjects.Length + ")");

        //VfxGameObjects = Resources.LoadAll<GameObject>("shooter/prefabs/vfx");
        //Debug.Assert((int)Vfx.Count == VfxGameObjects.Length, "PrefabManager : Vfx enum length (" + (int)Vfx.Count + ") does not match Resources folder (" + VfxGameObjects.Length + ")");
    }

    public GameObject Spawn(ShooterGlobal global, Transform parent)
    {
        return Instantiate(ShooterGlobalGameObjects[(int)global], parent);
    }

    public GameObject Spawn(ShooterGlobal global, Vector3 position)
    {
        return Instantiate(ShooterGlobalGameObjects[(int)global], position, Quaternion.identity);
    }

    public GameObject Spawn(ShooterGlobal global, Vector3 position, Vector3 rotation)
    {
        return Instantiate(ShooterGlobalGameObjects[(int)global], position, Quaternion.Euler(rotation));
    }

    public GameObject Spawn(ShooterGlobal global, Vector3 position, Quaternion rotation)
    {
        return Instantiate(ShooterGlobalGameObjects[(int)global], position, rotation);
    }

    // Spawn pour Platformer

    public GameObject PlatformerSpawn(PlatformerGlobal global, Transform parent)
    {
        return Instantiate(PlatformerGlobalGameObjects[(int)global], parent);
    }

    public GameObject PlatformerSpawn(PlatformerGlobal global, Vector3 position)
    {
        return Instantiate(PlatformerGlobalGameObjects[(int)global], position, Quaternion.identity);
    }

    public GameObject PlatformerSpawn(PlatformerGlobal global, Vector3 position, Vector3 rotation)
    {
        return Instantiate(PlatformerGlobalGameObjects[(int)global], position, Quaternion.Euler(rotation));
    }

    public GameObject PlatformerSpawn(PlatformerGlobal global, Vector3 position, Quaternion rotation)
    {
        return Instantiate(PlatformerGlobalGameObjects[(int)global], position, rotation);
    }

    //public GameObject Spawn(Vfx vfx, Transform parent)
    //{
    //    return Instantiate(VfxGameObjects[(int)vfx], parent);
    //}

    //public GameObject Spawn(Vfx vfx, Vector3 position)
    //{
    //    return Instantiate(VfxGameObjects[(int)vfx], position, Quaternion.identity);
    //}

    //public GameObject Spawn(Vfx vfx, Vector3 position, Vector3 rotation)
    //{
    //    return Instantiate(VfxGameObjects[(int)vfx], position, Quaternion.Euler(rotation));
    //}

    //public GameObject Spawn(Vfx vfx, Vector3 position, Quaternion rotation)
    //{
    //    return Instantiate(VfxGameObjects[(int)vfx], position, rotation);
    //}
}
