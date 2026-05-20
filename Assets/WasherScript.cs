using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasherScript : NPCScript
{
// Start is called before the first frame update
    void Start()
    {
        hasName = false;
    }

    public override string GetName() {
        return "Washer";
    }

    public override string[][] GetLines() {
        return new string[][] {
            new string[] {"I'm not really sure why there's a washer in the kitchen."},
            new string[] {"Also, there's no dryer to go along with it for some reason."}
        };
    }

    public override void ChoiceMaker(int choice) {}
}
