using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using static System.Console;

public class TextRender : MonoBehaviour
{
    public TextMeshProUGUI textbox;
    public string[][] lines;
    public static float textSpeed = 0.02f;
    public static int tick = 5;
    private int previousChoiceIndex;
    private int choiceIndex;
    private int index;
    private bool box = false;
    private float time = 0;
    private bool passcode = false;
    private bool npasscode = false;
    public static bool gotName = false;
    // Start is called before the first frame update
    private KeyCode[] numericKeyCodes = {
         KeyCode.Alpha0,
         KeyCode.Alpha1,
         KeyCode.Alpha2,
         KeyCode.Alpha3,
         KeyCode.Alpha4,
         KeyCode.Alpha5,
         KeyCode.Alpha6,
         KeyCode.Alpha7,
         KeyCode.Alpha8,
         KeyCode.Alpha9,
     };

    private KeyCode[] keyCodes = {
         KeyCode.A,
         KeyCode.B,
         KeyCode.C,
         KeyCode.D,
         KeyCode.E,
         KeyCode.F,
         KeyCode.G,
         KeyCode.H,
         KeyCode.I,
         KeyCode.J,
         KeyCode.K,
         KeyCode.L,
         KeyCode.M,
         KeyCode.N,
         KeyCode.O,
         KeyCode.P,
         KeyCode.Q,
         KeyCode.R,
         KeyCode.S,
         KeyCode.T,
         KeyCode.U,
         KeyCode.V,
         KeyCode.W,
         KeyCode.X,
         KeyCode.Y,
         KeyCode.Z
     };
 
    void Start()
    {
        textbox.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && this.GetComponent<Image>().color.a == 1f) {
            GameObject passcodeBox = GameObject.Find("PasscodeInput");
            TextMeshProUGUI ptextbox = passcodeBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            if (lines[index][previousChoiceIndex] == "/choice/") {
                index++;
                if (lines[index][choiceIndex] == "/end/") {
                    textbox.text = "";
                    textbox.color = new Color(1f, 1f, 1f, 1f);
                    Image image = this.GetComponent<Image>();
                    var tempColor = image.color;
                    tempColor.a = 0f;
                    image.color = tempColor;
                    GameObject nameplate = GameObject.Find("NamePlate");
                    nameplate.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
                    image = nameplate.GetComponent<Image>();
                    tempColor = image.color;
                    tempColor.a = 0f;
                    image.color = tempColor;
                    PlayerScript.movement = true;
                    PlayerScript.time = Time.time;
                    PlayerScript.endDialogue = true;
                    if (PlayerScript.NPC != null) {
                        NPCScript npcs = (NPCScript) PlayerScript.NPC.GetComponent(typeof(NPCScript));
                        if (npcs.canChoose) {
                            npcs.ChoiceMaker(choiceIndex);
                        }
                    }
                }
                else {
                    textbox.text = "";
                    StartCoroutine(TypeLine());
                }
            } else if (lines[index][previousChoiceIndex] == "/npasscode/") {
                index++;
                if (ptextbox.text == "1915" || ptextbox.text == "0715") {
                    choiceIndex = 0;
                } else {
                    choiceIndex = 1;
                }
                ptextbox.text = "";
                textbox.text = "";
                StartCoroutine(TypeLine());
            } else if (lines[index][previousChoiceIndex] == "/passcode/") {
                index++;
                if (!gotName) {
                    try {
                        PlayerScript.yourName = ptextbox.text.Substring(0, 1) + ptextbox.text.Substring(1, ptextbox.text.Length - 1).ToLower();
                    } catch (ArgumentOutOfRangeException e) {
                        PlayerScript.yourName = "Piccolo";
                    } finally {
                        gotName = true; 
                    }
                } else {
                    if (ptextbox.text == "PINKY") {
                        choiceIndex = 0;
                    } else {
                        choiceIndex = 1;
                    }
                }
                ptextbox.text = "";
                textbox.text = "";
                StartCoroutine(TypeLine());
            }
            else {
                if (textbox.text == lines[index][choiceIndex].Replace("$", "")) {
                    index++;
                    if (index <= lines.Length - 1) {  
                        if (lines[index][choiceIndex] == "/choice/") {
                            SpawnChoiceDialogueBox();
                        } else if (lines[index][choiceIndex] == "/npasscode/") {
                            SpawnPasscodeBox("numeric");
                        } else if (lines[index][choiceIndex] == "/passcode/") {
                            SpawnPasscodeBox("word");
                        }
                        else {
                            textbox.text = "";
                            StartCoroutine(TypeLine());
                        }
                    } else {
                        textbox.text = "";
                        textbox.color = new Color(1f, 1f, 1f, 1f);
                        Image image = this.GetComponent<Image>();
                        var tempColor = image.color;
                        tempColor.a = 0f;
                        image.color = tempColor;
                        GameObject nameplate = GameObject.Find("NamePlate");
                        nameplate.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
                        image = nameplate.GetComponent<Image>();
                        tempColor = image.color;
                        tempColor.a = 0f;
                        image.color = tempColor;
                        PlayerScript.time = Time.time;
                        PlayerScript.endDialogue = true;
                        PlayerScript.movement = true;
                        if (PlayerScript.endingCutscene == 4) {
                            // end game
                            BlackoutScript bs = (BlackoutScript) GameObject.Find("Blackout").GetComponent(typeof(BlackoutScript));
                            Destroy(GameObject.Find("InventoryBar"));
                            bs.SetColor(0, 0, 0);
                            bs.FadeToBlack(1f, false, 0f);
                            Application.Quit();
                            // UnityEditor.EditorApplication.isPlaying = false;
                        }
                        if (PlayerScript.NPC != null && (PlayerScript.NPC.name == "Sacrifice Pedestal" || PlayerScript.NPC.name == "Frenchman")) {
                            PlayerScript.startedBlackout = true;
                            BlackoutScript bs = (BlackoutScript) GameObject.Find("Blackout").GetComponent(typeof(BlackoutScript));
                            bs.FadeFromBlack(0.003f);
                            if (!AudioScript.IsPlaying()) {
                                AudioScript.PlayAudio();
                            }
                        }
                        if (PlayerScript.NPC != null) {
                            NPCScript npcs = (NPCScript) PlayerScript.NPC.GetComponent(typeof(NPCScript));
                            if (npcs.canChoose) {
                                npcs.ChoiceMaker(choiceIndex);
                            }
                        }

                    }
                } else {
                    StopAllCoroutines();
                    textbox.text = lines[index][choiceIndex].Replace("$", "");
                }
            }
            
        }
        if (box) {
            GameObject yesnobox = GameObject.Find("YesNoBox");
            if (choiceIndex == 0) {
                yesnobox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 0f, 1f);
                yesnobox.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, 1f);
            } else {
                yesnobox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, 1f);
                yesnobox.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 0f, 1f);
            }
            if(Input.GetKeyDown("up")) {
                choiceIndex--;
                if (choiceIndex < 0) {
                    choiceIndex = 1;
                }
            }
            if(Input.GetKeyDown("down")) {
                choiceIndex++;
                if (choiceIndex > 1) {
                    choiceIndex = 0;
                }
            }
            if(Input.GetKeyDown(KeyCode.Return) && (Time.time - time) > 0.25) {
                yesnobox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, 0f);
                yesnobox.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, 0f);
                Image image = yesnobox.GetComponent<Image>();
                var tempColor = image.color;
                tempColor.a = 0f;
                image.color = tempColor;
                box = false;
            }
        }
        if (npasscode) {
            GameObject passcodeBox = GameObject.Find("PasscodeInput");
            TextMeshProUGUI ptextbox = passcodeBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            for(int i = 0 ; i < numericKeyCodes.Length; i ++ ){
                if (ptextbox.text.Length < 9) {
                    if(Input.GetKeyDown(numericKeyCodes[i])){
                        int numberPressed = i;
                        ptextbox.text += numberPressed;
                    }
                }

            }
            if (Input.GetKeyDown(KeyCode.Backspace)) {
                ptextbox.text = ptextbox.text.Substring(0, ptextbox.text.Length - 1);
            }
            if(Input.GetKeyDown(KeyCode.Return) && (Time.time - time) > 0.25) {
                passcodeBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, 0f);
                Image image = passcodeBox.GetComponent<Image>();
                var tempColor = image.color;
                tempColor.a = 0f;
                image.color = tempColor;
                npasscode = false;
            }
        }
        if (passcode) {
            GameObject passcodeBox = GameObject.Find("PasscodeInput");
            TextMeshProUGUI ptextbox = passcodeBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            for(int i = 0 ; i < keyCodes.Length; i ++ ){
                if (ptextbox.text.Length < 9) {
                    if(Input.GetKeyDown(keyCodes[i])){
                        int numberPressed = i + 65;
                        ptextbox.text += (char) numberPressed;
                    }
                }

            }
            if (Input.GetKeyDown(KeyCode.Backspace)) {
                ptextbox.text = ptextbox.text.Substring(0, ptextbox.text.Length - 1);
            }
            if(Input.GetKeyDown(KeyCode.Return) && (Time.time - time) > 0.25) {
                passcodeBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, 0f);
                Image image = passcodeBox.GetComponent<Image>();
                var tempColor = image.color;
                tempColor.a = 0f;
                image.color = tempColor;
                passcode = false;
            }
        }
    }

    public void StartDialogue(string[][] lines) {
        index = 0;
        choiceIndex = 0;
        this.lines = lines;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine() {
        int chaI = 0;
        foreach (char c in lines[index][choiceIndex].ToCharArray()) {
            if (chaI % tick == 0) {
                GameObject.Find("TextSound").GetComponent<AudioSource>().Play();
            }
            if (c == '$') {
                textbox.color = new Color32(255, 0, 0, 255);
            } else {
                textbox.text += c;
                chaI++;
            }
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void SpawnChoiceDialogueBox() {
        previousChoiceIndex = choiceIndex;
        GameObject yesnobox = GameObject.Find("YesNoBox");
        Image image = yesnobox.GetComponent<Image>();
        var tempColor = image.color;
        tempColor.a = 1f;
        image.color = tempColor;
        Color color1 = yesnobox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color;
        Color color2 = yesnobox.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color;
        yesnobox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(color1.r, color1.g, color1.b, 1f);
        yesnobox.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = new Color(color2.r, color2.g, color2.b, 1f);
        box = true;
        time = Time.time;
    }

    void SpawnPasscodeBox(string type) {
        previousChoiceIndex = choiceIndex;
        GameObject passcodeBox = GameObject.Find("PasscodeInput");
        Image image = passcodeBox.GetComponent<Image>();
        var tempColor = image.color;
        tempColor.a = 1f;
        image.color = tempColor;
        Color color = passcodeBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color;
        passcodeBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(color.r, color.g, color.b, 1f);
        if (type == "numeric") {
            npasscode = true;
        } else {
            passcode = true;
        }
        time = Time.time;
    }
}
 