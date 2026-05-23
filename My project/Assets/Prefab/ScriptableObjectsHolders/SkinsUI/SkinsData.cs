using UnityEngine;


public class SkinsData : ScriptableObject
{
    [SerializeField] private Sprite skinIcon;
    [SerializeField] private string skinID;
    
    public Sprite SkinIcon => skinIcon;
    public string SkinID => skinID;
}
