using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Transform spawnPointOnPlatform;

    public void SpawnInhabitant()
    {
        if (enemyPrefabs.Length == 0) return;
        
        if (Random.value > 0.5f)
        {
            GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], 
                spawnPointOnPlatform.position, 
                Quaternion.identity);
            
            enemy.transform.SetParent(this.transform);
            
            enemy.transform.localRotation = Quaternion.identity;
        }
    }
}