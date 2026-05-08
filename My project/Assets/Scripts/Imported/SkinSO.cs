using UnityEngine;

[CreateAssetMenu(fileName = "NewShopItem", menuName = "EndlesRunner/ShopItem")]
public class SkinSO : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public int cost;
    [TextArea] public string description;
    
    public SkinsType itemType;
}
