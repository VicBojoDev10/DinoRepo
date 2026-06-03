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
 
    [Header("Prefab")]
    [SerializeField] private GameObject platformPrefab;
 
    [Header("Plataforma inicial de escena")]
    [Tooltip("Arrastra aquí la plataforma que ya existe en la escena.")]
    [SerializeField] private PlatformMovement initialPlatform;
 
    [Header("Configuración")]
    [SerializeField] private float spawnInterval      = 3.5f;
    [SerializeField] private float safeZoneDuration   = 10f;
    [SerializeField] private float platformSpeed      = 6f;
    [SerializeField] private float platformWidth      = 6f;
    [SerializeField] private float minGap             = 3f;
 
    private bool _canSpawnVaried;
    private bool _isSpawning;
    private readonly List<PlatformMovement> _activePlatforms = new List<PlatformMovement>();
    
 
    private void Awake()
    {
        Instance = this;
    }
 
    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }
 
 
    public void StartSpawning()
    {
        if (_isSpawning) return;
        _isSpawning     = true;
        _canSpawnVaried = false;
 
        if (initialPlatform != null)
        {
            _activePlatforms.Add(initialPlatform);
            initialPlatform.Activate();
        }
 
        StartCoroutine(SpawnLoop());
    }
 
    public void StopSpawning()
    {
        _isSpawning = false;
        StopAllCoroutines();
 
        _activePlatforms.RemoveAll(p => p == null);
        foreach (PlatformMovement pm in _activePlatforms)
            pm.Deactivate();
 
        _activePlatforms.Clear();
    }
 
    public void UnregisterPlatform(PlatformMovement pm)
    {
        _activePlatforms.Remove(pm);
    }
    
 
    private IEnumerator SpawnLoop()
    {
        float elapsed = 0f;
        yield return new WaitForSeconds(spawnInterval);
 
        while (_isSpawning)
        {
            yield return WaitForGap();
            if (!_isSpawning) yield break;
 
            SpawnPlatform();
 
            elapsed += spawnInterval;
            if (!_canSpawnVaried && elapsed >= safeZoneDuration)
            {
                _canSpawnVaried = true;
                Debug.Log("[PlatformSpawner] Zona segura terminada.");
            }
 
            yield return new WaitForSeconds(spawnInterval);
        }
    }
 
    private IEnumerator WaitForGap()
    {
        if (_activePlatforms.Count == 0) yield break;
 
        PlatformMovement last = null;
        for (int i = _activePlatforms.Count - 1; i >= 0; i--)
        {
            if (_activePlatforms[i] != null) { last = _activePlatforms[i]; break; }
        }
        if (last == null) yield break;
 
        float spawnX = transform3.position.x;
 
        while (last != null)
        {
            float rightEdge = last.transform.position.x + platformWidth * 0.5f;
            if (spawnX - rightEdge >= minGap) break;
            yield return null;
        }
    }
 
    private void SpawnPlatform()
    {
        Transform point = ChoosePoint();
 
        GameObject go = Instantiate(
            platformPrefab,
            new Vector3(point.position.x, point.position.y, 1.1f),
            Quaternion.identity
        );
 
        PlatformMovement pm = go.GetComponent<PlatformMovement>();
        if (pm != null)
        {
            _activePlatforms.Add(pm);
            pm.Activate();
        }
 
        ObjectSpawner s = go.GetComponent<ObjectSpawner>();
        if (s != null) s.SpawnInhabitant();
    }
 
    private Transform ChoosePoint()
    {
        if (!_canSpawnVaried) return transform3;
        return Random.Range(0, 3) switch
        {
            0 => transform1,
            1 => transform2,
            _ => transform3
        };
    }
}
