using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BonemasterScript : NPCScript
{    
    bool isGaming = false;
    int turn = 0;
    static int bones = 23;
    public static bool bonesShown = false;
    static bool talkedAfterBonesShown = false;
    int bonesRemaining = 25;
    float lastTime = 0;
    static int percent = 50;
    int[] goodNums = new int[] {5, 9, 13, 17, 21};
    static bool formlessGiven = false;
    static bool firstGame = true;
    // Start is called before the first frame update
    void Start()
    {
        canChoose = true;
    }

    public override string GetName() {
        return "Bonemaster";
    }

    public override string[][] GetLines() {
        string[][] lines;
        if (formlessGiven) {
            canChoose = false;
            lines = new string[][] {
                new string[] {"I have failed..."},
            };
        }
        else if (bones == 1) {
            canChoose = false;
            formlessGiven = true;
            lines = new string[][] {
                new string[] {"..."},
                new string[] {"I have only one bone left."},
                new string[] {"I cannot give it up."},
                new string[] {"Take this instead...I know not what it is, but it may help you out."},
            };
            InventoryScript ins = (InventoryScript) GameObject.Find("InventoryBar").GetComponent(typeof(InventoryScript));
            ins.AddInventory("Formless Object #1");
        }
        else if (bonesShown) {
            talkedAfterBonesShown = true;
            canChoose = true;
            lines = new string[][] {
                new string[] {"Ah, it's you. You want to play my game?"},
                new string[] {"/choice/"},
                new string[] {"Good. The rules are simple.", "/end/"},
                new string[] {"There are 25 bones in the pile. We will take turns taking 1, 2, or 3 bones from the pile. Whoever takes the last bone is the winner.", ""},
                new string[] {"As you are the challenger, you will go first. If you win, you will get half of my bones. There will be no penalty if you lose.", ""},
                new string[] {"Let us begin.", ""}
            };
        } else {
            lines = new string[][] {
                new string[] {"I am the Bonemaster."},
                new string[] {"If you have any bones, show them to me."}
            };
        }
        return lines;
    }

    public override void ChoiceMaker(int choice) {
        if (choice == 0 && talkedAfterBonesShown) {
            BoneGame();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isGaming) {
            if (turn % 2 == 1) {
                GameObject.Find("1").GetComponent<Button>().interactable = false;
                GameObject.Find("2").GetComponent<Button>().interactable = false;
                GameObject.Find("3").GetComponent<Button>().interactable = false;
                if (Time.time - lastTime > 1.2) {
                    int value = 0;
                    int bonesTaken = 25 - bonesRemaining;
                    int rand = Random.Range(0, 100);
                    if (rand < percent) {
                        foreach (int num in goodNums) {
                            if (num - bonesTaken <= 3 && num - bonesTaken > 0) {
                                value = num - bonesTaken;
                            }
                        }
                        if (value == 0) {
                            value = Random.Range(1, 4);
                        }
                    } else {
                        value = Random.Range(1, 4);
                    }  
                    if (bonesRemaining == 3) {
                        value = 3;
                    }
                    if (bonesRemaining == 2) {
                        value = 2;
                    }
                    if (bonesRemaining == 1) {
                        value = 1;
                    }

                    bonesRemaining -= value;
                    GameObject.Find("GameText").GetComponent<TextMeshProUGUI>().text = "Remaining Bones: " + bonesRemaining;
                    if (bonesRemaining <= 0) {
                        EndGame();
                    } else {
                        turn++;
                    }
                }
            } else {
                if (bonesRemaining >= 3) {
                    GameObject.Find("3").GetComponent<Button>().interactable = true;
                }
                if (bonesRemaining >= 2) {
                     GameObject.Find("2").GetComponent<Button>().interactable = true;
                }
                if (bonesRemaining >= 1) {
                     GameObject.Find("1").GetComponent<Button>().interactable = true;
                }               
            }
        }
    }

    void BoneGame() {
        PlayerScript.movement = false;
        turn = 0;
        bonesRemaining = 25;
        lastTime = 0;
        Debug.Log(bonesRemaining);
        Debug.Log(turn);
        ChangeGameVisibility(1f);
        GameObject.Find("GameText").GetComponent<TextMeshProUGUI>().text = "Remaining Bones: " + bonesRemaining;
        isGaming = true;
    }

    public void TakePlayerTurn(int value) {
        if (turn % 2 == 0) {
            bonesRemaining -= value;
            GameObject.Find("GameText").GetComponent<TextMeshProUGUI>().text = "Remaining Bones: " + bonesRemaining;
            if (bonesRemaining <= 0) {
                EndGame();
            } else {
                turn++;
            }
            lastTime = Time.time;
        }
    }

    void ChangeGameVisibility(float alpha) {
        Image i = GameObject.Find("GamePanel").GetComponent<Image>();
        i.color = new Color(i.color.r, i.color.g, i.color.b, alpha);
        TextMeshProUGUI tmp = GameObject.Find("GameText").GetComponent<TextMeshProUGUI>();
        tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, alpha);
        i = GameObject.Find("1").GetComponent<Image>();
        i.color = new Color(i.color.r, i.color.g, i.color.b, alpha);
        i = GameObject.Find("2").GetComponent<Image>();
        i.color = new Color(i.color.r, i.color.g, i.color.b, alpha);
        i = GameObject.Find("3").GetComponent<Image>();
        i.color = new Color(i.color.r, i.color.g, i.color.b, alpha);
        tmp = GameObject.Find("ButtonText1").GetComponent<TextMeshProUGUI>();
        tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, alpha);
        tmp = GameObject.Find("ButtonText2").GetComponent<TextMeshProUGUI>();
        tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, alpha);
        tmp = GameObject.Find("ButtonText3").GetComponent<TextMeshProUGUI>();
        tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, alpha);
    }

    void EndGame() {
        PlayerScript ps = (PlayerScript) GameObject.Find("Player").GetComponent(typeof(PlayerScript));
        PlayerScript.movement = true;
        isGaming = false;
        string[][] lines;
        if (firstGame) {
            firstGame = false;
            canChoose = false;
            lines = new string[][] {
                new string[] {"That was a practice round."},
                new string[] {"Please take this as thanks for playing with me."},
                new string[] {"Talk to me again if you want to play again."}
            };
            InventoryScript ins = (InventoryScript) GameObject.Find("InventoryBar").GetComponent(typeof(InventoryScript));
            ins.AddInventory("Scissors");
            ps.CreateCustomDialogue(lines, "Bonemaster");
        } else {
            if (turn % 2 == 1) {
                canChoose = false;
                lines = new string[][] {
                    new string[] {"You have lost."},
                    new string[] {"If you want to play again, talk to me again."}
                };
                ps.CreateCustomDialogue(lines, "Bonemaster");
            } else {
                canChoose = false;
                lines = new string[][] {
                    new string[] {"You have won."},
                    new string[] {"I currently have " + bones + " bones. I will give you " + bones / 2 + " of them."}
                };
                PlayerScript.bones += bones / 2;
                bones -= bones / 2;
                ps.CreateCustomDialogue(lines, "Bonemaster");
                percent += 13;
            }
        }
        
        bonesRemaining = 25;
        lastTime = 0;
        turn = 0;
        ChangeGameVisibility(0f);
    }
}
