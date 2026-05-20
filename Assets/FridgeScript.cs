using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeScript : NPCScript
{
// Start is called before the first frame update
    void Start()
    {
        hasName = false;
    }

    public override string GetName() {
        return "Fridge";
    }

    public override string[][] GetLines() {
        return new string[][] {
            new string[] {"The fridge is empty. All of our food is delivered to us."},
            new string[] {"I guess whoever's keeping us here doesn't want us to cook for ourselves."}
        };
    }

    public override void ChoiceMaker(int choice) {}
}
