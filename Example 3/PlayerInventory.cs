using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    public List<Item> ItemList = new List<Item>(); 

    public string interactKey;
    public string ItemName;
    public SpriteRenderer ItemSprite;

    public bool touchingPickup;

    public Item foundObject;

    public CatchFish CF;

    public GameObject AddedItemText;
    public TMP_Text AddedItem;
    public GameObject Canvas;

    public GameObject InventoryBG;
    public GameObject ItemContainerUI;

    public GameObject delItem;

    // Start is called before the first frame update
    void Start()
    {
        CF = GameObject.Find("Bait").GetComponent<CatchFish>();

        foundObject = new Item();
    }

    // Update is called once per frame
    void Update()
    {
        foundObject = ItemList.Find(obj => obj.itemName == ItemName);

        if(ItemList.Contains(foundObject) == true)
        {
            if(touchingPickup == true)
            {
                if(Input.GetKeyDown(interactKey))
                {
                    ItemList[ItemList.IndexOf(foundObject)].quantity += 1;
                    InstantiateChildObject();
                    StartCoroutine(DeleteItem());
                }
            }

            if(CF.FishCaught == true)
            {
                ItemList[ItemList.IndexOf(foundObject)].quantity += 1;
                InstantiateChildObject();
            }
        }
        
        if(ItemList.Contains(foundObject) == false)
        {
            if(touchingPickup == true)
            {
                if(Input.GetKeyDown(interactKey))
                {   
                    foundObject = new Item();
                    foundObject.itemName = ItemName;
                    foundObject.quantity = 1;
                    foundObject.itemSprite = ItemSprite;
                    foundObject.dropable = true;
                    foundObject.equipable = false;
                    if(foundObject.itemName.Contains("Rod"))
                    {
                        foundObject.equipable = true;
                    }
                    foundObject.thisItem = GameObject.Find(ItemName+"(Clone)");

                    ItemList.Add(foundObject);
                    InstantiateChildObject();
                    StartCoroutine(DeleteItem());

                    GameObject newItem = Instantiate(ItemContainerUI, InventoryBG.transform);
                    newItem.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                }
            }

            if(CF.FishCaught == true)
            {
                foundObject = new Item();
                foundObject.itemName = ItemName;
                foundObject.quantity = 1;
                foundObject.itemSprite = ItemSprite;
                foundObject.dropable = true;
                foundObject.equipable = false;
                if(foundObject.itemName.Contains("Rod"))
                {
                    foundObject.equipable = true;
                }
                foundObject.thisItem = GameObject.Find(ItemName+"(Clone)");

                ItemList.Add(foundObject);
                InstantiateChildObject();

                GameObject newItem = Instantiate(ItemContainerUI, InventoryBG.transform);
                newItem.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Item")
        {
            delItem = collision.gameObject;
            touchingPickup = true;
            ItemName = collision.gameObject.name;
            ItemSprite = collision.gameObject.GetComponent<SpriteRenderer>();
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        touchingPickup = false;
        foundObject = new Item();
    }

    void InstantiateChildObject()
    {
        GameObject newAddedItem = Instantiate(AddedItemText, Canvas.transform);
    }

    IEnumerator DeleteItem()
    {
        yield return new WaitForSeconds(0.05f);
        Destroy(delItem);
    }
}
