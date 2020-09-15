using SFML.Graphics;

namespace Tetris.Models
{
    public class Field : Drawable
    {
        private readonly Brick[,] _field = new Brick[8, 16];
        private readonly Texture _texture;

        public Field(Texture texture)
        {
            _texture = texture;
        }

        public void CreateNewBrick()
        {
            for (var i = 0; i < 8; i++)
            {
                var brick = new Brick(_texture, i, 0);
                _field[i, 0] = brick;
            }
        }

        public void MoveBricks()
        {
            if (AllBricksIsFrozen())
            {
                CreateNewBrick();
            }
            else
            {
                for (var y = 15; y > -1; y--)
                {
                    for (var x = 0; x < 8; x++)
                    {
                        if (_field[x, y] != null && !_field[x, y].Frozen)
                        {
                            if (y + 1 < 16 && _field[x, y + 1] == null)
                            {
                                _field[x, y].Y += 1;
                                _field[x, y + 1] = _field[x, y];
                                _field[x, y] = null;
                            }
                            else
                            {
                                FreezeBricks();
                                RemoveRows();
                            }
                        }
                    }
                }
            }
        }

        private void RemoveRows()
        {
            for (var y = 15; y > -1; y--)
            {
                for (var x = 0; x < 8; x++)
                {
                    // Выйти, если одна из ячеек не заполнена
                    if (_field[x, y] == null) break;

                    // Когда дошел до последней ячейки
                    if (x != 7) continue;

                    // Удаляю все ячейки из этой строки
                    for (var i = 0; i < 8; i++)
                    {
                        _field[i, y] = null;
                    }

                    // Все строки выше - смещаю на единицу вниз
                    for (var nY = y; nY > 0; nY--)
                    {
                        for (var i = 0; i < 8; i++)
                        {
                            if (_field[i, nY - 1] != null)
                            {
                                _field[i, nY - 1].Y += 1;
                                _field[i, nY] = _field[i, nY - 1];
                                _field[i, nY - 1] = null;
                            }
                        }
                    }
                }
            }
        }

        private void FreezeBricks()
        {
            for (var y = 0; y < 16; y++)
            {
                for (var x = 0; x < 8; x++)
                {
                    if (_field[x, y] != null)
                    {
                        _field[x, y].Frozen = true;
                    }
                }
            }
        }

        private bool AllBricksIsFrozen()
        {
            var result = true;
            for (var y = 15; y > -1; y--)
            {
                for (var x = 0; x < 8; x++)
                {
                    if (_field[x, y] != null && !_field[x, y].Frozen) result = false;
                }
            }

            return result;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            for (var y = 0; y < 16; y++)
            {
                for (var x = 0; x < 8; x++)
                {
                    if (_field[x, y] != null)
                    {
                        _field[x, y].Draw(target, states);
                    }
                }
            }
        }
    }
}
