using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenScript : NPCScript
{
// Start is called before the first frame update
    void Start()
    {
        hasName = false;
    }

    public override string GetName() {
        return "Oven";
    }

    public override string[][] GetLines() {
        return new string[][] {
            new string[] {"The stovetop is extremely clean since nobody uses it."},
        };
    }

    public override void ChoiceMaker(int choice) {}
}
