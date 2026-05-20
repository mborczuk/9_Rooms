using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlabasterOrbScript : NPCScript
{
    static bool destroyed = false;
    // Start is called before the first frame update
    void Start()
    {
        hasName = false;
        if (destroyed) {
            Destroy(GameObject.Find("Alabaster Orb"));
        }
    }

    public override string GetName() {
        return "Alabaster Orb";
    }

    public override string[][] GetLines() {
        destroyed = true;
        Destroy(GameObject.Find("Alabaster Orb"));
        InventoryScript ins = (InventoryScript) GameObject.Find("InventoryBar").GetComponent(typeof(InventoryScript));
        ins.AddInventory("Alabaster Orb");
        return new string[][] {
            new string[] {"It's the Alabaster Orb."},
            new string[] {"Its pedestal is broken, but it could come in handy later."}
        };
    }

    public override void ChoiceMaker(int choice) {}
}
