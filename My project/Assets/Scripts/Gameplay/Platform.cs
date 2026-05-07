using UnityEngine;

public class Platform : MonoBehaviour
{
    [Header("Settings")] public float width = 20f;

    public Transform[] enemySpawnPoints;

    public void SetUpEnemies(GameObject enemyPrefab, float SpawnChance)
    {
        foreach (Transform point in enemySpawnPoints)
        {
            if (point.childCount > 0)
            {
                foreach (Transform child in point) Destroy(child.gameObject);
            }

            if (Random.value < SpawnChance)
            {
                Instantiate(enemyPrefab,point.position, Quaternion.identity, point);
            }
        }
    }
}
