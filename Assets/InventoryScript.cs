using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryScript : MonoBehaviour
{
    private static ArrayList inventory = new ArrayList();
    private static int startingIndex = 0;
    private static int currentIndex = 0;
    private static bool reduceAlpha = false;
    private static TextMeshProUGUI tp;
    // Start is called before the first frame update
    void Start()
    {
        tp = GameObject.Find("ItemText").GetComponent<TextMeshProUGUI>();
        RenderInventory();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerScript.endDialogue) {
            if (Input.GetKeyDown("a")) {
                currentIndex--;
                if (currentIndex < 0) {
                    currentIndex = 0;
                    startingIndex--;
                    if (startingIndex < 0) {
                        startingIndex = 0;
                    }
                }
                string name = Selected();
                if (name == "Bones") {
                    name += " (" + PlayerScript.bones + ")";
                }
                tp.text = name;
                tp.color = new Color(tp.color.r, tp.color.g, tp.color.b, 1f);
                reduceAlpha = true;
                RenderInventory();
            } 
            if (Input.GetKeyDown("d")) {
                currentIndex++;
                if (currentIndex > inventory.Count - 1) {
                    currentIndex = inventory.Count - 1;
                }
                if (currentIndex > 7) {
                    currentIndex = 7;
                    startingIndex++;
                    if (startingIndex > inventory.Count - 8) {
                        startingIndex = inventory.Count - 8;
                    }
                }
                
                string name = Selected();
                if (name == "Bones") {
                    name += " (" + PlayerScript.bones + ")";
                }
                tp.text = name;
                tp.color = new Color(tp.color.r, tp.color.g, tp.color.b, 1f);
                reduceAlpha = true;
                RenderInventory();
            }
            if (reduceAlpha) {
                tp.color = new Color(tp.color.r, tp.color.g, tp.color.b, tp.color.a - 0.01f);
                if (tp.color.a == 0f) {
                    reduceAlpha = false;
                }
            }
        } else {
            tp.color = new Color(tp.color.r, tp.color.g, tp.color.b, 0f);
            reduceAlpha = false;
        }
    }

    public void AddInventory(string itemName) {
        inventory.Add(itemName);
        RenderInventory();
    }

    public void RemoveInventory(string itemName) {
        inventory.Remove(itemName);
        ResetSelection();
        RenderInventory();
    }

    void RenderInventory() {
        RenderSelection();
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        foreach (GameObject i in items) {
            Destroy(i);
        }
        SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
        float left = this.transform.position.x - sr.bounds.size.x / 2;
        float tileWidth = sr.bounds.size.x / 8 * 0.97f;
        for (int i = startingIndex; i < inventory.Count; i++) {
            if (i == startingIndex + 8) break;
            GameObject go1 = new GameObject();
            go1.name = "Item " + (i - startingIndex);
            go1.tag = "Item";
            go1.AddComponent<SpriteRenderer>();
            go1.transform.position = new Vector3(left + (i - startingIndex + 1) * tileWidth - 0.85f, this.transform.position.y, 0);
            go1.transform.localScale = new Vector3(1.5f, 1.5f, 0);
            sr = go1.GetComponent<SpriteRenderer>();
            sr.sprite = Resources.Load<Sprite>("" + inventory[i]);
            sr.sortingOrder = 80;
            if (this.GetComponent<SpriteRenderer>().sortingOrder == 9998) {
                sr.sortingOrder = 10000;
            }
        }
    }

    void RenderSelection() {
        GameObject[] selectors = GameObject.FindGameObjectsWithTag("Select");
        foreach (GameObject s in selectors) {
            Destroy(s);
        }
        if (inventory.Count > 0) {
            SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
            float left = this.transform.position.x - sr.bounds.size.x / 2;
            float tileWidth = sr.bounds.size.x / 8 * 0.97f;
            GameObject go1 = new GameObject();
            go1.name = "Selector";
            go1.tag = "Select";
            go1.AddComponent<SpriteRenderer>();
            go1.transform.position = new Vector3(left + (currentIndex + 1) * tileWidth - 0.85f, this.transform.position.y, 0);
            go1.transform.localScale = new Vector3(4f, 4f, 0);
            sr = go1.GetComponent<SpriteRenderer>();
            sr.sprite = Resources.Load<Sprite>("Selection");
            sr.sortingOrder = 79;
            if (this.GetComponent<SpriteRenderer>().sortingOrder == 9998) {
                sr.sortingOrder = 9999;
            }
        }
    }

    public string Selected() {
        if (inventory.Count > 0) {
            return (string) inventory[startingIndex + currentIndex];
        }
        return "";
    }

    public int GetSize() {
        return inventory.Count;
    }

    public void ResetSelection() {
        startingIndex = 0;
        currentIndex = 0;
    }

    public bool Contains(string item) {
        return inventory.Contains(item);
    }

    public void Clear() {
        inventory.Clear();
    }

}
