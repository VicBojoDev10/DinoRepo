using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlatformSpawner : MonoBehaviour
{
    public static PlatformSpawner Instance;

    [Header("Puntos de Spawn")]
    [SerializeField] private Transform transform1;
    [SerializeField] private Transform transform2;
    [SerializeField] private Transform transform3;

    [Header("Prefabs")]
    [SerializeField] private GameObject platformPrefab;

    [Header("Configuración de Tiempos")]
    [SerializeField] private float spawnRate = 2.5f;
    [SerializeField] private float initialDelayForVariedPositions = 20f;

    private bool _canStartVariedSpawn = false;
    private bool _isSpawning = false;

    private void Awake()
    {
        Instance = this;
    }
    
    public void StartSpawning()
    {
        if (_isSpawning) return;
        _isSpawning = true;

        StartCoroutine(GameplayTimerRoutine());
        InvokeRepeating(nameof(SpawnPlatform), 0f, spawnRate);
    }

    private IEnumerator GameplayTimerRoutine()
    {
        yield return new WaitForSeconds(initialDelayForVariedPositions);
        _canStartVariedSpawn = true;
    }

    private void SpawnPlatform()
    {
        Transform selectedPoint = transform3;

        if (_canStartVariedSpawn)
        {
            int rand = Random.Range(1, 4);
            if (rand == 1) selectedPoint = transform1;
            else if (rand == 2) selectedPoint = transform2;
            else selectedPoint = transform3;
        }

        GameObject newPlatform = Instantiate(platformPrefab, selectedPoint.position, Quaternion.identity);
        
        newPlatform.transform.position = new Vector3(newPlatform.transform.position.x, newPlatform.transform.position.y, 0.7f);
        
        ObjectSpawner helper = newPlatform.GetComponent<ObjectSpawner>();
        if (helper != null) helper.SpawnInhabitant();
    }

    public void StopSpawning()
    {
        CancelInvoke(nameof(SpawnPlatform));
        _isSpawning = false;
    }
}
