using UnityEngine;

public class SkinsManager : MonoBehaviour
{
    
    public static SkinsManager Instance;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
    }

    
}

public class RunTimeSkin
{
    public string ID;
    public bool IsUnlocked;

    public RunTimeSkin(string iD, bool isUnlocked)
    {
        //IsUnlocked = this.isUnlocked;
        //ID = this.iD;
    }
}
