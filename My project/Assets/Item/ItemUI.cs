using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUI : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;

    [Button]
    public void SetitemData(RunTimeItem runTimeItem)
    {
        itemIcon.sprite = runTimeItem.Icon;
        itemName.text = runTimeItem.ItemCost.ToString();
    }
}
