using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChairScript : NPCScript
{
    // Start is called before the first frame update
    void Start()
    {
        hasName = false;
    }

    public override string GetName() {
        return "Chair";
    }

    public override string[][] GetLines() {
        string[][] lines;
        if (SceneManager.GetActiveScene().name == "Room 1") {
            lines = new string[][] {
                new string[] {"It's a wooden chair that goes with the desk."}
            };
        } else {
            lines = new string[][] {
                new string[] {"It's a wooden chair that goes with the table."}
            };
        }
        return lines;
    }

    public override void ChoiceMaker(int choice) {}
}
