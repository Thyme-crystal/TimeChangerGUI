using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using BepInEx;
using GorillaLocomotion;
using GorillaNetworking;
using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UIElements;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using static System.TimeZoneInfo;

[Description("change time")]
[BepInPlugin("pilo.thyme.gorillatag.TimeGUI", "pilo.thyme.gorillatag.TimeGUI", "1.0.0")]
public class Plugin : BaseUnityPlugin
{
    private Texture2D bgTex;
    private GUIStyle boxStyle;
    private Texture2D buttonTex;
    private GUIStyle buttonStyle;

    public Plugin()
    {
        new Harmony("thyme.TimeGUI").PatchAll(Assembly.GetExecutingAssembly());

    }

    public void Update()
    {

        if (UnityInput.Current.GetKeyDown(OpenAndCloseGUI))
        {
            close = !close;
        }
    }

    public void Awake()
    {
        bgTex = new Texture2D(1, 1);
        bgTex.SetPixel(0, 0, new Color(0.2f, 0.2f, 0.2f, 0.8f));
        bgTex.Apply();
    }


    public void OnGUI()
    {
        if (boxStyle == null)
        {
            boxStyle = new GUIStyle(GUI.skin.box);
            boxStyle.fontSize = 18;
            boxStyle.alignment = TextAnchor.MiddleCenter;
            boxStyle.normal.textColor = Color.black;
            boxStyle.normal.background = bgTex;
        }

        if (buttonStyle == null)
        {
            buttonStyle = new GUIStyle(GUI.skin.box);
            buttonStyle.fontSize = 16;
            buttonStyle.alignment = TextAnchor.MiddleCenter;
            buttonStyle.normal.textColor = new Color(178 / 255f, 154 / 255f, 255 / 255f);
            buttonStyle.normal.background = bgTex;
        }

        if (!close)
        {
            GUILayout.BeginHorizontal(boxStyle);
            GUILayout.Box(Text);
            if (GUILayout.Button("Night", buttonStyle))
            {
                SetTime(0);
            }
            if (GUILayout.Button("Evening", buttonStyle))
            {
                SetTime(7);
            }
            if (GUILayout.Button("Morning", buttonStyle))
            {
                SetTime(1);
            }
            if (GUILayout.Button("Day", buttonStyle))
            {
                SetTime(3);
            }

            GUILayout.Label("| |");

            if (GUILayout.Button("Rain", buttonStyle)) 
            {
                Rain(true);
            }

            if (GUILayout.Button("Stop", buttonStyle))
            {
                Rain(false);
            }

            GUI.Label(new Rect(Screen.width - 100, Screen.height - 20, 100, 20), "thyme", buttonStyle);

            GUILayout.EndHorizontal();
        }
    }

    public static void SetTime(int time)
    {
        BetterDayNightManager.instance.SetTimeOfDay(time);
    }

    public static void Rain(bool rain)
    {
        if (rain)
        {
            BetterDayNightManager.instance.SetFixedWeather(BetterDayNightManager.WeatherType.Raining);
        }

        else
        {
            BetterDayNightManager.instance.SetFixedWeather(BetterDayNightManager.WeatherType.None);
        }
    }

    public static string Text = "Time Changer";
    public static bool close = true;
    public static KeyCode OpenAndCloseGUI = KeyCode.B;

}