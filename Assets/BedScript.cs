using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedScript : NPCScript
{
    static bool takenKey = false;

    void Start() {
        hasName = false;
    }
    public override string GetName() {
        return "Bed";
    }
    public override string[][] GetLines() {
        string[][] lines;
        if (this.name == "KeyBed") {
            if (takenKey) {
                lines = new string[][] {
                    new string[] {"Nothing but dust bunnies here."},
                };
            } else {
                lines = new string[][] {
                    new string[] {"There's a small key under the bed."},
                };
                InventoryScript ins = (InventoryScript) GameObject.Find("InventoryBar").GetComponent(typeof(InventoryScript));
                ins.AddInventory("Storage Key");
                takenKey = true;
            }
            
        } else {
            lines = new string[][] {
                new string[] {"It's a pretty comfy bed."},
            };
        }
        return lines;
    }
    public override void ChoiceMaker(int choice) {}
}
