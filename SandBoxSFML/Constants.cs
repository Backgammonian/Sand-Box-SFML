namespace SandBoxSFML
{
    public static class Constants
    {
        static Constants()
        {
            Gravity = 1;

            MaxRadius = 100;

            WaterSpreadRate = 10;
            OilSpreadRate = 5;
            AcidSpreadRate = 8;
            LavaSpreadRate = 1;
            FireSpreadChance = 3;
            SteamSpreadSpeed = 1.3f;
            SmokeSpreadSpeed = 1.1f;

            FireLifeTime = 100;
            SteamLifeTime = 100;
            SmokeLifeTime = 160;
            EmberLifeTime = 200;

            SmokeSpawnChance = 20;
            EmberSpawnChance = 50;

            WoodIgnitionChance = 100;
            CoalIgnitionChance = 20;
            OilIgnitionChance = 3;

            SteamRegionWidth = 3;
            SteamRegionHeight = 3;
            SmokeRegionWidth = 5;
            SmokeRegionHeight = 4;

            AcidMeltsStoneChance = 60;
            AcidMeltsWoodChance = 30;
            AcidMeltsSandChance = 20;
            AcidMeltsPlantChance = 10;
            AcidMeltsAshChance = 10;
            AcidDissolvesInWaterChance = 40;
    }

        public static int Gravity { get; }

        public static int MaxRadius { get; }

        public static int WaterSpreadRate { get; }
        public static int OilSpreadRate { get; }
        public static int AcidSpreadRate { get; }
        public static int LavaSpreadRate { get; }
        public static int FireSpreadChance { get; }
        public static float SteamSpreadSpeed { get; }
        public static float SmokeSpreadSpeed { get; }

        public static int FireLifeTime { get; }
        public static int SteamLifeTime { get; }
        public static int SmokeLifeTime { get; }
        public static int EmberLifeTime { get; }

        public static int SmokeSpawnChance { get; }
        public static int EmberSpawnChance { get; }

        public static int WoodIgnitionChance { get; }
        public static int CoalIgnitionChance { get; }
        public static int OilIgnitionChance { get; }

        public static int SteamRegionWidth { get; }
        public static int SteamRegionHeight { get; }
        public static int SmokeRegionWidth { get; }
        public static int SmokeRegionHeight { get; }

        public static int AcidMeltsStoneChance { get; }
        public static int AcidMeltsWoodChance { get; }
        public static int AcidMeltsSandChance { get; }
        public static int AcidMeltsPlantChance { get; }
        public static int AcidMeltsAshChance { get; }
        public static int AcidDissolvesInWaterChance { get; }
    }
}
