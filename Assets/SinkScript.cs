using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkScript : NPCScript
{
    // Start is called before the first frame update
    void Start()
    {
        hasName = false;
    }

    public override string GetName() {
        return "Sink";
    }

    public override string[][] GetLines() {
        return new string[][] {
            new string[] {"It's a sink. It seems to work perfectly fine."}
        };
    }

    public override void ChoiceMaker(int choice) {}
}
