using UnityEngine;
[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemData")]
public class ItemData : ScriptableObject
{
    public Sprite icon;
    public string itemName;

    [SerializeField] private SkinsType _skinsType;

    public SkinsType SkinsType => _skinsType;

    public Sprite Icon => icon;
    public string ItemName => itemName;
}
