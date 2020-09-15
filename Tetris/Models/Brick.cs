using SFML.Graphics;
using SFML.System;
using System;

namespace Tetris.Models
{
    public class Brick : Drawable
    {
        private const float Scale = 1.0f;
        private const int BrickSize = 56;

        private readonly Sprite _brickSprite;
        private readonly Random _rand = new Random(Guid.NewGuid().GetHashCode());

        public Brick(Texture texture, int x, int y)
        {
            var randomLine = _rand.Next(0, 2);
            var randomCol = _rand.Next(0, 3);
            _brickSprite = new Sprite(texture)
            {
                TextureRect = new IntRect(new Vector2i(BrickSize * randomCol, BrickSize * randomLine), new Vector2i(BrickSize, BrickSize)),
                Scale = new Vector2f(Scale, Scale)
            };
            X = x;
            Y = y;
        } 

        public int X { get; set; }
        public int Y { get; set; }

        public Vector2f LeftAngle { get; set; } = new Vector2f(300, 100);

        public bool Frozen { get; set; } = false;

        public void Draw(RenderTarget target, RenderStates states)
        {
            _brickSprite.Position = new Vector2f(LeftAngle.X + (float)BrickSize * X * Scale + 1, LeftAngle.Y + (float)BrickSize * Y * Scale + 1);
            target.Draw(_brickSprite);
        }
    }
}
