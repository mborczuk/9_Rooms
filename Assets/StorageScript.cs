using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageScript : NPCScript
{
    public static bool unlocked = false;
    // Start is called before the first frame update
    void Start()
    {
        hasName = false;
    }


    public override string GetName() {
        return "Storage Locker";
    }
    public override string[][] GetLines() {
        string[][] lines;
        if (!unlocked) {
            lines = new string[][] {
                new string[] {"It's locked. I need a key to open it."},
            };
        } else {
            lines = new string[][] {
                new string[] {"Nothing left in here."}
            };
        }
        return lines;

    }

    public override void ChoiceMaker(int choice) {}
}
