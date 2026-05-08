using Unity.VisualScripting;
using UnityEngine;
using Vic.Code;

public class SkinsUI : UIWindow
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform itemContainer;

    [SerializeField] private SkinSO[] availableSkins;

    [SerializeField] private UIWindow upgradesUI;
    public override void Initialize()
    {
        base.Initialize();
       SpawnItems();
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
}
