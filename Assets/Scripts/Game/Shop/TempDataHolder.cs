using UnityEngine;

public static class TempDataHolder
{
    public static int CurrentCurrency {  get; set; }
    public static int CurrentLife {  get; set; }
    public static int MaxLife {  get { return 5; } }
    public static bool HasHelmet { get; set; }
    public static bool HasRam { get; set; }
    public static bool HasNet { get; set; }


    public static void Init()
    {
        CurrentCurrency = 5000;
        CurrentLife = 3;

        HasHelmet = true;
        HasRam = false;
        HasNet = false;
    }




}
