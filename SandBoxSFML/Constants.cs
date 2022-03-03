namespace SandBoxSFML
{
    public static class Constants
    {
        static Constants()
        {
            Gravity = 1;
            MethaneGravity = -Gravity * 2;
            GasGravity = -Gravity;
            OneEighthOfGravity = Gravity / 8f;

            MaxRadius = 100;

            WaterSpreadRate = 10;
            SandSpreadRate = 2;
            OilSpreadRate = 5;
            AcidSpreadRate = 8;
            LavaSpreadRate = 2;
            MethaneSpreadRate = 5;
            FireSpreadChance = 4;
            BurningGasSpreadChance = 2;
            SteamSpreadSpeed = 1.3f;
            SmokeSpreadSpeed = 1.1f;

            FireLifeTime = 100;
            SteamLifeTime = 100;
            SmokeLifeTime = 160;
            EmberLifeTime = 200;
            LavaLifeTime = 1500;
            BurningGasLifeTime = 120;
            VirusLifeTime = 100;

            SmokeSpawnChance = 20;
            SteamSpawnChance = 25;
            EmberSpawnChance = 75;
            AshSpawnChance = 5;
            LavaSpawnsSmokeChance = 200;
            LavaSpawnsEmberChance = 400;
            SteamCondencesChance = 150;
            SmokeCondencesChance = 250;
            LavaMeltsStoneChance = 70;
            LavaMeltsSandChance = 40;
            LavaMeltsDirtChance = 50;

            WoodIgnitionChance = 50;
            CoalIgnitionChance = 30;
            OilIgnitionChance = 15;
            MethaneIgnitionChance = 1;
            PlantIgnitionChance = 20;

            SteamRegionWidth = 3;
            SteamRegionHeight = 3;
            SmokeRegionWidth = 5;
            SmokeRegionHeight = 4;

            AcidMeltsStoneChance = 60;
            AcidMeltsWoodChance = 30;
            AcidMeltsPlantChance = 10;
            AcidMeltsAshChance = 10;
            AcidMeltsObsidianChance = 100;
            AcidMeltsIceChance = 150;
            AcidDissolvesInWaterChance = 40;
            AcidMakesSandFromDirtChance = 20;

            IceFreezesWaterChance = 20;
            IceMeltsFromHeatChance = 10;

            PlantGrowthChance = 5;
            PlantSpontaneousGrowthChance = 200;

            VirusDevourChance = 2;
    }

        public static float Gravity { get; }
        public static float OneEighthOfGravity { get; }
        public static float MethaneGravity { get; }
        public static float GasGravity { get; }

        public static int MaxRadius { get; }

        public static int WaterSpreadRate { get; }
        public static int SandSpreadRate { get; }
        public static int OilSpreadRate { get; }
        public static int AcidSpreadRate { get; }
        public static int LavaSpreadRate { get; }
        public static int MethaneSpreadRate { get; }
        public static int FireSpreadChance { get; }
        public static int BurningGasSpreadChance { get; }
        public static float SteamSpreadSpeed { get; }
        public static float SmokeSpreadSpeed { get; }

        public static int FireLifeTime { get; }
        public static int SteamLifeTime { get; }
        public static int SmokeLifeTime { get; }
        public static int EmberLifeTime { get; }
        public static int LavaLifeTime { get; }
        public static int BurningGasLifeTime { get; }
        public static int VirusLifeTime { get; }

        public static int SmokeSpawnChance { get; }
        public static int SteamSpawnChance { get; }
        public static int EmberSpawnChance { get; }
        public static int AshSpawnChance { get; }
        public static int LavaSpawnsSmokeChance { get; }
        public static int LavaSpawnsEmberChance { get; }
        public static int SteamCondencesChance { get; }
        public static int SmokeCondencesChance { get; }
        public static int LavaMeltsStoneChance { get; }
        public static int LavaMeltsSandChance { get; }
        public static int LavaMeltsDirtChance { get; }

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
        public static int AcidMeltsPlantChance { get; }
        public static int AcidMeltsAshChance { get; }
        public static int AcidMeltsObsidianChance { get; }
        public static int AcidMeltsIceChance { get; }
        public static int AcidDissolvesInWaterChance { get; }
        public static int AcidMakesSandFromDirtChance { get; }

        public static int IceFreezesWaterChance { get; }
        public static int IceMeltsFromHeatChance { get; }

        public static int PlantGrowthChance { get; }
        public static int PlantSpontaneousGrowthChance { get; }

        public static int VirusDevourChance { get; }
    }
}
