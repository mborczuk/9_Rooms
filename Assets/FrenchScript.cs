using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrenchScript : NPCScript
{
    public static bool bookGiven = false;
    public static bool talkedAfterGiven = false;
    public static bool talkedAfterRefusal = false;
    public static bool downed = false;

    void Start() {
        if (downed) {
            SetSprite("Frenchman-Hurt");
        }
    }
    public override string GetName() {
        return "Jean-Luc";
    }

    public override string[][] GetLines() {
        string[][] lines;
        if (downed) {
            lines = new string[][] {
                new string[] {"Owwwwww..."},
            };
        } else if (bookGiven && !talkedAfterGiven) {
            talkedAfterGiven = true;
            lines = new string[][] {
                new string[] {"You want me to translate zis? Fine, I vill."},
                new string[] {"Come back later."},
            };
        } else if (talkedAfterGiven && PlayerScript.doorsUnlocked >= 3) {
            talkedAfterRefusal = true;
            lines = new string[][] {
                new string[] {"Ah yes, I have read ze entire thing. Very interesting book."},
                new string[] {"I do not think I will give zis back to you. There ees plenty of useful information in here that I do not want you to have."},
                new string[] {"Get lost, mon ami."},
            };
        } else if (talkedAfterGiven) {
            lines = new string[][] {
                new string[] {"I am busy reading the book you gave me."},
                new string[] {"Leave me alone."},
            };
        }
        else {
            lines = new string[][] {
                new string[] {"I am very busy. Leave me alone."},
            };
        }
        return lines;
    }

    public override void ChoiceMaker(int choice) {}

    public void SetSprite(string sprite) {
        this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(sprite);
    }
}
