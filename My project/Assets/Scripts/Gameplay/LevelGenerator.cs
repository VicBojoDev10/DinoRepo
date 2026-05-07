using UnityEngine;
using System.Collections.Generic;
public class LevelGenerator : MonoBehaviour
{
    [Header("Prefabs")]
    public List<Platform> platformPrefabs;
    public GameObject wormEnemyPrefab;

    [Header("Configuration")] public float scrollSpeed = 8f;
    public float spawnXPosition = 20f;
    public float destroyXPosition = -20f;
    public float gapChance = 0.2f;
    public float enemySpawnChance = 0.5f;
    
    private List<Platform> activePlatforms = new List<Platform>();
    private Vector3 nextSpawnPoint;

    void Start()
    {
        nextSpawnPoint = Vector3.zero;
        for (int i = 0; i < 5; i++)
        {
            SpawnPlatform();
        }
    }

    void Update()
    {
        MoveLevel();
    }

    private void MoveLevel()
    {
        for (int i = activePlatforms.Count - 1; i >= 0; i--)
        {
            Platform platform = activePlatforms[i];
            platform.transform.Translate(Vector2.left * scrollSpeed * Time.deltaTime);

            if (platform.transform.position.x + platform.width < destroyXPosition)
            {
                activePlatforms.RemoveAt(i);
                Destroy(platform.gameObject);
                SpawnPlatform();
            }
        }
    }

    private void SpawnPlatform()
    {
        if (Random.value < gapChance && activePlatforms.Count > 2)
        {
            float gapWidth = Random.Range(2f, 5f);
            nextSpawnPoint.x += gapWidth;
        }

        Platform prefab = platformPrefabs[Random.Range(0, platformPrefabs.Count)];
        Platform newPlatform = Instantiate(prefab, nextSpawnPoint, Quaternion.identity);
        
        newPlatform.SetUpEnemies(wormEnemyPrefab, enemySpawnChance);
        
        activePlatforms.Add(newPlatform);
        
        nextSpawnPoint.x += newPlatform.width;
    }
}
