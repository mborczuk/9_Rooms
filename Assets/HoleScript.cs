using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HoleScript : NPCScript
{
    public static bool ratFed = false;
    static bool itemsPickedUp = false;
    static bool cashPickedUp = false;
    // Start is called before the first frame update
    void Start()
    {
        hasName = false;
        if (cashPickedUp) {
            Destroy(GameObject.Find("Wad of Cash"));
        }
    }


    public override string GetName() {
        return "Hole";
    }

    public override string[][] GetLines() {
        string[][] lines;
        if (SceneManager.GetActiveScene().name == "Room 4") {
            if (!itemsPickedUp) {
                lines = new string[][] {
                    new string[] {"There's a vial of something and a can of silver paint in here."},
                    new string[] {"I picked them up and put them with my other things."},
                };
                itemsPickedUp = true;
            } else {
                lines = new string[][] {
                    new string[] {"There's nothing else there."}
                };
            }
        } else if (SceneManager.GetActiveScene().name == "Room 8") {
            if (!PlayerScript.wallOpen) {
                lines = new string[][] {
                    new string[] {"The wall is slightly cracked here."},
                };
            } else if (PlayerScript.wallOpen && !cashPickedUp) {
                lines = new string[][] {
                    new string[] {"I picked up the cash in the wall."}
                };
                InventoryScript ins = (InventoryScript) GameObject.Find("InventoryBar").GetComponent(typeof(InventoryScript));
                ins.AddInventory("Wad of Cash");
                Destroy(GameObject.Find("Wad of Cash"));
                cashPickedUp = true;
            } else {
                lines = new string[][] {
                    new string[] {"All in all, it's just another hole in the wall."}
                };
            }
        }
        else {
            if (!ratFed) {
                lines = new string[][] {
                    new string[] {"It's a small hole. I can hear the faint squeaking of a rat inside it."},
                    new string[] {"There's a glint of bronze in the hole."},
                };
            } else {
                lines = new string[][] {
                    new string[] {"I'm going to leave the rat alone now."}
                };
            }
        }

        return lines;

    }

    public override void ChoiceMaker(int choice) {}
}
