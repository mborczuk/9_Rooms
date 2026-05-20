using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : NPCScript
{
    public static ArrayList openDoors = new ArrayList();
    public static bool platinumDoorOpen = false;
    string doorName; 

    void Start() {
        canChoose = true;
        doorName = SceneManager.GetActiveScene().name + this.GetComponent<SpriteRenderer>().sprite.name;
        hasName = false;
        if (platinumDoorOpen) {
            Destroy(GameObject.Find("PlatinumDoor"));
        }
        if (openDoors.Contains(doorName)) {
            OpenDoor();
        }
    }

    void Update() {

    }
    public override string GetName() {
        return this.GetComponent<SpriteRenderer>().sprite.name;
    }

    public override string[][] GetLines() {
        string[][] lines = new string[][] {
                new string[] {"This is a failsafe message. This message should not appear."}
        };
        if (this.GetName() == "BronzeDoor") {
            lines = new string[][] {
                new string[] {"It's locked. I can't open it."},
                new string[] {"I need the Bronze Orb to open this door."}
            };
        }
        if (this.GetName() == "GoldDoor") {
            lines = new string[][] {
                new string[] {"It's locked. I can't open it."},
                new string[] {"I need the Gold Orb to open this door."}
            };
        }
        if (this.GetName() == "SilverDoor") {
            lines = new string[][] {
                new string[] {"It's locked. I can't open it."},
                new string[] {"I need the Silver Orb to open this door."}
            };
        }
        if (this.GetName() == "PlatinumDoor") {
            lines = new string[][] {
                new string[] {"It's locked. I can't open it."},
                new string[] {"I need the Alabaster Orb to open this door."}
            };
        }
        if (this.doorName == "Room 1PasswordDoor") {
            lines = new string[][] {
                new string[] {"It's locked. I can't open it."},
                new string[] {"It looks like I need a key card to open this door."}
            };
        }
        if (this.doorName == "Room 2PasswordDoor") {
            lines = new string[][] {
                new string[] {"ENTER NUMERIC PASSCODE"},
                new string[] {"/npasscode/"},
                new string[] {"PASSCODE ACCEPTED. PLEASE PROCEED.", "PASSCODE DENIED. PLEASE TRY AGAIN."}
            };
        }
        if (this.doorName == "Room 7PasswordDoor") {
            lines = new string[][] {
                new string[] {"ENTER PASSCODE"},
                new string[] {"/passcode/"},
                new string[] {"PASSCODE ACCEPTED. PLEASE PROCEED.", "PASSCODE DENIED. PLEASE TRY AGAIN."}
            };
        }

        return lines;
    }
    public override void ChoiceMaker(int choice) {
        if (this.doorName == "Room 2PasswordDoor" && choice == 0) {
            OpenDoor();
        }
        if (this.doorName == "Room 7PasswordDoor" && choice == 0) {
            OpenDoor();
        }
    }

    public void OpenDoor() {
        if (!openDoors.Contains(doorName)) {
            openDoors.Add(doorName);
        }
        SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
        sr.sprite = Resources.Load<Sprite>(sr.sprite.name + "Open");
        this.GetComponent<BoxCollider2D>().enabled = false;
        this.tag = "Untagged";
    }
}
