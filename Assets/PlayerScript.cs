using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    float dx = 0;
    float dy = 0;
    float speed = 0.1f;
    public static float time = 0;
    string direction = "none";
    public static bool movement = true;
    public static GameObject NPC = null;
    public static bool endDialogue = true;
    public static int endingCutscene = 0;
    bool chooseSecondItem = false;
    string firstItem = "";
    public static bool goodEnding = false;
    public static bool firstCutscene = false;
    public static bool axeGotten = false;
    public static bool batGotten = false;
    public static bool mugGotten = false;
    public static bool chair1Destroyed = false;
    public static bool chair2Destroyed = false;
    public static bool nightstandDestroyed = false;
    public static bool itemsGotten = false;
    public static bool goldOrb = false;
    public static bool pickaxeGotten = false;
    public static bool wallOpen = false;
    public static int doorsUnlocked = 0;
    public static bool hairGotten = false;
    public static bool inCombination = false;
    public static bool startedBlackout = true;
    public static bool drankSleepingPotion = false;
    public static int bones = 0;
    public static string yourName = "Piccolo";
    public static bool inFirstCutscene = false;
    public static bool formless3Gotten = false;
    SpriteRenderer spriteRenderer;
    Sprite[] playerSprites;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        playerSprites = Resources.LoadAll<Sprite>("PlayerSpritesheet");
        if (!firstCutscene) {
            // play first cutscene
            firstCutscene = false;
            string[][] lines = new string[][] {
                new string[] {"Hey. I can see you."},
                new string[] {"Yeah, you. Outside the screen."},
                new string[] {"What's your name?"},
                new string[] {"/passcode/"},
                new string[] {"..."}
            };
            CreateCustomDialogue(lines, "");
        }
        if (axeGotten) {
            Destroy(GameObject.Find("Axe"));
        }
        if (batGotten) {
            Destroy(GameObject.Find("Big Baseball Bat"));
        }
        if (mugGotten) {
            Destroy(GameObject.Find("Mug"));
        }
        if (chair1Destroyed && SceneManager.GetActiveScene().name == "Room 1") {
            Destroy(GameObject.Find("Chair"));
        }
        if (chair2Destroyed && SceneManager.GetActiveScene().name == "Room 5") {
            Destroy(GameObject.Find("Chair"));
        }
        if (nightstandDestroyed) {
            Destroy(GameObject.Find("Nightstand"));
        } 
        if (goldOrb) {
            Destroy(GameObject.Find("Gold Orb"));
        } 
        if (itemsGotten) {
            Destroy(GameObject.Find("Mysterious Liquid"));
            Destroy(GameObject.Find("Silver Paint"));
        } 
        if (wallOpen) {
            if (GameObject.Find("Wall Hole") != null) {
                GameObject.Find("Wall Hole").GetComponent<SpriteRenderer>().sortingOrder = 2;
                if (GameObject.Find("Wad of Cash") != null) {
                    GameObject.Find("Wad of Cash").GetComponent<SpriteRenderer>().sortingOrder = 3;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (endingCutscene >= 1) {
            movement = false;
        }
        
        if (endingCutscene == 1) {
            endingCutscene = 2;        
            string[][] lines;
            if (goodEnding) {
                lines = new string[][] {
                    new string[] {"Well, here we are."},
                    new string[] {"You must be wondering what just happened."},
                    new string[] {"Well, after you helped me knock out that guard, I took his keys and escaped 9 Rooms for good."},
                    new string[] {"I got my memories back. I know why I was there."},
                    new string[] {"Maybe you already guessed this, but..."},
                    new string[] {"$I'm one of the most feared criminals in the world."},
                    new string[] {"No prison could hold me. So they put me in 9 Rooms and took away my memories and my body. It would have worked, too..."},
                    new string[] {"...if you hadn't helped me escape."},
                    new string[] {"Oh, and I could have gotten my old form back if I wanted to. But I decided against it."},
                    new string[] {"After all, I'm unrecognizable in this new form. And it makes stealth much easier."},
                    new string[] {"I really must thank you, " + yourName + ". Your part in this was instrumental."},
                    new string[] {"Now I can go back to my life of crime. And you know what the best part is?"},
                    new string[] {"It's all your fault, " + yourName + "."},
                    new string[] {"And there's nothing you can do about it."},
                    new string[] {"In fact, I've decided I'm going to sit here for a bit and just...take it all in. Gloat a little bit. You know."}
                };
            } else {
                lines = new string[][] {
                    new string[] {"Well, here we are."},
                    new string[] {"You must be wondering what just happened."},
                    new string[] {"Well, after you helped me knock out that guard, I took his keys and escaped 9 Rooms for good."},
                    new string[] {"I got my memories back. I know why I was there."},
                    new string[] {"Maybe you already guessed this, but..."},
                    new string[] {"$I'm one of the most feared criminals in the world."},
                    new string[] {"No prison could hold me. So they put me in 9 Rooms and took away my memories and my body. It would have worked, too..."},
                    new string[] {"...if you hadn't helped me escape."},
                    new string[] {"Oh, and I could have gotten my old form back if I wanted to. But I decided against it."},
                    new string[] {"After all, I'm unrecognizable in this new form. And it makes stealth much easier."},
                    new string[] {"I really must thank you, " + yourName + ". Your part in this was instrumental."},
                    new string[] {"Now I can go back to my life of crime. And you know what the best part is?"},
                    new string[] {"It's all your fault, " + yourName + "."},
                    new string[] {"Goodbye, now."}
                };
            }
            CreateCustomDialogue(lines, "");
        }
        if (goodEnding && endDialogue && endingCutscene == 2 && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown("space"))) {
            // Destroy(GameObject.Find("Player"));
            GameObject.Find("Blackout Canvas").GetComponent<Canvas>().sortingOrder = 10001;
            GameObject.Find("Canvas").GetComponent<Canvas>().sortingOrder = 10002;
            BlackoutScript bs = (BlackoutScript) GameObject.Find("Blackout").GetComponent(typeof(BlackoutScript));
            bs.SetColor(0f, 0f, 0f);
            bs.FadeToBlack(1f, false, 0f);
            GameObject.Find("Stabbing Sound").GetComponent<AudioSource>().Play();
            StartCoroutine(GoodEndCutscene());
        }
        if (goodEnding && endDialogue && endingCutscene == 3) {
            endingCutscene++;
            TextRender.textSpeed = 0.02f;
            TextRender.tick = 5;
            string[][] lines = new string[][] {
                new string[] {"..."},
                new string[] {"You did it..."},
                new string[] {"Congratulations. And again..."},
                new string[] {"Thank you for playing my game! :)"}
            };
            CreateCustomDialogue(lines, "");
        }
        if (!goodEnding && endDialogue && endingCutscene == 2) {
            endingCutscene += 2;
            Destroy(GameObject.Find("Player"));
            string[][] lines = new string[][] {
                new string[] {"..."},
                new string[] {"This wasn't the best timeline..."},
                new string[] {"You need to try again..."}
            };
            CreateCustomDialogue(lines, "");
        }
        if (endDialogue && !firstCutscene && TextRender.gotName) {
            firstCutscene = true;
            string[][] lines = new string[][] {
                new string[] {yourName + ", huh? That's a nice name."},
                new string[] {"I'd tell you my name, but I can't remember it. I can't remember anything about who I was before I woke up here."},
                new string[] {"The only thing I do remember is that I used to look different. I think whoever put me here took away my real form for some reason."},
                new string[] {"I've done some research about this place - it's called 9 Rooms. Most of the rooms are secured by a single password door."},
                new string[] {"However, the center room has much more security than the others. There must be something valuable in there, something that might help me escape."},
                new string[] {"There are four doors blocking its entrance - each door can be opened by placing the respective orb into its pedestal."},
                new string[] {"I haven't been able to find any of the orbs on my own."},
                new string[] {"But with your help, " + yourName + ", I think I can escape this place and get my memories and body back."},
                new string[] {"Here are the controls:"},
                new string[] {"UP, DOWN, LEFT, RIGHT - Move\nENTER - Interact/Talk\nSPACE (while touching an entity) - Use Item on Entity"},
                new string[] {"SPACE (while not touching entity) - Use Item/Combine Items\nENTER (during a combination) - Select Second Item to Combine\n"},
                new string[] {"A, D - Select Item in Inventory\nESCAPE - View Controls\nC - View Credits"},
                new string[] {"Note: the controls and credits can only be viewed if you're not touching an entity."}
            };
            CreateCustomDialogue(lines, "");
        }
        if (SceneManager.GetActiveScene().name != RoomStatic.currentRoom) {
            this.transform.position = PlayerPositionManager();
            RoomStatic.currentRoom = SceneManager.GetActiveScene().name;
        }
        if (startedBlackout && GameObject.Find("Blackout").GetComponent<Image>().color.a == 0f) {
            movement = true;
            startedBlackout = false;
        }
        if (movement) {
            if (Input.GetKeyDown("up")) {
                dy = speed;
            }

            if (Input.GetKeyDown("down")) {
                dy = -1 * speed;
            }

            if (Input.GetKeyDown("left")) {
                dx = -1 * speed;
            }

            if (Input.GetKeyDown("right")) {
                dx = speed;
            }

            if (Input.GetKeyUp("up")) {
                dy = 0;
            }

            if (Input.GetKeyUp("down")) {
                dy = 0;
            }

            if (Input.GetKeyUp("left")) {
                dx = 0;
            }

            if (Input.GetKeyUp("right")) {
                dx = 0;
            }
            if (Input.GetKeyDown(KeyCode.Escape) && NPC == null) {
                string[][] lines = new string[][] {
                    new string[] {"UP, DOWN, LEFT, RIGHT - Move\nENTER - Interact/Talk\nSPACE (while touching an entity) - Use Item on Entity"},
                    new string[] {"SPACE (while not touching entity) - Use Item/Combine Items\nENTER (during a combination) - Select Second Item to Combine\n"},
                    new string[] {"A, D - Select Item in Inventory\nESCAPE - View Controls\nC - View Credits"},
                    new string[] {"Note: the controls and credits can only be viewed if you're not touching an entity."}
                };
                CreateCustomDialogue(lines, "Controls");
            }
            if (Input.GetKeyDown("c") && NPC == null) {
                if (formless3Gotten == false) {
                    formless3Gotten = true;
                    InventoryScript ins = (InventoryScript) GameObject.Find("InventoryBar").GetComponent(typeof(InventoryScript));
                    ins.AddInventory("Formless Object #3");
                    string[][] lines = new string[][] {
                        new string[] {"Game programmed by chpas"},
                        new string[] {"Assets:\nDog by Benvictus\nMysterious Liquid by Onocentaur\nStorage Key by dustdfg"},
                        new string[] {"Doors by Pipoya\nInventory Bar by Dev Bandeira\nSkeleton by Calciumtrice"},
                        new string[] {"Furniture by sierrassets\nTileset/NPCs by 0x72\nFont - \"Menu Card\" by SysL"},
                        new string[] {"Music:\n\"Awkward Meeting\" Kevin MacLeod (incompetech.com)"},
                        new string[] {"Licensed under Creative Commons: By Attribution 4.0\nLicense: http://creativecommons.org/licenses/by/4.0/"},
                        new string[] {"Dialogue Sound by Alan Dalcastagne"},
                        new string[] {"Thank you for playing my game!"},
                        new string[] {"..."},
                        new string[] {"Hey, thanks for actually reading the credits. It means a lot."},
                        new string[] {"Here's Formless Object #3 to show my gratitude."},
                        new string[] {"On its own, it's not very useful, but when combined with the other 2...some say it has the power to alter timelines."},
                        new string[] {"You should try and find the other 2."},
                    };
                    CreateCustomDialogue(lines, "Credits");
                } else {
                    string[][] lines = new string[][] {
                        new string[] {"Game programmed by chpas"},
                        new string[] {"Assets:\nDog by Benvictus\nMysterious Liquid by Onocentaur\nStorage Key by dustdfg"},
                        new string[] {"Doors by Pipoya\nInventory Bar by Dev Bandeira\nSkeleton by Calciumtrice"},
                        new string[] {"Furniture by sierrassets\nTileset/NPCs by 0x72\nFont - \"Menu Card\" by SysL"},
                        new string[] {"Music:\n'Awkward Meeting' Kevin MacLeod (incompetech.com)"},
                        new string[] {"Licensed under Creative Commons: By Attribution 4.0\nLicense: http://creativecommons.org/licenses/by/4.0/"},
                        new string[] {"Dialogue Sound by Alan Dalcastagne"},
                        new string[] {"Thank you for playing my game!"},
                        new string[] {"..."},
                        new string[] {"Good luck."},
                    };
                    CreateCustomDialogue(lines, "Credits");
                }

            }
            if (chooseSecondItem || !endDialogue) {
                dx = 0;
                dy = 0;
                movement = false;
            }
            if (Input.GetKey(KeyCode.Return)) {
                if (NPC != null && NPC.name == "OverworldRug" && (Time.time - time > 0.25)) {
                    float rightXBound = 2.29f;
                    if (TrapdoorScript.timesJumped >= 3) {
                        rightXBound = 5.22f;
                    }
                    if (!(this.transform.position.x > -2.18 && this.transform.position.x < rightXBound && this.transform.position.y < 1.39 && this.transform.position.y > -3.02)) {
                        CreateDialogue();
                    }
                }
                else if (NPC != null && (Time.time - time > 0.25)) {
                    if (NPC.name == "Miner" && MinerScript.isAsleep == true && pickaxeGotten == false) {
                        string[][] lines = new string[][] {
                            new string[] {"$While he's sleeping, I'll steal his pickaxe."},
                        };
                        pickaxeGotten = true;
                        CreateCustomDialogue(lines, "");
                        InventoryScript ins = (InventoryScript) GameObject.Find("InventoryBar").GetComponent(typeof(InventoryScript));
                        ins.AddInventory("Pickaxe");
                    } else {
                        CreateDialogue();
                    }
                }
                if (NPC != null) {
                    if (endDialogue && NPC.name == "BatDeskTop") {
                        if (!batGotten) {
                            InventoryScript ins = (InventoryScript) GameObject.Find("InventoryBar").GetComponent(typeof(InventoryScript));
                            ins.AddInventory("Baseball Bat");
                            Destroy(GameObject.Find("Big Baseball Bat"));
                            batGotten = true;
                        }

                    } 
                    if (endDialogue && NPC.name == "AxeDeskTop") {
                        // insert blackout here
                        if (!axeGotten) {
                            InventoryScript ins = (InventoryScript) GameObject.Find("InventoryBar").GetComponent(typeof(InventoryScript));
                            ins.AddInventory("Axe");
                            Destroy(GameObject.Find("Axe"));
                            // unblackout
                            axeGotten = true;
                        }
                    } 
                    if (endDialogue && NPC.name == "Table") {
                        if (!mugGotten) {
                            InventoryScript ins = (InventoryScript) GameObject.Find("InventoryBar").GetComponent(typeof(InventoryScript));
                            ins.AddInventory("Mug With Coffee Powder");
                            Destroy(GameObject.Find("Mug"));
                            mugGotten = true;
                        }
                    }
                    if (endDialogue && NPC.name == "Trapdoor Hole") {
                        if (!itemsGotten) {
                            InventoryScript ins = (InventoryScript) GameObject.Find("InventoryBar").GetComponent(typeof(InventoryScript));
                            ins.AddInventory("Mysterious Liquid");
                            ins.AddInventory("Silver Paint");
                            Destroy(GameObject.Find("Mysterious Liquid"));
                            Destroy(GameObject.Find("Silver Paint"));
                            itemsGotten = true;
                        }
                    }
                }
                
            }

            if (Input.GetKeyDown("space")) {
                InventoryScript ins = (InventoryScript) GameObject.Find("InventoryBar").GetComponent(typeof(InventoryScript));
                if (NPC != null && NPC.name == "Goo" && ins.Selected() == "Mop") {
                    GooScript.gooCleaned.Add(RoomStatic.currentRoom);
                    Destroy(GameObject.Find("Goo"));
                    if (ins.Contains("Bucket")) {
                        ins.RemoveInventory("Bucket");
                        ins.AddInventory("Bucket of Goo");
                    }
                } else if (RoomStatic.currentRoom == "Room 1" && NPC != null) {
                    NPCScript npcs = (NPCScript) NPC.GetComponent(typeof(NPCScript));
                    if (npcs.GetName() == "PasswordDoor" && ins.Selected() == "Card Key") {
                        DoorScript ds = (DoorScript) NPC.GetComponent(typeof(DoorScript));
                        ds.OpenDoor();
                        ins.RemoveInventory("Card Key");
                    }
                    if (npcs.GetName() == "Chair" && ins.Selected() == "Axe") {
                        chair1Destroyed = true;
                        Destroy(GameObject.Find("Chair"));
                        ins.AddInventory("Desk Chair Wood");
                    }
                    if (npcs.GetName() == "Gold Pedestal" && ins.Selected() == "Gold Orb") {
                        doorsUnlocked++;
                        PedestalScript ps = (PedestalScript) NPC.GetComponent(typeof(PedestalScript));
                        ps.OpenPedestal();
                        ins.RemoveInventory("Gold Orb");
                        string[][] lines = new string[][] {
                            new string[] {"I placed the Gold Orb on the Gold Pedestal."},
                            new string[] {"The Gold Door should be open now."}
                        };
                        CreateCustomDialogue(lines, "");
                    }
                    if (npcs.GetName() == "Jeffrey" && ins.Selected() == "Gold Collar" && !DogNPCLogic.dogShown) {
                        DogNPCLogic.dogShown = true;
                        ins.AddInventory("Dog Biscuit");
                        string[][] lines = new string[][] {
                            new string[] {"Is this Rufus' collar? Did you lose him or something? It's designed to not come off."},
                            new string[] {"It's not broken or anything, though..."},
                            new string[] {"D-did you kill my dog?"},
                            new string[] {"Don't even answer, I can see it in your face. You killed Rufus."},
                            new string[] {"*sniffle* Guess I don't need this dog biscuit anymore. Take it to remind yourself of what you've done. It was his favorite kind."},
                            new string[] {"$You're a monster."}
                        };
                        CreateCustomDialogue(lines, "Jeffrey");
                    }
                } else if (RoomStatic.currentRoom == "Room 2" && NPC != null) {
                    NPCScript npcs = (NPCScript) NPC.GetComponent(typeof(NPCScript));
                    if (npcs.GetName() == "Jean-Luc" && ins.Selected() == "Livre De Potions") {
                        FrenchScript.bookGiven = true;
                        ins.RemoveInventory("Livre De Potions");
                        CreateDialogue();
                    }
                    else if (npcs.GetName() == "Jean-Luc" && ins.Selected() == "Baseball Bat" && FrenchScript.talkedAfterRefusal) {
                        ins.AddInventory("Potion Recipes");
                        ins.RemoveInventory("Baseball Bat");
                        FrenchScript.talkedAfterRefusal = false;
                        FrenchScript.downed = true;
                        ((FrenchScript) npcs).SetSprite("Frenchman-Hurt");
                        movement = false;
                        startedBlackout = true;
                        BlackoutScript bs = (BlackoutScript) GameObject.Find("Blackout").GetComponent(typeof(BlackoutScript));
                        if (!BlackoutScript.fadeToBlack && !BlackoutScript.fadeFromBlack) {
                            bs.FadeToBlack(1f, false, 0.002f);
                            AudioScript.StopAudio();
                        }
                        StartCoroutine(WaitAfterThunk());
                    }
                    else if (npcs.GetName() == "Jean-Luc" && ins.Selected() == "Scissors" && FrenchScript.downed && !hairGotten) {
                        ins.AddInventory("Mustache Hair");
                        hairGotten = true;
                        string[][] lines = new string[][] {
                            new string[] {"Ah!"},
                        };
                        CreateCustomDialogue(lines, "Frenchman");
                    }
                } else if (RoomStatic.currentRoom == "Room 3" && NPC != null) {
                    NPCScript npcs = (NPCScript) NPC.GetComponent(typeof(NPCScript));
                    if (npcs.GetName() == "Sink" && ins.Selected() == "Mug With Coffee Powder") {
                        string[][] lines = new string[][] {
                            new string[] {"I filled the mug up with water."},
                            new string[] {"Now there's coffee in it, but it's cold."},
                        };
                        ins.RemoveInventory("Mug With Coffee Powder");
                        ins.AddInventory("Mug With Coffee");
                        CreateCustomDialogue(lines, "");
                    }
                    else if (npcs.GetName() == "Sink" && ins.Selected() == "Mug") {
                        string[][] lines = new string[][] {
                            new string[] {"I filled the mug up with water."},
                        };
                        ins.RemoveInventory("Mug");
                        ins.AddInventory("Mug With Water");
                        CreateCustomDialogue(lines, "");
                    }
                    else if (npcs.GetName() == "Sink" && ins.Selected() == "Bloody Stone") {
                        string[][] lines = new string[][] {
                            new string[] {"..."},
                        };
                        ins.RemoveInventory("Bloody Stone");
                        ins.AddInventory("Sharp Stone");
                        CreateCustomDialogue(lines, "");
                    }
                    if (npcs.GetName() == "Microwave" && ins.Selected() == "Mug With Coffee") {
                        string[][] lines = new string[][] {
                            new string[] {"I microwaved the coffee. Now it's ready to serve."},
                        };
                        ins.RemoveInventory("Mug With Coffee");
                        ins.AddInventory("Mug With Hot Coffee");
                        CreateCustomDialogue(lines, "");
                    }
                    else if (npcs.GetName() == "Microwave" && ins.Selected() == "Mug With Coffee Powder") {
                        string[][] lines = new string[][] {
                            new string[] {"If I don't put water in this first, I'll just have hot coffee powder."},
                            new string[] {"That would be pretty useless."},
                        };
                        CreateCustomDialogue(lines, "");
                    }
                    else if (npcs.GetName() == "Microwave" && ins.Selected() == "Dog") {
                        string[][] lines = new string[][] {
                            new string[] {"No."},
                        };
                        CreateCustomDialogue(lines, "");
                    }
                } else if (RoomStatic.currentRoom == "Room 4" && NPC != null) { 
                    NPCScript npcs = (NPCScript) NPC.GetComponent(typeof(NPCScript));
                    if (npcs.GetName() == "OrbBookshelf" && ins.Selected() == "Ladder" && !goldOrb) {
                        string[][] lines = new string[][] {
                            new string[] {"I climbed up the ladder and grabbed the Gold Orb."},
                            new string[] {"Now I need to put it in its pedestal."},
                        };
                        ins.AddInventory("Gold Orb");
                        Destroy(GameObject.Find("Gold Orb"));
                        goldOrb = true;
                        CreateCustomDialogue(lines, "");
                    }
                    if (npcs.GetName() == "Silver Pedestal" && ins.Selected() == "Silver Orb") {
                        doorsUnlocked++;
                        PedestalScript ps = (PedestalScript) NPC.GetComponent(typeof(PedestalScript));
                        ps.OpenPedestal();
                        ins.RemoveInventory("Silver Orb");
                        string[][] lines = new string[][] {
                            new string[] {"I placed the Silver Orb on the Silver Pedestal."},
                            new string[] {"The Silver Door should be open now."}
                        };
                        CreateCustomDialogue(lines, "");
                    }
                } else if (RoomStatic.currentRoom == "Room 5" && NPC != null) { 
                    NPCScript npcs = (NPCScript) NPC.GetComponent(typeof(NPCScript));
                    if (npcs.GetName() == "Chair" && ins.Selected() == "Axe") {
                        chair2Destroyed = true;
                        Destroy(GameObject.Find("Chair"));
                        ins.AddInventory("Table Chair Wood");
                    }
                    if (npcs.GetName() == "Melvin the Miner" && ins.Selected() == "Spiked Coffee") {
                        MinerScript.isAsleep = true;
                        ins.RemoveInventory("Spiked Coffee");
                        ins.AddInventory("Mug");
                        ((MinerScript) npcs).SetSprite("Miner-Asleep");
                        string[][] lines = new string[][] {
                            new string[] {"He's fallen asleep."},
                            new string[] {"I took the mug back and spilled out the remaining coffee. It's empty now."}
                        };
                        CreateCustomDialogue(lines, "");
                    }
                    else if (npcs.GetName() == "Melvin the Miner" && ins.Selected() == "Mug With Hot Coffee") {
                        movement = false;
                        dy = 0;
                        dx = 0;
                        MinerScript ms = (MinerScript) GameObject.Find("Miner").GetComponent(typeof(MinerScript));
                        ms.PlayAnimation();
                    }
                    else if (npcs.GetName() == "Melvin the Miner" && ins.Selected() == "Mug With Coffee Powder") {
                        string[][] lines = new string[][] {
                            new string[] {"It's just coffee powder. I can't give him this."},
                        };
                        CreateCustomDialogue(lines, "");
                    }
                    else if (npcs.GetName() == "Melvin the Miner" && ins.Selected() == "Mug With Coffee") {
                        string[][] lines = new string[][] {
                            new string[] {"It's cold. I can't give him this."},
                        };
                        CreateCustomDialogue(lines, "");
                    }
                    if (npcs.GetName() == "Bonemaster" && ins.Selected() == "Bones" && !BonemasterScript.bonesShown) {
                        BonemasterScript.bonesShown = true;
                        string[][] lines = new string[][] {
                            new string[] {"Hmmm..."},
                            new string[] {"Strong, yes..."},
                            new string[] {"These will suffice. Talk to me again to play my game."},
                        };
                        CreateCustomDialogue(lines, "Bonemaster");
                    }
                } else if (RoomStatic.currentRoom == "Room 6" && NPC != null) { 
                    NPCScript npcs = (NPCScript) NPC.GetComponent(typeof(NPCScript));
                    if (npcs.GetName() == "Nightstand" && ins.Selected() == "Axe") {
                        nightstandDestroyed = true;
                        Destroy(GameObject.Find("Nightstand"));
                        ins.AddInventory("Nightstand Wood");
                    }
                    if (npcs.GetName() == "Old Man" && ins.Selected() == "Dog Biscuit") {
                        CrazyManScript.biscuitGiven = true;
                        ins.RemoveInventory("Dog Biscuit");
                        ins.AddInventory("Stinky Cheese");
                        string[][] lines = new string[][] {
                            new string[] {"T-thanks..."},
                            new string[] {"*chomp chomp chomp*"},
                            new string[] {"Ah, much better. Here, take this cheese as a token of my thanks."},
                            new string[] {"...I could've just eaten that, couldn't I have."}
                        };
                        CreateCustomDialogue(lines, "Old Man");
                    }
                } else if (RoomStatic.currentRoom == "Room 7" && NPC != null) {
                    NPCScript npcs = (NPCScript) NPC.GetComponent(typeof(NPCScript));
                    if (npcs.GetName() == "Storage Locker" && ins.Selected() == "Storage Key") {
                        StorageScript.unlocked = true;
                        ins.RemoveInventory("Storage Key");
                        string[][] lines = new string[][] {
                            new string[] {"I unlocked the storage locker. There was a hammer and some nails inside."},
                        };
                        CreateCustomDialogue(lines, "");
                        ins.AddInventory("Hammer and Nails");
                    } 
                    if (npcs.GetName() == "Hole" && ins.Selected() == "Stinky Cheese") {
                        HoleScript.ratFed = true;
                        ins.RemoveInventory("Stinky Cheese");
                        ins.AddInventory("Bronze Orb");
                        string[][] lines = new string[][] {
                            new string[] {"The rat appreciated my gift and nudged the Bronze Orb out of the hole."},
                        };
                        CreateCustomDialogue(lines, "");
                    }
                } else if (RoomStatic.currentRoom == "Room 8" && NPC != null) {
                    NPCScript npcs = (NPCScript) NPC.GetComponent(typeof(NPCScript));
                    if (npcs.GetName() == "Bronze Pedestal" && ins.Selected() == "Bronze Orb") {
                        doorsUnlocked++;
                        PedestalScript ps = (PedestalScript) NPC.GetComponent(typeof(PedestalScript));
                        ps.OpenPedestal();
                        ins.RemoveInventory("Bronze Orb");
                        string[][] lines = new string[][] {
                            new string[] {"I placed the Bronze Orb on the Bronze Pedestal."},
                            new string[] {"The Bronze Door should be open now."}
                        };
                        CreateCustomDialogue(lines, "");
                    }
                    if (npcs.GetName() == "Hole" && ins.Selected() == "Pickaxe" && !wallOpen) {
                        GameObject.Find("Wall Hole").GetComponent<SpriteRenderer>().sortingOrder = 2;
                        GameObject.Find("Wad of Cash").GetComponent<SpriteRenderer>().sortingOrder = 3;
                        ins.AddInventory("Sharp Stone");
                        wallOpen = true;
                    }
                    if (npcs.GetName() == "???") {
                        if (ins.Selected() == "Dog") {
                            SacrificeScript ss = (SacrificeScript) NPC.GetComponent(typeof(SacrificeScript));
                            ins.RemoveInventory("Dog");
                            SacrificeScript.dogOn = true;
                            ss.SetSprite("Sacrifice Pedestal Dog");
                            string[][] lines = new string[][] {
                                new string[] {"I put the dog on the pedestal..."},
                                new string[] {"I don't like where this is going."}
                            };
                            CreateCustomDialogue(lines, "");
                        }
                        else if (SacrificeScript.dogOn && !SacrificeScript.dogKilled) {
                            if (ins.Selected() == "Axe") {
                                string[][] lines = new string[][] {
                                    new string[] {"$Only good for cutting wood."}
                                };
                                CreateCustomDialogue(lines, "");
                            } else if (ins.Selected() == "Sharp Stone") {
                                ins.RemoveInventory("Sharp Stone");
                                SacrificeScript.stoneUsed = true;
                                CreateDialogue();
                            } else {
                                string[][] lines = new string[][] {
                                    new string[] {"$Not sharp enough."}
                                };
                                CreateCustomDialogue(lines, "");
                            }
                        }
  
                    }
                } else if (RoomStatic.currentRoom == "Room 9" && NPC != null) {
                    NPCScript npcs = (NPCScript) NPC.GetComponent(typeof(NPCScript));
                    if (npcs.GetName() == "Janitor" && ins.Selected() == "Bucket of Goo" && GooScript.gooCleaned.Count == 4 && !JanitorScript.gooShown) {
                        JanitorScript.gooShown = true;
                        string[][] lines = new string[][] {
                            new string[] {"Oh, you actually did it."},
                            new string[] {"That's a bit awkward. I need to report you for being here."},
                            new string[] {"Maybe if you gave me a little something...you know...budget cuts are pretty severe these days..."}
                        };
                        CreateCustomDialogue(lines, "Janitor");
                    }
                    else if (npcs.GetName() == "Janitor" && ins.Selected() == "Wad of Cash" && JanitorScript.gooShown && !JanitorScript.cashShown) {
                        JanitorScript.cashShown = true;
                        ins.RemoveInventory("Wad of Cash");
                        string[][] lines = new string[][] {
                            new string[] {"Ooh, cash."},
                            new string[] {"That looks like a lot of singles...I'd better count it to be sure."},
                        };
                        CreateCustomDialogue(lines, "Janitor");
                    }
                    else if (npcs.GetName() == "Janitor" && ins.Selected() == "Dangerous Rag" && JanitorScript.cashShown) {
                        startedBlackout = true;
                        BlackoutScript bs = (BlackoutScript) GameObject.Find("Blackout").GetComponent(typeof(BlackoutScript));
                        Color color = GameObject.Find("ItemText").GetComponent<TextMeshProUGUI>().color;
                        GameObject.Find("ItemText").GetComponent<TextMeshProUGUI>().color = new Color(color.r, color.g, color.b, 0f);
                        if (!BlackoutScript.fadeToBlack && !BlackoutScript.fadeFromBlack) {
                            bs.SetColor(0.25f, 0.25f, 0.25f);
                            bs.FadeToBlack(0.8f, false, 0.002f);
                            AudioScript.StopAudio();
                            Destroy(GameObject.Find("Janitor"));
                            movement = false;
                            StartCoroutine(StartEndingCutscene());
                        }
                    }
                } else if (endDialogue && !inCombination) {
                    if (ins.Selected() == "Potion Recipes") {
                        string[][] lines = new string[][] {
                            new string[] {"It's two translated pages of the potion recipes book."},
                            new string[] {"Read them?"},
                            new string[] {"/choice/"},
                            new string[] {"PHASING POTION\nAllows the user to phase through walls. Does not work if the walls are too thick.", "/end/"},
                            new string[] {"First, add gold to plain water. Then, add mold to the gold water. Then, add hair to the mold gold water."},
                            new string[] {"Finally, add stone to the mixture. Be sure that the stone has no blood on it, or the potion won't work."},
                            new string[] {"SLEEPING POTION\nMakes the user fall asleep."},
                            new string[] {"First, add gold to plain water. Then, add stone to the gold water. Then, add any goo type to the mixture."},
                            new string[] {"Finally, add any sleeping draught to the potion to finish it off."},
                        };
                        CreateCustomDialogue(lines, "");
                    } 
                    else if (ins.Selected() == "Sleeping Potion" && !drankSleepingPotion) {
                        drankSleepingPotion = true;
                        ins.RemoveInventory("Sleeping Potion");
                        ins.AddInventory("Formless Object #2");
                        ins.AddInventory("Mug");
                        movement = false;
                        dx = 0;
                        dy = 0;
                        startedBlackout = true;
                        BlackoutScript bs = (BlackoutScript) GameObject.Find("Blackout").GetComponent(typeof(BlackoutScript));
                        if (!BlackoutScript.fadeToBlack && !BlackoutScript.fadeFromBlack) {
                            bs.FadeToBlack(1f, false, 0.02f);
                        }
                        StartCoroutine(WaitAfterDrink());
                    }
                    else if (ins.Selected() == "Sleeping Potion" && drankSleepingPotion) {
                        string[][] lines = new string[][] {
                            new string[] {"I don't want to drink this again."},
                        };
                        CreateCustomDialogue(lines, "");
                    }
                    else if (ins.Selected() == "Phasing Potion") {
                        string[][] lines;
                        if (RoomStatic.currentRoom == "Room 2") {
                            ins.RemoveInventory("Phasing Potion");
                            ins.AddInventory("Mug");
                            this.transform.position = new Vector3(2, 10000, this.transform.position.z);
                        } else {
                            lines = new string[][] {
                                new string[] {"The walls are too thick for me to use this here."},
                            };
                            CreateCustomDialogue(lines, "");
                        }
                    }
                    else if (ins.Selected() == "Potion") {
                        string[][] lines = new string[][] {
                            new string[] {"Nothing happened."},
                        };
                        ins.RemoveInventory("Potion");
                        ins.AddInventory("Mug");
                        CreateCustomDialogue(lines, "");
                    }
                    else if (ins.GetSize() > 1 && NPC == null){
                        TextRender renderer = (TextRender) GameObject.Find("DialogueBox").GetComponent(typeof(TextRender));
                        Image image = GameObject.Find("DialogueBox").GetComponent<Image>();
                        movement = false;
                        dx = 0;
                        dy = 0;
                        var tempColor = image.color;
                        tempColor.a = 1f;
                        image.color = tempColor;
                        endDialogue = false;
                        inCombination = true;
                        renderer.StartDialogue(new string[][] {
                            new string[] {"What do you want to combine the " + ins.Selected() + " with?"}
                        });
                        firstItem = ins.Selected();
                        chooseSecondItem = true;
                    }
                }
            }
        }
        if (!movement) {
            if (Input.GetKey(KeyCode.Return)) {
                if (chooseSecondItem && endDialogue && ((Time.time - time) > 0.25)) {
                    InventoryScript ins = (InventoryScript) GameObject.Find("InventoryBar").GetComponent(typeof(InventoryScript));
                    string secondItem = ins.Selected();
                    CombinationScript cs = (CombinationScript) this.GetComponent(typeof(CombinationScript));
                    Image image = GameObject.Find("DialogueBox").GetComponent<Image>();
                    movement = false;
                    var tempColor = image.color;
                    tempColor.a = 1f;
                    image.color = tempColor;
                    endDialogue = false;
                    TextRender renderer = (TextRender) GameObject.Find("DialogueBox").GetComponent(typeof(TextRender));
                    renderer.StartDialogue(cs.CombineItems(firstItem, secondItem));
                    movement = true;
                    inCombination = false;
                    firstItem = "";
                    chooseSecondItem = false;
                }
            }
        }
        
        if (dy > 0 && dx > 0) {
            spriteRenderer.sprite = playerSprites[8];
            direction = "upright";
        } else if (dy > 0 && dx < 0) {
            spriteRenderer.sprite = playerSprites[7];
            direction = "upleft";
        } else if (dy > 0 && dx == 0) {
            spriteRenderer.sprite = playerSprites[6];
            direction = "up";
        } else if (dy == 0 && dx > 0) {
            spriteRenderer.sprite = playerSprites[5];
            direction = "right";
        } else if (dy == 0 && dx < 0) {
            spriteRenderer.sprite = playerSprites[3];
            direction = "left";
        } else if (dy == 0 && dx == 0) {
            spriteRenderer.sprite = playerSprites[4];
            direction = "none";
        } else if (dy < 0 && dx > 0) {
            spriteRenderer.sprite = playerSprites[2];
            direction = "downright";
        } else if (dy < 0 && dx < 0) {
            spriteRenderer.sprite = playerSprites[1];
            direction = "downleft";
        } else if (dy < 0 && dx == 0) {
            spriteRenderer.sprite = playerSprites[0];
            direction = "down";
        }
    }

    void FixedUpdate() {
        transform.position = new Vector3(transform.position.x + dx, transform.position.y + dy, transform.position.z);
        if (SceneManager.GetActiveScene().name == "Room 1") {
            if (transform.position.y > 10.2) {
                RoomStatic.entrance = "down";
                SceneManager.LoadScene("Room 3");
            }
            if (transform.position.x > 10.2) {
                RoomStatic.entrance = "left";
                SceneManager.LoadScene("Room 2");
            }
        }
        if (SceneManager.GetActiveScene().name == "Room 2") {
            if (transform.position.x < -10.2) {
                RoomStatic.entrance = "right";
                SceneManager.LoadScene("Room 1");
            }
            if (transform.position.y > 10.5 && transform.position.x > 6) {
                RoomStatic.entrance = "down";
                SceneManager.LoadScene("Room 6");
            }
            else if (transform.position.y > 10.5) {
                RoomStatic.entrance = "down";
                SceneManager.LoadScene("Room 9");
            }
        }
        if (SceneManager.GetActiveScene().name == "Room 3") {
            if (transform.position.y < -9) {
                RoomStatic.entrance = "up";
                SceneManager.LoadScene("Room 1");
            }
            if (transform.position.y > 10.2) {
                RoomStatic.entrance = "down";
                SceneManager.LoadScene("Room 4");
            }
        }
        if (SceneManager.GetActiveScene().name == "Room 4") {
            if (transform.position.y < -9) {
                RoomStatic.entrance = "up";
                SceneManager.LoadScene("Room 3");
            }

            if (transform.position.x > 10.2) {
                RoomStatic.entrance = "left";
                SceneManager.LoadScene("Room 5");
            }
        }
        if (SceneManager.GetActiveScene().name == "Room 5") {
            if (transform.position.x < -10.2) {
                RoomStatic.entrance = "right";
                SceneManager.LoadScene("Room 4");
            }
        }
        if (SceneManager.GetActiveScene().name == "Room 6") {
            if (transform.position.y < -9) {
                RoomStatic.entrance = "right";
                SceneManager.LoadScene("Room 2");
            }
            if (transform.position.y > 10.2) {
                RoomStatic.entrance = "down";
                SceneManager.LoadScene("Room 7");
            }
        }
        if (SceneManager.GetActiveScene().name == "Room 7") {
            if (transform.position.y < -9) {
                RoomStatic.entrance = "up";
                SceneManager.LoadScene("Room 6");
            }
            if (transform.position.y > 10.2) {
                RoomStatic.entrance = "down";
                SceneManager.LoadScene("Room 8");
            }
        }
        if (SceneManager.GetActiveScene().name == "Room 8") {
            if (transform.position.y < -9) {
                RoomStatic.entrance = "up";
                SceneManager.LoadScene("Room 7");
            }
        }
        if (SceneManager.GetActiveScene().name == "Room 9") {
            if (transform.position.y < -9) {
                RoomStatic.entrance = "up";
                SceneManager.LoadScene("Room 2");
            }
        }
    }

    void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.tag == "NPC") {
            NPC = other.gameObject;
        }
    }

    void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.tag == "NPC") {
            NPC = null;
        }
    }

    Vector3 PlayerPositionManager() {
        if (SceneManager.GetActiveScene().name == "Room 1" && RoomStatic.entrance == "up") {
            return new Vector3(1f, 10f, this.transform.position.z);
        }
        if (SceneManager.GetActiveScene().name == "Room 1" && RoomStatic.entrance == "right") {
            return new Vector3(10f, 0f, this.transform.position.z);
        }
        if (SceneManager.GetActiveScene().name == "Room 2" && RoomStatic.entrance == "left") {
            return new Vector3(-10f, 0f, this.transform.position.z);
        }
        if (SceneManager.GetActiveScene().name == "Room 2" && RoomStatic.entrance == "right") {
            return new Vector3(7f, 10f, this.transform.position.z);
        }
        if (SceneManager.GetActiveScene().name == "Room 2" && RoomStatic.entrance == "up") {
            return new Vector3(0f, 10f, this.transform.position.z);
        }
        if (SceneManager.GetActiveScene().name == "Room 3" && RoomStatic.entrance == "down") {
            return new Vector3(1f, -9f, this.transform.position.z);
        }
        if (SceneManager.GetActiveScene().name == "Room 3" && RoomStatic.entrance == "up") {
            return new Vector3(1f, 10f, this.transform.position.z);
        }
        if (SceneManager.GetActiveScene().name == "Room 4" && RoomStatic.entrance == "down") {
            return new Vector3(1f, -9f, this.transform.position.z);
        }
        if (SceneManager.GetActiveScene().name == "Room 4" && RoomStatic.entrance == "right") {
            return new Vector3(10f, 0f, this.transform.position.z);
        }
        if (SceneManager.GetActiveScene().name == "Room 5" && RoomStatic.entrance == "left") {
            return new Vector3(-10f, 0f, this.transform.position.z);
        }
        if (SceneManager.GetActiveScene().name == "Room 6" && RoomStatic.entrance == "down") {
            return new Vector3(0f, -9f, this.transform.position.z);
        }
        if (SceneManager.GetActiveScene().name == "Room 6" && RoomStatic.entrance == "up") {
            return new Vector3(0f, 10f, this.transform.position.z);
        }
        if (SceneManager.GetActiveScene().name == "Room 7" && RoomStatic.entrance == "down") {
            return new Vector3(0f, -9f, this.transform.position.z);
        }
        if (SceneManager.GetActiveScene().name == "Room 7" && RoomStatic.entrance == "up") {
            return new Vector3(0f, 9f, this.transform.position.z);
        }
        if (SceneManager.GetActiveScene().name == "Room 8" && RoomStatic.entrance == "down") {
            return new Vector3(0f, -9f, this.transform.position.z);
        }
        if (SceneManager.GetActiveScene().name == "Room 9" && RoomStatic.entrance == "down") {
            return new Vector3(0f, -9f, this.transform.position.z);
        }

        return new Vector3(0, 0, -0.02f); // failsafe
    }

    void CreateDialogue() {
        NPCScript npcs = (NPCScript) NPC.GetComponent(typeof(NPCScript));
        string[][] lines = npcs.GetLines();
        endDialogue = false;
        Image image = GameObject.Find("DialogueBox").GetComponent<Image>();
        movement = false;
        dx = 0;
        dy = 0;
        var tempColor = image.color;
        tempColor.a = 1f;
        image.color = tempColor;
        if (npcs.hasName) {
            image = GameObject.Find("NamePlate").GetComponent<Image>();
            tempColor = image.color;
            tempColor.a = 1f;
            image.color = tempColor;
            GameObject.Find("NamePlate").transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ((NPCScript) NPC.GetComponent(typeof(NPCScript))).GetName();
        }
        TextRender renderer = (TextRender) GameObject.Find("DialogueBox").GetComponent(typeof(TextRender));
        renderer.StartDialogue(lines);
    }

    public void CreateCustomDialogue(string[][] lines, string name) {
        endDialogue = false;
        Image image = GameObject.Find("DialogueBox").GetComponent<Image>();
        movement = false;
        dx = 0;
        dy = 0;
        var tempColor = image.color;
        tempColor.a = 1f;
        image.color = tempColor;
        if (name != "") {
            image = GameObject.Find("NamePlate").GetComponent<Image>();
            tempColor = image.color;
            tempColor.a = 1f;
            image.color = tempColor;
            GameObject.Find("NamePlate").transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = name;
        }
        TextRender renderer = (TextRender) GameObject.Find("DialogueBox").GetComponent(typeof(TextRender));
        renderer.StartDialogue(lines);
    }

    IEnumerator WaitAfterThunk() {
        string[][] lines = new string[][] {
            new string[] {"*thunk*"}
        };
        CreateCustomDialogue(lines, "");
        yield return new WaitForSeconds(7);
    }

    IEnumerator WaitAfterDrink() {
        movement = false;
        yield return new WaitForSeconds(3);
        BlackoutScript bs = (BlackoutScript) GameObject.Find("Blackout").GetComponent(typeof(BlackoutScript));
        if (!BlackoutScript.fadeToBlack && !BlackoutScript.fadeFromBlack) {
            bs.FadeFromBlack(0.008f);
        }
        string[][] lines = new string[][] {
            new string[] {"What just happened?"}
        };
        CreateCustomDialogue(lines, "");
    }

    IEnumerator StartEndingCutscene() {
        yield return new WaitForSeconds(3);
        InventoryScript ins = (InventoryScript) GameObject.Find("InventoryBar").GetComponent(typeof(InventoryScript));
        GameObject.Find("Player").GetComponent<SpriteRenderer>().sortingOrder = 10000;
        GameObject.Find("InventoryBar").GetComponent<SpriteRenderer>().sortingOrder = 9998;
        GameObject.Find("Canvas").GetComponent<Canvas>().sortingOrder = 10002;
        GameObject.Find("Blackout Canvas").GetComponent<Canvas>().sortingOrder = 9997;
        GameObject.Find("Player").transform.position = new Vector3(0, 0, transform.position.z);
        if (ins.Contains("Formless Object #1") && ins.Contains("Formless Object #2") && ins.Contains("Formless Object #3")) {
            ins.Clear();
            ins.ResetSelection();
            ins.AddInventory("Knife");
            goodEnding = true;
        } else {
            ins.Clear();
        }
        endingCutscene++;
    }
    IEnumerator GoodEndCutscene() {
        yield return new WaitForSeconds(2);
        endingCutscene++;
        TextRender.textSpeed = 0.5f;
        TextRender.tick = 1;
        string[][] lines = new string[][] {
            new string[] {"$Well played."}
        };
        CreateCustomDialogue(lines, "");
    }
}
