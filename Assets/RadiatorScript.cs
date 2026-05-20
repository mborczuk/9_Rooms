using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiatorScript : NPCScript
{
    // Start is called before the first frame update
    void Start()
    {
        hasName = false;
    }

    public override string GetName() {
        return "Radiator";
    }

    public override string[][] GetLines() {
        return new string[][] {
            new string[] {"It's a radiator. It's not even plugged in."}
        };
    }

    public override void ChoiceMaker(int choice) {}
}
