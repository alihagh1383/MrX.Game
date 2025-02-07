
using OpenTK.Windowing.Desktop;

namespace MrX.Game
{
    public class Program
    {
        [STAThread]
        // Entry point of the program
        static void Main(string[] args)
        {
            var E = new IsometrikEngin(GameWindowSettings.Default, NativeWindowSettings.Default);
            E.Run();
        }
    }
}