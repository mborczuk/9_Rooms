using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackoutScript : MonoBehaviour
{
    float speed = 0;
    float oppSpeed = 0;
    public static bool fadeToBlack = false;
    public static bool fadeFromBlack = false;
    bool doOpposite = false;

    void Update() {
        if (fadeToBlack) {
            Color c = this.GetComponent<Image>().color;
            this.GetComponent<Image>().color = new Color(c.r, c.g, c.b, c.a + speed);
            if (c.a >= 1) {
                fadeToBlack = false;
                if (doOpposite) {
                    FadeFromBlack(oppSpeed);
                }
            }
        }
        if (fadeFromBlack) {
            int min = 0;
            Color c = this.GetComponent<Image>().color;
            this.GetComponent<Image>().color = new Color(c.r, c.g, c.b, c.a - speed);
            if (c.a <= min) {
                this.GetComponent<Image>().color = new Color(c.r, c.g, c.b, min);
                fadeFromBlack = false;
            }
        }
    }
    public void FadeToBlack(float speed, bool opposite, float oppSpeed) {
        PlayerScript.movement = false;
        BlackoutScript.fadeToBlack = true;
        this.speed = speed;
        doOpposite = opposite;
        if (doOpposite) {
            this.oppSpeed = oppSpeed;
        }
    }
    public void FadeFromBlack(float speed) {
        PlayerScript.movement = false;
        BlackoutScript.fadeFromBlack = true;
        this.speed = speed;
    }
    public void SetColor(float r, float g, float b) {
        this.GetComponent<Image>().color = new Color(r, g, b, this.GetComponent<Image>().color.a);
    }
    
}
