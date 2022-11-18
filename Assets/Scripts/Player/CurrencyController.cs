using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurrencyType
{
    RedSouls,
    BlueSouls,
    YellowSouls,
    Eyece,
    FingerBurn
}

public class CurrencyController : MonoBehaviour
{
    private static int redSouls = 0;
    private static int blueSouls = 0;
    private static int yellowSouls = 0;
    private static int eyece = 0;
    private static int fingerBurn = 0;

    public static int RedSouls
    {
        get { return redSouls; }
        set { redSouls = value; }
    }

    public static int BlueSouls
    {
        get { return blueSouls; }
        set { blueSouls = value; }
    }

    public static int YellowSouls
    {
        get { return yellowSouls; }
        set { yellowSouls = value; }
    }

    public static int Eyece
    {
        get { return eyece; }
        set { eyece = value; }
    }

    public static int FingerBurn
    {
        get { return fingerBurn; }
        set { fingerBurn = value; }
    }

    public void ResetCurrency()
    {
        redSouls = 0;
        blueSouls = 0;
        yellowSouls = 0;
        eyece = 0;
        fingerBurn = 0;
    }

}
