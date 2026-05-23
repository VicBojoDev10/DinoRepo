using UnityEngine;
[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemData")]
public class ItemData : ScriptableObject
{
    [SerializeField] private Sprite icon;
    [SerializeField] private string itemName;
    [SerializeField] private int itemCost;

    [SerializeField] private SkinsType _skinsType;

    public SkinsType SkinsType => _skinsType;

    public Sprite Icon => icon;

    public int ItemCost => itemCost;
    public string ItemName => itemName;
}
