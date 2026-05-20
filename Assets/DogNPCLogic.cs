using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogNPCLogic : NPCScript
{
    static bool choiceMade = false;
    public static bool dogShown = false;
    // Start is called before the first frame update
    void Start()
    {
        canChoose = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override string GetName() {
        return "Jeffrey";
    }
    public override string[][] GetLines() {
        string[][] lines;
        if (CrazyManScript.biscuitGiven) {
            lines = new string[][] {
                new string[] {"You even gave the dog biscuit away. You're a horrible person."},
                new string[] {"I should never have trusted you with Rufus."},
                new string[] {"*sniffle*"}
            };
        }
        else if (dogShown) {
            lines = new string[][] {
                new string[] {"*sniffle* I don't want to talk to you."},
                new string[] {"$Dog killer."}
            };
        }
        else if (!choiceMade) {
            lines = new string[][] {
                new string[] {"Man, the people here are the worst. They don't give me enough food for both me and my dog. Can you believe that?"},
                new string[] {"I've been splitting my meals with him, but I don't think I can handle doing that for much longer."},
                new string[] {"Hey, wait a sec - could you take him for a bit? Then I can eat full meals again. I'll take him back in a few days."},
                new string[] {"/choice/"},
                new string[] {"Thanks, man. Take good care of him.", "Guess I'll just starve then."}
            };
        } else {
            lines = new string[][] {
                new string[] {"Take good care of Rufus."}
            };
        }
        return lines;

    }

    public override void ChoiceMaker(int choice) {
        if (choice == 0 && !choiceMade) {
            InventoryScript ins = (InventoryScript) GameObject.Find("InventoryBar").GetComponent(typeof(InventoryScript));
            ins.AddInventory("Dog");
            choiceMade = true;
        }
    }


}
