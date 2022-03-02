namespace SandBoxSFML
{
    public static class Constants
    {
        static Constants()
        {
            Gravity = 1;
            AshGravity = Gravity / 8.0f;

            MaxRadius = 100;

            WaterSpreadRate = 10;
            OilSpreadRate = 5;
            AcidSpreadRate = 8;
            LavaSpreadRate = 2;
            FireSpreadChance = 4;
            SteamSpreadSpeed = 1.3f;
            SmokeSpreadSpeed = 1.1f;

            FireLifeTime = 100;
            SteamLifeTime = 100;
            SmokeLifeTime = 160;
            EmberLifeTime = 200;
            LavaLifeTime = 1500;

            SmokeSpawnChance = 20;
            EmberSpawnChance = 50;
            LavaSpawnsSmokeChance = 200;
            LavaSpawnsEmberChance = 300;
            SteamCondencesChance = 20;
            SmokeCondencesChance = 100;
            LavaMeltsStoneChance = 70;
            LavaMeltsSandChance = 40;

            WoodIgnitionChance = 100;
            CoalIgnitionChance = 50;
            OilIgnitionChance = 10;
            MethaneIgnitionChance = 1;
            PlantIgnitionChance = 20;

            SteamRegionWidth = 3;
            SteamRegionHeight = 3;
            SmokeRegionWidth = 5;
            SmokeRegionHeight = 4;

            AcidMeltsStoneChance = 60;
            AcidMeltsWoodChance = 30;
            AcidMeltsSandChance = 20;
            AcidMeltsPlantChance = 10;
            AcidMeltsAshChance = 10;
            AcidMeltsObsidianChance = 100;
            AcidDissolvesInWaterChance = 40;
    }

        public static float Gravity { get; }
        public static float AshGravity { get; }

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
        public static int LavaLifeTime { get; }

        public static int SmokeSpawnChance { get; }
        public static int EmberSpawnChance { get; }
        public static int LavaSpawnsSmokeChance { get; }
        public static int LavaSpawnsEmberChance { get; }
        public static int SteamCondencesChance { get; }
        public static int SmokeCondencesChance { get; }
        public static int LavaMeltsStoneChance { get; }
        public static int LavaMeltsSandChance { get; }

        public static int WoodIgnitionChance { get; }
        public static int CoalIgnitionChance { get; }
        public static int OilIgnitionChance { get; }
        public static int MethaneIgnitionChance { get; }
        public static int PlantIgnitionChance { get; }

        public static int SteamRegionWidth { get; }
        public static int SteamRegionHeight { get; }
        public static int SmokeRegionWidth { get; }
        public static int SmokeRegionHeight { get; }

        public static int AcidMeltsStoneChance { get; }
        public static int AcidMeltsWoodChance { get; }
        public static int AcidMeltsSandChance { get; }
        public static int AcidMeltsPlantChance { get; }
        public static int AcidMeltsAshChance { get; }
        public static int AcidMeltsObsidianChance { get; }
        public static int AcidDissolvesInWaterChance { get; }
    }
}
