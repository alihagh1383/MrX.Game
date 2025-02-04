using MrX.Game.World;
namespace MrX.Game
{
    public class Program
    {
        [STAThread]
        // Entry point of the program
        static void Main(string[] args)
        {
            List<double> data = new List<double>();
            for (int t = 0; t < 10; t++)
            {
                WorldGenerator worldGenerator = new WorldGenerator(Random.Shared.Next());
                for (int i = 0; i < 100; i++)
                    for (int j = 0; j < 100; j++)
                        data.Add(worldGenerator.Get(i, j));
            }
            Console.WriteLine(data.Max());
            Console.WriteLine(data.Min());
            // Creates game object and disposes of it after leaving the scope
            using (Game game = new Game(1000, 1000))
            {
                // running the game
                game.UpdateFrequency = 5 / 1f;
                game.Run();
            }
        }
    }
}