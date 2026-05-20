using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GooScript : NPCScript
{
    public static ArrayList gooCleaned = new ArrayList();
    // Start is called before the first frame update
    void Start()
    {
        hasName = false;
        foreach (string gooName in gooCleaned) {
            if (SceneManager.GetActiveScene().name == gooName) {
                Destroy(GameObject.Find("Goo"));
            }
        }
    }

    public override string GetName() {
        return "Goo";
    }

    public override string[][] GetLines() {
        return new string[][] {
            new string[] {"It's a puddle of goo."}
        };
    }

    public override void ChoiceMaker(int choice) {}
}
