using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Graphics;

namespace MonoGame.Extended.TileMaps
{
    public class TileSet
    {
        public TileSet(Texture2D texture, int firstId, int tileWidth, int tileHeight, int spacing = 2, int margin = 2)
        {
            Texture = texture;
            FirstId = firstId;
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            Spacing = spacing;
            Margin = margin;

            var id = FirstId;
            _regions = new Dictionary<int, TextureRegion2D>();

            for (var x = Margin; x < texture.Width - Margin; x += TileWidth + Spacing)
            {
                for (var y = Margin; y < texture.Height - Margin; y += TileHeight + Spacing)
                {
                    _regions.Add(id, new TextureRegion2D(Texture, x, y, TileWidth, TileHeight));
                    id++;
                }
            }
        }

        private readonly Dictionary<int, TextureRegion2D> _regions; 

        public Texture2D Texture { get; private set; }
        public int FirstId { get; private set; }
        public int TileWidth { get; private set; }
        public int TileHeight { get; private set; }
        public int Spacing { get; private set; }
        public int Margin { get; private set; }

        public TextureRegion2D GetTileRegion(int id)
        {
            return id == 0 ? null : _regions[id];
        }
    }
}