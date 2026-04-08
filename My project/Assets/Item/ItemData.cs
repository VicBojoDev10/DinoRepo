using UnityEngine;
[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemData")]
public class ItemData : ScriptableObject
{
    public Sprite icon;
    public string itemName;
    
    public Sprite Icon => icon;
    public string ItemName => itemName;
}
