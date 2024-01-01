using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataList", menuName = "Inventory/ItemDataList")]

public class ItemDataList : ScriptableObject
{
    public List<ItemDetails> itemDetailList;

    public ItemDetails GetItemDetails(ItemName itemName)
    {
        return itemDetailList.Find(i => i.itemName == itemName);
    }

}
