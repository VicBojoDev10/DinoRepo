using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SkinItemUI : MonoBehaviour
{
        public Image iconImage;
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI costText;
        public Button purchaseButton;

        private SkinSO currentData;

        private PopUpUi popUp;
        private StoreUI storeUI;

        private void Start()
        {
                popUp = FindFirstObjectByType<PopUpUi>(FindObjectsInactive.Include);
                storeUI = FindFirstObjectByType<StoreUI>(FindObjectsInactive.Include);
        }

        public void Setup(SkinSO data)
        {
                currentData = data;

                iconImage.sprite = data.itemIcon;
                nameText.text = data.itemName;
                costText.text = data.cost.ToString();
                
                purchaseButton.onClick.RemoveAllListeners();
                purchaseButton.onClick.AddListener(OnPurchaseClicked);
        }

        private void OnPurchaseClicked()
        {
                if (CurrencyManager.Instance.SpendScales(currentData.cost))
                {
                        Debug.Log($"You Bought {currentData.itemName} Skin!");
                }
                else
                {
                        if (popUp != null)
                        {
                                popUp.YesButton.onClick.RemoveAllListeners();
                                popUp.NoButton.onClick.RemoveAllListeners();

                                popUp.YesButton.onClick.AddListener(() =>
                                {
                                        popUp.Hide();
                                        if(storeUI != null) storeUI.Show();
                                });
                                popUp.NoButton.onClick.AddListener(() =>
                                {
                                        popUp.Hide();
                                });
                                popUp.Show();
                        }
                }
        }
}
