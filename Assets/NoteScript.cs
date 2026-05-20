using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NoteScript : NPCScript
{
    // Start is called before the first frame update
    void Start()
    {
        hasName = false;
    }

    public override string GetName() {
        return "Note";
    }

    public override string[][] GetLines() {
        return new string[][] {
            new string[] {"There's a note on the floor. It says: "},
            new string[] {"*NOTICE*\nPassword Reminders"},
            new string[] {"If you can't remember this week's password, remember this: "},
            new string[] {"\"Take the middle two from green, the last from red, and the first three from blue, and don't forget to remove blue's middle.\""},
            new string[] {"...Wait, was that even right?"}
        };
    }

    public override void ChoiceMaker(int choice) {}
}
