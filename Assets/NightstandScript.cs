using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightstandScript : NPCScript
{
    // Start is called before the first frame update
    void Start()
    {
        hasName = false;
    }

    public override string GetName() {
        return "Nightstand";
    }

    public override string[][] GetLines() {
        return new string[][] {
            new string[] {"It's a wooden nightstand."}
        };
    }

    public override void ChoiceMaker(int choice) {}
}
