using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance { get; private set; }

    [SerializeField] private float totalMoneySpent = 0f;
    public bool adsRemoved = false;

    public string currencyName = "Scales";
    public Sprite currencyIcon;

    [SerializeField] private int currentScales = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        
        currentScales = PlayerPrefs.GetInt("PlayerScales", 0);
        totalMoneySpent = PlayerPrefs.GetFloat("PlayerTotalMoney", 0f);
        adsRemoved = PlayerPrefs.GetInt("AdsRemoved, 0") == 1;
    }
    public int GetScales() => currentScales;
    
    public void AddScales(int amount)
    {
        currentScales += amount;
        SaveCurrency();
    }

    public bool SpendScales(int amount)
    {
        if (currentScales >= amount)
        {
            currentScales -= amount;
            SaveCurrency();
            return true;
        }
        return false;
    }

    private void SaveCurrency()
    {
        PlayerPrefs.SetInt("PlayerScales", currentScales);
    }

    public void BuyStoreBundle(int scalesAmount, float priceInUSD)
    {
        AddScales(scalesAmount);
        
        totalMoneySpent += priceInUSD;
        PlayerPrefs.SetFloat("PlayerTotalMoney", totalMoneySpent);

        CheckAdRemoval();
    }

    private void CheckAdRemoval()
    {
        if (!adsRemoved && totalMoneySpent >= 4.99f)
        {
            adsRemoved = true;
            PlayerPrefs.SetInt("AdsRemoved", 1);
            Debug.Log(PlayerPrefs.GetInt("AdsRemoved"));
            
            // AdManager.Instance.DisableAds();
        }
    }
}
