using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrificeScript : NPCScript
{
    public static bool dogOn = false;
    public static bool dogKilled = false;
    public static bool stoneUsed = false;
    // Start is called before the first frame update
    void Start()
    {
        if (dogOn) {
            this.SetSprite("Sacrifice Pedestal Dog");
        }
        if (dogKilled) {
            this.SetSprite("Sacrifice Pedestal Bloody");
        }
    }

    public override string GetName() {
        return "???";
    }

    public override string[][] GetLines() {
        string[][] lines;
        if (dogKilled) {
            hasName = false;
            lines = new string[][] {
                new string[] {"..."},
                new string[] {"..."},
                new string[] {"I don't want to be here anymore..."},
                new string[] {"$It was necessary."},
            };
        } else if (stoneUsed) {
            canChoose = true;
            hasName = false;
            lines = new string[][] {
                new string[] {"I really don't want to do this..."},
                new string[] {"/choice/"},
                new string[] {"/end/", "G-good."}
            };
        }
        else if (dogOn) {
            hasName = false;
            canChoose = true;
            lines = new string[][] {
                new string[] {"The dog is so cute..."},
                new string[] {"Take him off the pedestal?"},
                new string[] {"/choice/"},
                new string[] {"He'll be safe and sound with me.", "I left the dog alone..."}
            };
        } else {
            hasName = true;
            lines = new string[][] {
                new string[] {"$I DEMAND A SACRIFICE."}
            };
        }
        return lines;
    }

    public override void ChoiceMaker(int choice) {
        if (choice == 0 && stoneUsed) {
            InventoryScript ins = (InventoryScript) GameObject.Find("InventoryBar").GetComponent(typeof(InventoryScript));
            BlackoutScript bs = (BlackoutScript) GameObject.Find("Blackout").GetComponent(typeof(BlackoutScript));
            PlayerScript.movement = false;
            AudioScript.StopAudio();
            bs.FadeToBlack(1f, false, 0.002f);
            ins.AddInventory("Bloody Stone");
            ins.AddInventory("Gold Collar");
            ins.AddInventory("Bones");
            this.SetSprite("Sacrifice Pedestal Bloody");
            PlayerScript.bones += 2;
            dogKilled = true;
            stoneUsed = false;
            StartCoroutine(WaitAfterBlackout());
        } else if (choice == 1 && stoneUsed) {
            InventoryScript ins = (InventoryScript) GameObject.Find("InventoryBar").GetComponent(typeof(InventoryScript));
            ins.AddInventory("Sharp Stone");
            stoneUsed = false;
        }
        else if (choice == 0) {
            Debug.Log("hi");
            InventoryScript ins = (InventoryScript) GameObject.Find("InventoryBar").GetComponent(typeof(InventoryScript));
            ins.AddInventory("Dog");
            this.SetSprite("Sacrifice Pedestal");
            dogOn = false;
        }
        canChoose = false;
    }

    public void SetSprite(string sprite) {
        this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(sprite);
    }

    IEnumerator WaitAfterBlackout() {
        GameObject.Find("Stabbing Sound").GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(5);
        PlayerScript ps = (PlayerScript) GameObject.Find("Player").GetComponent(typeof(PlayerScript));
        string[][] lines = new string[][] {
            new string[] {"$YOUR SACRIFICE IS APPRECIATED."},
            new string[] {"$YOU MAY KEEP THE COLLAR AND THE BONES."},
        };
        ps.CreateCustomDialogue(lines, "???");
    }
}
