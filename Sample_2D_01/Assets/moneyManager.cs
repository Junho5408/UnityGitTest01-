using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moneyManager : MonoBehaviour
{
    static int money = 0;
    public GUISkin mySkin;
    static int stageLevel = 0;

    public static void setMoney(int value)
    {
        money += value;
    }

    // Update is called once per frame
    public static int getMoney()
    {
        return money;
    }

    void OnGUI()
    {

        GUI.skin = mySkin;

        //Set the GUIStyle style to be label
        GUIStyle style = GUI.skin.GetStyle("label");
        style.fontSize = (int)(50.0f);

        //Create a label and display with the current settings
        GUI.Label(new Rect(10, 10, 500, 200), "Money : "+money.ToString());


        style.fontSize = (int)(50.0f);
        GUI.Label(new Rect(10, 60, 500, 200), "STAGE " + stageLevel.ToString());

    }
}
