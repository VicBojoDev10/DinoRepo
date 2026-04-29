using System;
using System.Collections.Generic;
using Dino.UtilityTools.Extensions.Json;
using NaughtyAttributes;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<RunTimeItem> items = new List<RunTimeItem>();
    public List<ItemData> skinsDataList = new List<ItemData>();
    public static InventoryManager Instance { get; private set; }
    private void Awake()
    {
        if(Instance != null &&  Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void Start()
    {
        LoadSkins();
    }

    [Button]
    private void SaveInventoryInJSon()
    {
        //CREATE FILE
        string json = JsonHelper.ToJson(items.ToArray(), prettyPrint: true);
        Debug.Log("Json" + json);

        string path = Application.persistentDataPath + "Inventory.json";
        System.IO.File.WriteAllText(path, json);
        
        Debug.Log("Inventory saved to:" +path);
    }
    [Button]
    public void CreateItemTest()
    {
        CreateSkins(SkinsType.blue);
    }
    public void CreateSkins(SkinsType skinsType)
    {
        foreach (var skin in skinsDataList)
        {
            if (skin.SkinsType == skinsType)
            {
                RunTimeItem runTimeItem = new RunTimeItem(skin.icon , skin.ItemName, skin.SkinsType);
                items.Add(runTimeItem); 
            }
        }
    }

    private void LoadSkins()
    {
        string path = Application.persistentDataPath + "Inventory.json";
        if (!System.IO.File.Exists(path))
        {
            Debug.LogError("No inventory File found at: " + path);
            return;
        }

        string json = System.IO.File.ReadAllText(path);
        RunTimeItem[] loadedItems = JsonHelper.FromJson<RunTimeItem>(json);
        items.Clear();
        items.AddRange(loadedItems);
    }
}

[Serializable]
public class RunTimeItem
{
    public Sprite icon;
    public string itemData;
    public SkinsType skinsType;

    public RunTimeItem(Sprite icon, string itemName, SkinsType skinsType)
    {
        this.icon = icon;
        this.itemData = itemName;
        this.skinsType = skinsType;
    }
}

public enum SkinsType
{
    red,
    blue,
    purple,
    yellow,
    shiny,
    rainbow,
}
