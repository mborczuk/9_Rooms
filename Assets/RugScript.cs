using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RugScript : NPCScript
{
    static bool isDestroyed = false;

    void Start()
    {
        if (isDestroyed) {
            Destroy(GameObject.Find("OverworldRug"));
            GameObject.Find("Trapdoor").GetComponent<BoxCollider2D>().enabled = true;
            TrapdoorScript ts = (TrapdoorScript) GameObject.Find("Trapdoor").GetComponent(typeof(TrapdoorScript));
            if (TrapdoorScript.timesJumped >= 3) {
                GameObject.Find("Trapdoor Hole").GetComponent<BoxCollider2D>().enabled = true;
            }
        }
        canChoose = true;
        hasName = false;
    }

    public override string GetName() {
        return "Rug";
    }

    public override string[][] GetLines() {
        return new string[][] {
            new string[] {"It's a blue rug with a simple pattern. The fabric seems durable and could be useful."},
            new string[] {"Should I take it with me?"},
            new string[] {"/choice/"},
            new string[] {"I rolled the rug up and put it with the rest of my stuff.", "I left the rug alone."},
        };
    }

    public override void ChoiceMaker(int choice) {
        if (choice == 0) {
            InventoryScript ins = (InventoryScript) GameObject.Find("InventoryBar").GetComponent(typeof(InventoryScript));
            ins.AddInventory("Rug");
            isDestroyed = true;
            GameObject.Find("Trapdoor").GetComponent<BoxCollider2D>().enabled = true;
            if (TrapdoorScript.timesJumped >= 3) {
                GameObject.Find("Trapdoor Hole").GetComponent<BoxCollider2D>().enabled = true;
            }
            Destroy(GameObject.Find("OverworldRug"));
        }
    }
}
