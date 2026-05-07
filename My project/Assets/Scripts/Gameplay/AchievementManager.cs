using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance { get; private set; }

    private int totalJumps = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        
        totalJumps = PlayerPrefs.GetInt("totalJumps", 0);
    }

    public void AddJump()
    {
        totalJumps++;
        PlayerPrefs.SetInt("totalJumps", totalJumps);

        CheckJumpAchievements();
    }

    private void CheckJumpAchievements()
    {
        if (totalJumps >= 50 && PlayerPrefs.GetInt("Ach_Jumper1", 0) == 0)
        {
            UnlockAchievement("Ach_Jumper1" , "Big Jumper!");
        }
    }

    private void UnlockAchievement(string id, string name)
    {
        PlayerPrefs.SetInt(id, 1);
        Debug.Log($"Logro Desbloqueado: {name}");
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
