using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerScript : NPCScript
{
    public static bool isAsleep = false;
    static bool animateUp = false;
    static bool animateDown = false;
    static bool animateJustFinished = false;

    void Start() {
        if (isAsleep) {
            SetSprite("Miner-Asleep");
        }
    }
    // Start is called before the first frame update
    void FixedUpdate()
    {
       if (animateUp) {
            this.GetComponent<BoxCollider2D>().enabled = false;
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.15f, this.transform.position.z);
            if (this.transform.position.y > 9) {
                animateUp = false;
                animateDown = true;
            } 
       } 
       if (animateDown) {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.5f, this.transform.position.z);
            if (this.transform.position.y < 3.86) {
                this.transform.position = new Vector3(this.transform.position.x, 3.86f, this.transform.position.z);
                animateDown = false;
                animateJustFinished = true;
            }
       }
       if (animateJustFinished == true) {
            PlayerScript.movement = true;
            animateJustFinished = false;
            this.GetComponent<BoxCollider2D>().enabled = true;
            TrapdoorScript.timesJumped++;
            PlayerScript ps = (PlayerScript) GameObject.Find("Player").GetComponent(typeof(PlayerScript));
            string[][] lines = new string[][] {
                new string[] {"$THAT WAS INSTANT COFFEE!"},
            };
            ps.CreateCustomDialogue(lines, this.GetName());
       }
    }

    public override string GetName() {
        return "Melvin the Miner";
    }

    public override string[][] GetLines() {
        if (isAsleep) {
            return new string[][] {
                new string[] {"zzz...zzz....zzz....."}
            };
        } else {
            return new string[][] {
                new string[] {"I...need...COFFEE!"},
                new string[] {"And don't you DARE give me instant coffee!"}
            };
        }
    }

    public override void ChoiceMaker(int choice) {}

    public void PlayAnimation() {
        animateUp = true;
    }

    public void SetSprite(string sprite) {
        this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(sprite);
    }
}
