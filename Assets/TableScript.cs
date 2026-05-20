using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableScript : NPCScript
{
    static bool takenMug = false;
    // Start is called before the first frame update
    void Start()
    {
        hasName = false;
    }

    public override string GetName() {
        return "Table";
    }

    public override string[][] GetLines() {
        string[][] lines;
        if (!takenMug) {
            lines = new string[][] {
                new string[] {"There are a few mugs on this table. The leftmost one has some coffee powder in it."},
                new string[] {"I'm going to take it."}
            };
            takenMug = true;
        } else {
            lines = new string[][] {
                new string[] {"I don't need more mugs."}
            };
        }

        return lines;
    }

    public override void ChoiceMaker(int choice) {}
}
