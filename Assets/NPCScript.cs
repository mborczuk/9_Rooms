using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPCScript : MonoBehaviour
{
    public bool canChoose = false;
    public bool hasName = true;
    public abstract string GetName();
    public abstract string[][] GetLines();
    public abstract void ChoiceMaker(int choice);
}
