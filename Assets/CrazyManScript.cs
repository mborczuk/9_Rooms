using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyManScript : NPCScript
{
    public static bool biscuitGiven = false;

    public override string GetName() {
        return "Old Man";
    }

    public override string[][] GetLines() {
        if (biscuitGiven) {
            return new string[][] {
                new string[] {"Thanks for the biscuit. Even if it was a dog biscuit."},
                new string[] {"See, we were locked up in here for so long they forgot about us. I guess they forgot the password to the door or something."},
                new string[] {"My roommate was a giant...he didn't last long, as you can see."}
            };
        } else {
            return new string[][] {
                new string[] {"P-please...f-food...a-anything..."}
            };
        }

    }

    public override void ChoiceMaker(int choice) {}
}
