using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapdoorScript : NPCScript
{
    public static int timesJumped = 0;

    // Start is called before the first frame update
    void Start()
    {
        hasName = false;
        if (timesJumped >= 3) {
            SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
            sr.sprite = Resources.Load<Sprite>(sr.sprite.name + "Open");
            this.transform.position = new Vector3(3.03f,this.transform.position.y,this.transform.position.z);
            int sortingOrder = this.GetComponent<SpriteRenderer>().sortingOrder;
            GameObject.Find("Trapdoor Hole").GetComponent<SpriteRenderer>().sortingOrder = -4;
            GameObject.Find("Mysterious Liquid").GetComponent<SpriteRenderer>().sortingOrder = -3;
            GameObject.Find("Silver Paint").GetComponent<SpriteRenderer>().sortingOrder = -3;
        }
    }

    public override string GetName() {
        return "Trapdoor";
    }

    public override string[][] GetLines() {
        string[][] lines;
        if (timesJumped == 0) {
            lines = new string[][] {
                new string[] {"There's a metal trapdoor in the floor."},
                new string[] {"It's securely locked."}
            }; 
        } else if (timesJumped == 1) {
            lines = new string[][] {
                new string[] {"There's a metal trapdoor in the floor."},
                new string[] {"The lock seems to have loosened a little bit."}
            };
        } else if (timesJumped == 2) {
            lines = new string[][] {
                new string[] {"There's a metal trapdoor in the floor."},
                new string[] {"The lock is extremely loose. It seems like it will break with just a little more force."}
            };
        } else {
            lines = new string[][] {
                new string[] {"The trapdoor is fully open now."},
                new string[] {"That miner is incredibly strong."}
            };
        }
        return lines;
    }

    public override void ChoiceMaker(int choice) {}
}
