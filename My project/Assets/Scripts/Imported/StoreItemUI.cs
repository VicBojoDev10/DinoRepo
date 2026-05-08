using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class StoreItemUI : MonoBehaviour
{
    public TextMeshProUGUI scalesText;
    public TextMeshProUGUI costText;
    public Image iconImage;
    public Button buyButton;
    
    private StoreBundleSO bundleData;

    public void Setup(StoreBundleSO data)
    {
        bundleData =  data;
        scalesText.text = $"{data.scalesAmount} Scales";
        costText.text = $"{data.priceUSD}";
        iconImage.sprite = data.bundleIcon;
        
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(OnBuyClicked);
    }

    private void OnBuyClicked()
    {
        CurrencyManager.Instance.BuyStoreBundle(bundleData.scalesAmount, bundleData.priceUSD);
        Debug.Log($"You Bought {bundleData.scalesAmount} Scales at {bundleData.priceUSD}");
    }
}
