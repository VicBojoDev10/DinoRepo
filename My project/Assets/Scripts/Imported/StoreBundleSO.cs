using UnityEngine;

[CreateAssetMenu(fileName = "NewStoreBundle", menuName = "EndlessRunner/Store Bundle")]
public class StoreBundleSO : ScriptableObject
{
    public int scalesAmount;
    public float priceUSD;
    public Sprite bundleIcon;
}
