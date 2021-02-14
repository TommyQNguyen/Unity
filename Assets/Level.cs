using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelSpawnObject
{
    public GameObject GameObject;
    // creer property pour Timer
    public float SpawnTime;

}

public class Level : MonoBehaviour
{
    public BoxCollider2D LevelCollider;
    public List<LevelSpawnObject> LevelSpawnObjects;
    public SpawnTime SpawnTime { get; private set; }

    //public GameObject MonsterGameObject;
    //public float MonsterTimer = 3;

    // Update is called once per frame

    public void Awake()
    {
        SpawnTime = GetComponent<SpawnTime>(); 
    }
    void Update()
    {
        // A Faire
        foreach (var levelSpawnObject in LevelSpawnObjects)
        {

            levelSpawnObject.SpawnTime = levelSpawnObject.SpawnTime - Time.deltaTime;

            if (levelSpawnObject.SpawnTime <= 0)
            {

                var x = Random.Range(LevelCollider.bounds.min.x, LevelCollider.bounds.max.x);
                var y = Random.Range(LevelCollider.bounds.min.y, LevelCollider.bounds.max.y);

                // Pour spawner le monster a une position random dans le Level
                Instantiate(levelSpawnObject.GameObject, new Vector3(x, y, 0), Quaternion.identity);
                levelSpawnObject.SpawnTime = SpawnTime.GreenMonsterTimer;
            }
        }








        //MonsterTimer = MonsterTimer - Time.deltaTime;
        //if (MonsterTimer <= 0)
        //{

        //    var x = Random.Range(LevelCollider.bounds.min.x, LevelCollider.bounds.max.x);
        //    var y = Random.Range(LevelCollider.bounds.min.y, LevelCollider.bounds.max.y);

        //    Pour spawner le monster a une position random dans le Level
        //    Instantiate(MonsterGameObject, new Vector3(x, y, 0), Quaternion.identity);
        //    MonsterTimer = 3;
        //}
    }
}
