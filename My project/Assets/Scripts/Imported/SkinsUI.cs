using UnityEngine;
using Vic.Code;

public class SkinsUI : UIWindow
{
    [SerializeField] private GameObject _itemPrefab;
    [SerializeField] private Transform _itemContainer;


    /*public override void Initialize()
    {
       // SpawnItems();
    }*/

    private void SpawnItems()
    {
       // foreach(ItemData ItemData in )
    }
    public override void Show()
    {
        base.Show();
    }

    public override void Hide()
    {
        base.Hide();
    }
}
