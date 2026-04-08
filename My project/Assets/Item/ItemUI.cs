using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUI : MonoBehaviour
{
    public ItemData itemData;
    [SerializeField] private Image itemIcon;
    private TextMeshProUGUI itemName;

    [Button]
    public void SetitemData()
    {
        itemIcon.sprite = itemData.icon;
        itemName.text = itemData.name;
    }
}
