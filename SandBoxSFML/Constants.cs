namespace SandBoxSFML
{
    public static class Constants
    {
        //rate -> how far can move in 1 frame
        //chance -> probability of event in 1.0/chance
        //lifetime -> how long will live in frames

        public static float Gravity { get; } = 1;
        public static float OneEighthOfGravity { get; } = Gravity / 8f;
        public static float MethaneGravity { get; } = -Gravity * 2;
        public static float GasGravity { get; } = -Gravity;
        public static int MaxRadius { get; } = 100;
        public static int WaterSpreadRate { get; } = 10;
        public static int SandSpreadRate { get; } = 2;
        public static int OilSpreadRate { get; } = 5;
        public static int AcidSpreadRate { get; } = 8;
        public static int LavaSpreadRate { get; } = 2;
        public static int MethaneSpreadRate { get; } = 5;
        public static int FireSpreadChance { get; } = 4;
        public static int BurningGasSpreadChance { get; } = 2;
        public static float SteamSpreadSpeed { get; } = 1.3f;
        public static float SmokeSpreadSpeed { get; } = 1.1f;
        public static int FireLifeTime { get; } = 100;
        public static int SteamLifeTime { get; } = 100;
        public static int SmokeLifeTime { get; } = 160;
        public static int EmberLifeTime { get; } = 200;
        public static int LavaLifeTime { get; } = 1500;
        public static int BurningGasLifeTime { get; } = 120;
        public static int VirusLifeTime { get; } = 100;
        public static int SmokeSpawnChance { get; } = 20;
        public static int SteamSpawnChance { get; } = 25;
        public static int EmberSpawnChance { get; } = 75;
        public static int AshSpawnChance { get; } = 5;
        public static int LavaSpawnsSmokeChance { get; } = 200;
        public static int LavaSpawnsEmberChance { get; } = 400;
        public static int SteamCondencesChance { get; } = 150;
        public static int SmokeCondencesChance { get; } = 250;
        public static int LavaMeltsStoneChance { get; } = 70;
        public static int LavaMeltsSandChance { get; } = 40;
        public static int LavaMeltsDirtChance { get; } = 50;
        public static int WoodIgnitionChance { get; } = 50;
        public static int CoalIgnitionChance { get; } = 30;
        public static int OilIgnitionChance { get; } = 15;
        public static int MethaneIgnitionChance { get; } = 1;
        public static int PlantIgnitionChance { get; } = 20;
        public static int SteamRegionWidth { get; } = 3;
        public static int SteamRegionHeight { get; } = 3;
        public static int SmokeRegionWidth { get; } = 5;
        public static int SmokeRegionHeight { get; } = 4;
        public static int AcidMeltsStoneChance { get; } = 60;
        public static int AcidMeltsWoodChance { get; } = 30;
        public static int AcidMeltsPlantChance { get; } = 10;
        public static int AcidMeltsAshChance { get; } = 10;
        public static int AcidMeltsObsidianChance { get; } = 200;
        public static int AcidMeltsIceChance { get; } = 140;
        public static int AcidDissolvesInWaterChance { get; } = 40;
        public static int AcidMakesSandFromDirtChance { get; } = 20;
        public static int AcidReactsWithOilChance { get; } = 10;
        public static int IceFreezesWaterChance { get; } = 20;
        public static int IceMeltsFromHeatChance { get; } = 10;
        public static int PlantGrowthChance { get; } = 5;
        public static int PlantSpontaneousGrowthChance { get; } = 200;
        public static int VirusDevourChance { get; } = 2;
    }
}
