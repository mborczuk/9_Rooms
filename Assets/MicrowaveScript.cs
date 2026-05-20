using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrowaveScript : NPCScript
{
    // Start is called before the first frame update
    void Start()
    {
        hasName = false;
    }

    public override string GetName() {
        return "Microwave";
    }

    public override string[][] GetLines() {
        return new string[][] {
            new string[] {"It's a regular old microwave."},
            new string[] {"Seems to work just fine."}
        };
    }

    public override void ChoiceMaker(int choice) {}
}
