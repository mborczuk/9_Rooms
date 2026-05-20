using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonScript : NPCScript
{
    static bool takenFlesh = false;
    // Start is called before the first frame update
    void Start()
    {
        hasName = false;
    }

    public override string GetName() {
        return "Skeleton";
    }

    public override string[][] GetLines() {
        string[][] lines;
        if (takenFlesh) {
            lines = new string[][] {
                new string[] {"$I won't take any more."},
            };
        } else {
            lines = new string[][] {
                new string[] {"It's a skeleton. Its flesh is moldy and peeling off of its bones...ugh."},
                new string[] {"$The skin is loose...I'll rip some off."}
            };
            InventoryScript ins = (InventoryScript) GameObject.Find("InventoryBar").GetComponent(typeof(InventoryScript));
            ins.AddInventory("Moldy Flesh");
            takenFlesh = true;
        }
        return lines;
    }

    public override void ChoiceMaker(int choice) {}
}
