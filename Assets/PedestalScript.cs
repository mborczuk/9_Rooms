using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PedestalScript : NPCScript
{
    static ArrayList openPedestals = new ArrayList();
    string pedestalName; 
    static bool gotKey = false;
    void Start() {
        pedestalName = this.GetComponent<SpriteRenderer>().sprite.name;
        hasName = false;
        if (openPedestals.Contains(pedestalName)) {
            OpenPedestal();
        }
    }

    void Update() {

    }

    public override string GetName() {
        return this.GetComponent<SpriteRenderer>().sprite.name;
    }

    public override string[][] GetLines() {
        string[][] lines = new string[][] {
                new string[] {"This is a failsafe message. This message should not appear."}
        };
        if (this.GetName() == "Bronze Pedestal") {
            lines = new string[][] {
                new string[] {"It's the pedestal for the Bronze Orb."},
                new string[] {"Placing the Bronze Orb on it should open the Bronze Door."}
            };
        }
        if (this.GetName() == "Gold Pedestal") {
            lines = new string[][] {
                new string[] {"It's the pedestal for the Gold Orb."},
                new string[] {"Placing the Gold Orb on it should open the Gold Door."}
            };
        }
        if (this.GetName() == "Silver Pedestal") {
            lines = new string[][] {
                new string[] {"It's the pedestal for the Silver Orb."},
                new string[] {"Placing the Silver Orb on it should open the Silver Door."}
            };
        }
        if (this.GetName() == "Alabaster Pedestal") {
            if (gotKey) {
                lines = new string[][] {
                    new string[] {"This pedestal is broken. Putting the Alabaster Orb on it won't do anything."},
                };
            } else {
                lines = new string[][] {
                    new string[] {"Someone jammed something into this pedestal. Now it doesn't work properly."},
                    new string[] {"I'll pull whatever it was out of the pedestal."},
                    new string[] {"It was a card key."},
                };
                gotKey = true;
                InventoryScript ins = (InventoryScript) GameObject.Find("InventoryBar").GetComponent(typeof(InventoryScript));
                ins.AddInventory("Card Key");
            }

        }
        if (this.GetName().Contains("Filled")) {
            lines = new string[][] {
                new string[] {"I've already placed the orb onto this pedestal."},
            };
        }

        return lines;
    }
    public override void ChoiceMaker(int choice) {}

    public void OpenPedestal() {
        if (!openPedestals.Contains(pedestalName)) {
            openPedestals.Add(pedestalName);
        }
        string metal = "";
        if (pedestalName == "Bronze Pedestal") {
            metal = "Bronze";
        }
        if (pedestalName == "Gold Pedestal") {
            metal = "Gold";
        }
        if (pedestalName == "Silver Pedestal") {
            metal = "Silver";
        }
        SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
        sr.sprite = Resources.Load<Sprite>(sr.sprite.name + " Filled");
        if (!DoorScript.openDoors.Contains("Room 2" + metal + "Door")) {
            DoorScript.openDoors.Add("Room 2" + metal + "Door");
        }
    }
}