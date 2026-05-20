using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeskScript : NPCScript
{
    // Start is called before the first frame update
    void Start()
    {
        hasName = false;
    }

    public override string GetName() {
        return "Desk";
    }

    public override string[][] GetLines() {
        string[][] lines;
        string roomName = SceneManager.GetActiveScene().name;
        if (roomName == "Room 1") {
            lines = new string[][] {
                new string[] {"It's a desk. It has a map of the United States of America on it."},
                new string[] {"New York is colored in green, Pennsylvania is blue, and California's red."}
            };
        } else if (roomName == "Room 7") {
            if (this.name == "AxeDeskTop") {
                if (!PlayerScript.axeGotten) {
                    lines = new string[][] {
                        new string[] {"It's a table with some tools on it. All of the tools are adhered to the table somehow."},
                        new string[] {"$I want that axe."}
                    };
                } else {
                    lines = new string[][] {
                        new string[] {"It's a table with some tools on it."},
                    };
                }
            } else {
                if (!PlayerScript.batGotten) {
                    lines = new string[][] {
                        new string[] {"It's a table with some tools on it. All of the tools are adhered to the table somehow."},
                        new string[] {"$The battle axe isn't sharp enough. I want the baseball bat."}
                    };
                    
                } else {
                    lines = new string[][] {
                        new string[] {"Why is there a ukelele on this table?"},
                    };
                }
            }
        } else {
            lines = new string[][] {
                new string[] {"This message should not appear."},
                new string[] {"Contact the developer if you see it."}
            };
        }

        return lines;

    }

    public override void ChoiceMaker(int choice) {}
}
