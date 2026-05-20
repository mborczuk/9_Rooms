using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockScript : NPCScript
{
    // Start is called before the first frame update
    void Start()
    {
        hasName = false;
    }

    public override string GetName() {
        return "Clock";
    }

    public override string[][] GetLines() {
        return new string[][] {
            new string[] {"It's a clock. The hands are stopped at 07:15."}
        };
    }

    public override void ChoiceMaker(int choice) {}
}
