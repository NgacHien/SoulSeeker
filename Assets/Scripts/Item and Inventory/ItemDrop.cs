using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private int possibleItemDrop;
    [SerializeField] private ItemData[] possibleDrop;
    private List<ItemData> dropList = new List<ItemData>();


    [SerializeField] private GameObject dropPrefab;
    

    public virtual void GenerateDrop()
    {
       
        for (int i = 0; i < possibleDrop.Length; i++)
        {
            if (Random.Range(0, 100) <= possibleDrop[i].dropChance)
                dropList.Add(possibleDrop[i]);
        }

        if (dropList.Count == 0)
        {
            Debug.Log("No items dropped because dropList is empty.");
            return;
        }

        
        for (int i = 0; i < possibleItemDrop && dropList.Count > 0; i++)
        {
            int randomIndex = Random.Range(0, dropList.Count); 
            ItemData randomItem = dropList[randomIndex];

            dropList.RemoveAt(randomIndex);
            DropItem(randomItem);
        }
    }

   protected void DropItem(ItemData _itemData)
    {
        GameObject newDrop = Instantiate(dropPrefab, transform.position, Quaternion.identity);

        Vector2 randomVelocity = new Vector2(Random.Range(-5, 5), Random.Range(12, 15));

        newDrop.GetComponent<ItemObject>().SetupItem(_itemData, randomVelocity);
    }
}
