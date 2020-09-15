using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.IO;
using Tetris.Models;

namespace Tetris
{
    internal class Program
    {
        private const int FrameLimit = 60;

        private static void Main()
        {
            var rand = new Random(Guid.NewGuid().GetHashCode());
            var gameIsOpened = true;

            var window = new RenderWindow(VideoMode.DesktopMode, "TETRIS")
            {
                Size = new Vector2u(800, 600)
            };

            var dir = Directory.GetCurrentDirectory();
            var texture = new Texture($@"{dir}\Textures\bricks.png");

            var field = new Field(texture);

            window.Closed += (o, e) => { gameIsOpened = false; };
            var clock = new Clock();

            while (gameIsOpened)
            {
                window.DispatchEvents();

                if (clock.ElapsedTime.AsMilliseconds() <= 1000 / FrameLimit) continue;

                var time = clock.ElapsedTime.AsSeconds();
                Console.Clear();
                Console.WriteLine(1.0f / time);
                clock.Restart();

                field.MoveBricks();

                window.Clear(Color.Black);
                window.Draw(field);
                window.Display();
            }
        }
    }
}
