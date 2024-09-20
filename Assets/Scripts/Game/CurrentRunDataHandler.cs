namespace Game
{
    public static class CurrentRunDataHandler
    {
        public static int CurrentCurrency {  get; set; }
        public static int CurrentLife {  get; set; }
        public static int MaxLife => 4;
        public static bool HasHelmet { get; set; }
        public static bool HasRam { get; set; }
        public static bool HasNet { get; set; }
        
        
        public static bool HasInitialized { get; private set; }
        
        private const int _initialCurrency = 0;
        private const int _initialLife = 4;


        public static void Init()
        {
            CurrentCurrency = _initialCurrency;
            CurrentLife = _initialLife;

            HasHelmet = false;
            HasRam = false;
            HasNet = false;
            
            HasInitialized = true;
        }

        public static void Reset()
        {
            HasInitialized = false;
        }
    }
}
