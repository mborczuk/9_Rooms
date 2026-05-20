using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcScript : NPCScript
{
    static int whichGossip = 0;
    // Start is called before the first frame update
    void Start()
    {
        canChoose = true;
    }

    public override string GetName() {
        return "Dennis";
    }

    public override string[][] GetLines() {
        // maybe expand this
        if (whichGossip == 0) {
            return new string[][] {
                new string[] {"Wanna hear some gossip?"},
                new string[] {"/choice/"},
                new string[] {"See the big guy in the corner? He used to be a miner for a successful corporation.", "/end/"},
                new string[] {"Then he got fired for stealing some of the precious stones he had mined.", ""},
                new string[] {"I heard that after that happened, he got so angry that he did some...unpleasant things to his former bosses.", ""},
                new string[] {"I believe it. When he gets angry, he shakes the whole facility.", ""},
                new string[] {"Don't tell him I told you that. If you do, he'll definitely kill me.", ""}
            };
        } else {
            return new string[][] {
                new string[] {"Wanna hear some gossip?"},
                new string[] {"/choice/"},
                new string[] {"See the guy in the robe in the corner?", "/end/"},
                new string[] {"All I know about him is that he's obsessed with bones. Mostly human ones.", ""},
                new string[] {"He'll play his game with anyone who shows him a bone. Any type.", ""},
                new string[] {"Maybe there are some animal bones around you can use.", ""}
            };
        }
    }

    public override void ChoiceMaker(int choice) {
        if (choice == 0) {
            whichGossip++;
            if (whichGossip > 1) {
                whichGossip = 0;
            }
        }
    }
}
