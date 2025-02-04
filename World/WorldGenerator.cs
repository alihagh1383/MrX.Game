using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrX.Noise;
using MrX.Game.NoiseGen;
using MrX.Game.World.Blocks;
using OpenTK.Mathematics;

namespace MrX.Game.World
{
    public class WorldGenerator
    {


        FastNoiseLite moisture = new();
        FastNoiseLite temperature = new();
        FastNoiseLite altitude = new();
        public WorldGenerator(int seed = 150)
        {
            var r = new Random(seed);
            var f = 0.01f;
            altitude.SetSeed(r.Next());
            altitude.SetFractalOctaves(5);
            altitude.SetFractalType(FastNoiseLite.FractalType.PingPong);
            altitude.SetNoiseType(FastNoiseLite.NoiseType.Cellular);
            altitude.SetFrequency(f);
            altitude.SetFractalGain(0.2f);
            altitude.SetFractalLacunarity(7f);

            temperature.SetSeed(r.Next());
            temperature.SetFractalOctaves(10);
            temperature.SetFractalType(FastNoiseLite.FractalType.FBm);
            temperature.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2 );
            temperature.SetFrequency(f);
            temperature.SetFractalGain(0.6f);
            temperature.SetFractalLacunarity(2f);
            temperature.SetFractalPingPongStrength(2f);
            temperature.SetCellularDistanceFunction(FastNoiseLite.CellularDistanceFunction.EuclideanSq);
        }
        public double Get(float i, float j) =>
            moisture.GetNoise(i, j) * 100;
        //perlinNoise.Get2D(i, j) * 100;
        public Block BlockCategoryAtPosition(Vector3 blockPos)
        {
            //var moist = (moisture.GetNoise(blockPos.X, blockPos.Z) + 1) / 2;
            var temp = (temperature.GetNoise(blockPos.X, blockPos.Z) + 1) / 2;
            var alt = (altitude.GetNoise(blockPos.X, blockPos.Z) + 1) / 2;
            switch (alt)
            {
                case < 0.2f:
                    return (temp < 0.3) ? new Sand(blockPos) : (temp < 0.6) ? new Water(blockPos) : new Snow(blockPos);
                case < 0.6f:
                    return (temp < 0.3) ? new Sand(blockPos) : (temp < 0.6) ? new Grass(blockPos) : new Snow(blockPos);
                default:
                    return (temp < 0.3) ? new Sand(blockPos) : (temp < 0.6) ? new Grass(blockPos) : new Snow(blockPos);
            }
        }

    }
}
