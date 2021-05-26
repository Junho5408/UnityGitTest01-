using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moneyManager : MonoBehaviour
{
    static int money = 0;
    public GUISkin mySkin;

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
        //Make a background box
        //GUI.Box(new Rect(10, 10, 202, 118), " Money : " + money.ToString());
        //GUILayout.Label("Money : " + money.ToString());

        // Assign the skin to be the one currently used.
        GUI.skin = mySkin;

        //Set the GUIStyle style to be label
        GUIStyle style = GUI.skin.GetStyle("label");
        //Set the style font size to increase and decrease over time
        style.fontSize = (int)(50.0f);

        //Create a label and display with the current settings
        GUI.Label(new Rect(10, 10, 500, 200), "Money : "+money.ToString());

        // Make a button. This will get the default "button" style from the skin assigned to mySkin.
        //GUI.Button(new Rect(10, 10, 150, 20), "Skinned Button");
    }
}
