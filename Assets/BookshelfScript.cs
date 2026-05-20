using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookshelfScript : NPCScript
{
    static bool bookTaken = false;

    void Start()
    {
        hasName = false;
    }

    public override string GetName() {
        return this.name;
    }

    public override string[][] GetLines() {
        string[][] lines;
        if (GetName() == "OrbBookshelf") {
            if (!PlayerScript.goldOrb) {
                lines = new string[][] {
                    new string[] {"The Gold Orb is sitting on top of this bookshelf, but I can't reach it."},
                    new string[] {"The books on this shelf are pretty boring."}
                };
            } else {
                lines = new string[][] {
                    new string[] {"The books on this shelf are pretty boring."}
                };
            }

        } else if (GetName() == "NicknameBookshelf") {
            lines = new string[][] {
                new string[] {"There's a book here that lists US states and their state nicknames."},
                new string[] {"It's sorted by population size."},
                new string[] {"Should I read it?"},
                new string[] {"/choice/"},
                new string[] {"Okay, let's take a look.", "/end/"},
                new string[] {"CALIFORNIA\nPopulation: 40,223,504\nNickname: The Golden State"},
                new string[] {"TEXAS\nPopulation: 30,345,487\nNickname: The Lone Star State"},
                new string[] {"FLORIDA\nPopulation: 22,359,251\nNickname: The Sunshine State"},
                new string[] {"NEW YORK\nPopulation: 20,448,194\nNickname: The Empire State"},
                new string[] {"PENNSYLVANIA\nPopulation: 13,092,796\nNickname: The Keystone State"},
                new string[] {"ILLINOIS\nPopulation: 12,807,072\nNickname: The Prairie State"},
                new string[] {"OHIO\nPopulation: 11,878,330\nNickname: The Buckeye State"},
                new string[] {"GEORGIA\nPopulation: 11,019,186\nNickname: The Peach State"},
                new string[] {"NORTH CAROLINA\nPopulation: 10,710,558\nNickname: The Tar Heel State"},
                new string[] {"MICHIGAN\nPopulation: 10,135,438\nNickname: The Great Lakes State"},
                new string[] {"..."},
                new string[] {"There's a note tucked inside a page:"},
                new string[] {"\"The key is not 'state' or 'the'.\""},
            };
        } else if (GetName() == "FrenchBookshelf") {
            if (!bookTaken) {
                lines = new string[][] {
                    new string[] {"This bookshelf has books that are all in different languages."},
                    new string[] {"This one is in French. I can only understand one word from the title - 'Potions'."},
                    new string[] {"It seems interesting enough. I'll take it with me. Maybe I can find someone to translate it."}
                };
                InventoryScript ins = (InventoryScript) GameObject.Find("InventoryBar").GetComponent(typeof(InventoryScript));
                ins.AddInventory("Livre De Potions");
                bookTaken = true;
            } else {
                lines = new string[][] {
                    new string[] {"This bookshelf has books that are all in different languages."},
                    new string[] {"I don't want to read any of them."},
                };
            }

        }
        else {
            lines = new string[][] {
                new string[] {"None of the books on this shelf seem interesting."}
            };
        }
        return lines;
    }

    public override void ChoiceMaker(int choice) {}
}
