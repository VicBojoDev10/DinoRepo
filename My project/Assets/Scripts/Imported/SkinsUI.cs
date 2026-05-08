using Unity.VisualScripting;
using UnityEngine;
using Vic.Code;

public class SkinsUI : UIWindow
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform itemContainer;
    
    public GameObject skinsListPanel;
    public GameObject skinDetailsPanel;
    public GameObject backToUpgradesButton;
    public GameObject backToListButton;
    

    [SerializeField] private SkinSO[] availableSkins;

    [SerializeField] private UIWindow upgradesUI;
    public override void Initialize()
    {
        base.Initialize();
        ShowSkinsList();
        SpawnItems();
    }

    public void ShowSkinsList()
    {
        skinsListPanel.SetActive(true);
        skinDetailsPanel.SetActive(false);
        
        backToUpgradesButton.SetActive(true);
        backToListButton.SetActive(false);
    }

    public void ShowSkinsDetails()
    {
        skinsListPanel.SetActive(false);
        skinDetailsPanel.SetActive(true);
        
        backToUpgradesButton.SetActive(false);
        backToListButton.SetActive(true);
    }

    private void SpawnItems()
    {
        foreach (ItemData itemData in itemContainer)
        {
            Destroy(itemData.GameObject());
        }

        foreach (SkinSO skinData in availableSkins)
        {
            GameObject skin = Instantiate(itemPrefab, itemContainer);

            SkinItemUI itemUI = skin.GetComponent<SkinItemUI>();
            if (itemUI != null)
            {
                itemUI.Setup(skinData);
            }
        }
    }

    public void OnReturnButtonClick()
    {
        this.Hide();
        if (upgradesUI != null)
        {
            upgradesUI.Show();
        }
    }

    public void OnBackToSkinsClick()
    {
        ShowSkinsList();
    }
}
