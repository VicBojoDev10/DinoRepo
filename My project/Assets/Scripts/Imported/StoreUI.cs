using UnityEngine;
using Vic.Code;
public class StoreUI : UIWindow
{
    [SerializeField] private GameObject bundlePrefab;
    [SerializeField] private Transform bundlesContainer;
    [SerializeField] private StoreBundleSO[] availablebundles;

    public override void Initialize()
    {
        base.Initialize();
        PopulateStore();
    }

    private void PopulateStore()
    {
        foreach (Transform child in bundlesContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (var bundle in availablebundles)
        {
            GameObject bundleObj = Instantiate(bundlePrefab, bundlesContainer);
            StoreItemUI itemUI = bundleObj.GetComponent<StoreItemUI>();
            if (itemUI != null)
            {
                itemUI.Setup(bundle);
            }
        }
    }
}
