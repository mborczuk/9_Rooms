using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationScript : MonoBehaviour
{
    string[][] combinations;
    // Start is called before the first frame update
    void Start()
    {
        combinations = new string[][] {
            new string[] {"Mysterious Liquid", "Mug With Hot Coffee", "Spiked Coffee"}, 
            new string[] {"Rug", "Scissors", "Rug Piece"}, 
            new string[] {"Rug Piece", "Bucket of Goo", "Dangerous Rag"}, 
            new string[] {"Alabaster Orb", "Silver Paint", "Silver Orb"},
            new string[] {"Mug With Water", "Gold Collar", "Gold Water"},
            new string[] {"Gold Water", "Moldy Flesh", "Mold Gold Water"},
            new string[] {"Mold Gold Water", "Mustache Hair", "Mold Gold Hair Water"},
            new string[] {"Mold Gold Hair Water", "Sharp Stone", "Phasing Potion"},
            new string[] {"Mold Gold Hair Water", "Bloody Stone", "Potion"},
            new string[] {"Nightstand Wood", "Hammer and Nails", "Ladder"},
            new string[] {"Desk Chair Wood", "Hammer and Nails", "Ladder"},
            new string[] {"Table Chair Wood", "Hammer and Nails", "Ladder"},
            new string[] {"Gold Water", "Sharp Stone", "Stone Gold Water"},
            new string[] {"Stone Gold Water", "Bucket of Goo", "Stone Gold Goo Water"},
            new string[] {"Stone Gold Goo Water", "Mysterious Liquid (Half Full)", "Sleeping Potion"},
        };
    }

    public string[][] CombineItems(string firstItem, string secondItem) {
        if (firstItem == secondItem) {
            return new string[][] {
                new string[] {"$I can't combine an item with itself, idiot."}
            };
        }
        foreach (string[] recipe in combinations) {
            if ((recipe[0] == firstItem && recipe[1] == secondItem) || (recipe[0] == secondItem && recipe[1] == firstItem)) {
                InventoryScript ins = (InventoryScript) GameObject.Find("InventoryBar").GetComponent(typeof(InventoryScript));
                if (firstItem.Contains("Wood") && secondItem == "Hammer and Nails" || secondItem.Contains("Wood") && firstItem == "Hammer and Nails") {
                    if (ins.Contains("Desk Chair Wood") && ins.Contains("Table Chair Wood") && ins.Contains("Nightstand Wood")) {
                        ins.RemoveInventory("Desk Chair Wood");
                        ins.RemoveInventory("Table Chair Wood");
                        ins.RemoveInventory("Nightstand Wood");
                        ins.RemoveInventory("Hammer and Nails");
                        ins.AddInventory("Ladder");
                        return new string[][] {
                            new string[] {"I've used the wood and nails to build a ladder."}
                        };
                    } else {
                        return new string[][] {
                            new string[] {"With more wood, I could build a ladder."}
                        };
                    }
                }
                ins.RemoveInventory(firstItem);
                ins.RemoveInventory(secondItem);
                ins.AddInventory(recipe[2]);
                if (firstItem == "Mysterious Liquid" || secondItem == "Mysterious Liquid") {
                    ins.AddInventory("Mysterious Liquid (Half Full)");
                }
                if (firstItem == "Moldy Flesh" || secondItem == "Moldy Flesh") {
                    ins.AddInventory("Moldy Flesh");
                }
                if (firstItem == "Mustache Hair" || secondItem == "Mustache Hair") {
                    ins.AddInventory("Mustache Hair");
                }
                if (firstItem == "Scissors" || secondItem == "Scissors") {
                    ins.AddInventory("Scissors");
                }
                if (firstItem == "Gold Collar" || secondItem == "Gold Collar") {
                    ins.AddInventory("Gold Collar");
                }
                if (firstItem == "Sharp Stone" || secondItem == "Sharp Stone") {
                    ins.AddInventory("Sharp Stone");
                }
                if (firstItem == "Bloody Stone" || secondItem == "Bloody Stone") {
                    ins.AddInventory("Bloody Stone");
                }
                if (firstItem == "Bucket of Goo" || secondItem == "Bucket of Goo") {
                    ins.AddInventory("Bucket of Goo");
                }
                return new string[][] {
                    new string[] {"I've combined the " + firstItem + " and the " + secondItem + " into the " + recipe[2] + "."}
                };
            }
        }
        return new string[][] {
            new string[] {"I don't know how to combine these items."}
        };
    }
    // Update is called once per frame
    void Update()
    {
        
    }


}
