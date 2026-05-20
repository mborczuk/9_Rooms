using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JanitorScript : NPCScript
{
    static bool talkedOnce = false;
    public static bool gooShown = false;
    public static bool cashShown = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override string GetName() {
        return "Janitor";
    }

    public override string[][] GetLines() {
        if (cashShown) {
            canChoose = false;
            int one = Random.Range(1, 20);
            int two = Random.Range(one, 20);
            int three = Random.Range(two, 20);
            return new string[][] {
                new string[] {one + ", " + two + ", " + three + "...Oh, you distracted me. Now I have to start again."},
            };
        }
        else if (gooShown) {
            canChoose = false;
            return new string[][] {
                new string[] {"Maybe if you gave me a little something...you know...budget cuts are pretty severe these days..."},
            };
        } else if (talkedOnce) {
            canChoose = false;
            return new string[][] {
                new string[] {"Show me the bucket when you're done."},
            };
        } else {
            canChoose = true;
            return new string[][] {
                new string[] {"You're not supposed to be in here."},
                new string[] {"But, hm. I'm feeling kinda lazy right now. Since the prison got funding cuts, I've had to work as both a guard AND a janitor..."},
                new string[] {"Oh, did you not know that 9 Rooms was a prison? I guess they don't tell you guys that you're prisoners."},
                new string[] {"Anyway, if you do some cleaning for me, I'll forget I ever saw you here."},
                new string[] {"/choice/"},
                new string[] {"Cool. There are four goo puddles around the prison. Mop them up and fill the bucket. Bring it back here when you're done. I'll open the door.", "You do realize you can't leave this room, right?"}
            };
        }
    }

    public override void ChoiceMaker(int choice) {
        if (choice == 0) {
            InventoryScript ins = (InventoryScript) GameObject.Find("InventoryBar").GetComponent(typeof(InventoryScript));
            ins.AddInventory("Bucket");
            ins.AddInventory("Mop");
            talkedOnce = true;
            DoorScript.platinumDoorOpen = true;
        }
    }
}
