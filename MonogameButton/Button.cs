using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FormElementsLib;

namespace MonogameButton
{
    public class Button : TextFE
    {
        public Texture2D textureinit, textureon, texturepressed;
        public SpriteFont font;
        public int fontsize, fontinitsize;
        public bool textmode;
        public SpriteEffects effects = SpriteEffects.None;
        public float scale = 1f;
        public int texty = 0, textx = 0;

        public Button(int x, int y, int width, int height, Texture2D _textureinit, Texture2D _textureon, Texture2D _texturepressed)
        {
            color = Color.White;
            textcolor = Microsoft.Xna.Framework.Color.Black;
            Location = new Vector2(x, y);
            Size = new System.Drawing.Size(width, height);
            textureinit = _textureinit;
            textureon = _textureon;
            texturepressed = _texturepressed;
            textmode = false;
        }

        public Button(int x, int y, Texture2D texture)
        {
            color = Color.White;
            textcolor = Microsoft.Xna.Framework.Color.Black;
            Location = new Vector2(x, y);
            Size = new System.Drawing.Size(texture.Width, texture.Height);
            textureinit = textureon = texturepressed = texture;
            textmode = false;
        }

        public Button(GraphicsDevice graphicsDevice, int x, int y, int width, int height, string _text, SpriteFont _font, int _fontinitsize, int _fontsize)
        {
            textcolor = Microsoft.Xna.Framework.Color.Black;
            Location = new Vector2(x, y);
            Size = new System.Drawing.Size(width, height);
            font = _font;
            text = _text;
            fontsize = _fontsize;
            fontinitsize = _fontinitsize;
            makebuttoncolor(graphicsDevice, ref textureinit, new Microsoft.Xna.Framework.Color(225, 225, 225), new Microsoft.Xna.Framework.Color(193, 173, 173));
            makebuttoncolor(graphicsDevice, ref textureon, new Microsoft.Xna.Framework.Color(229, 241, 251), new Microsoft.Xna.Framework.Color(0, 120, 215));
            makebuttoncolor(graphicsDevice, ref texturepressed, new Microsoft.Xna.Framework.Color(204, 228, 247), new Microsoft.Xna.Framework.Color(0, 84, 153));
            textmode = true;
        }
        public Button(GraphicsDevice graphicsDevice, int x, int y, int width, int height, string _text, SpriteFont _font, int _fontinitsize, int _fontsize, Texture2D _textureinit, Texture2D _textureon, Texture2D _texturepressed)
        {
            color = Color.White;
            textcolor = Microsoft.Xna.Framework.Color.Black;
            Location = new Vector2(x, y);
            Size = new System.Drawing.Size(width, height);
            font = _font;
            text = _text;
            fontsize = _fontsize;
            fontinitsize = _fontinitsize;
            textureinit = _textureinit;
            textureon = _textureon;
            texturepressed = _texturepressed;
            textmode = true;
        }

        private void makebuttoncolor(GraphicsDevice graphicsDevice, ref Texture2D texture, Microsoft.Xna.Framework.Color colorin, Microsoft.Xna.Framework.Color colorout)
        {
            color = Color.White;
            texture = new Texture2D(graphicsDevice, Size.Width, Size.Height);
            Microsoft.Xna.Framework.Color[] colordata = new Microsoft.Xna.Framework.Color[Size.Width * Size.Height];
            for (int i = 0; i < Size.Width * Size.Height; i++) colordata[i] = colorin;
            for (int i = 0; i < Size.Width; i++) colordata[i] = colorout;
            for (int i = 0; i < Size.Width; i++) colordata[(Size.Height - 1) * Size.Width + i] = colorout;
            for (int i = 0; i < Size.Height; i++) colordata[i * Size.Width] = colorout;
            for (int i = 0; i < Size.Height; i++) colordata[i * Size.Width + Size.Width - 1] = colorout;
            texture.SetData<Microsoft.Xna.Framework.Color>(colordata);
        }

        public static Texture2D makebuttoncolor(GraphicsDevice graphicsDevice, int width, int height, Microsoft.Xna.Framework.Color colorin, Microsoft.Xna.Framework.Color colorout)
        {
            Texture2D texture = new Texture2D(graphicsDevice, width, height);
            Microsoft.Xna.Framework.Color[] colordata = new Microsoft.Xna.Framework.Color[width * height];
            for (int i = 0; i < width * height; i++) colordata[i] = colorin;
            for (int i = 0; i < width; i++) colordata[i] = colorout;
            for (int i = 0; i < width; i++) colordata[(height - 1) * width + i] = colorout;
            for (int i = 0; i < height; i++) colordata[i * width] = colorout;
            for (int i = 0; i < height; i++) colordata[i * width + width - 1] = colorout;
            texture.SetData<Microsoft.Xna.Framework.Color>(colordata);
            return texture;
        }

        public override void Release()
        {
            mode = 0;
            pressed = false;
        }
        public override void MakePressed()
        {
            mode = 2;
        }
        public override void OnElement()
        {
            mode = 1;
        }

        public override void Dispose()
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (visible)
            {
                switch (mode)
                {
                    case 0:
                        spriteBatch.Draw(textureinit, Location, null, color, 0f, new Vector2(0, 0), scale, effects, 1);
                        if (textmode)
                        {
                            spriteBatch.DrawString(font, text, new Vector2(textx + Location.X + (Size.Width - (float)fontsize / fontinitsize * font.MeasureString(text).X) / 2, texty + Location.Y + (Size.Height - (float)fontsize / fontinitsize * font.MeasureString(text).Y) / 2), textcolor, 0f, new Vector2(0, 0), ((float)fontsize / fontinitsize)*scale, effects, 1f);
                        }
                        break;
                    case 1:
                        spriteBatch.Draw(textureon, Location, null, color, 0f, new Vector2(0, 0), scale, effects, 1f);
                        if (textmode)
                        {
                            spriteBatch.Draw(textureon, Location, null, color, 0f, new Vector2(0, 0), scale, effects, 1f);
                            spriteBatch.DrawString(font, text, new Vector2(textx + Location.X + (Size.Width - (float)fontsize / fontinitsize * font.MeasureString(text).X) / 2, texty + Location.Y + (Size.Height - (float)fontsize / fontinitsize * font.MeasureString(text).Y) / 2), textcolor, 0f, new Vector2(0, 0), ((float)fontsize / fontinitsize) * scale, effects, 1f);
                        }
                        break;
                    case 2:
                        spriteBatch.Draw(texturepressed, Location, null, color, 0f, new Vector2(0, 0), scale, effects, 1f);
                        if (textmode)
                        {
                            spriteBatch.DrawString(font, text, new Vector2(textx + Location.X + (Size.Width - (float)fontsize / fontinitsize * font.MeasureString(text).X) / 2, texty + Location.Y + (Size.Height - (float)fontsize / fontinitsize * font.MeasureString(text).Y) / 2), textcolor, 0f, new Vector2(0, 0), ((float)fontsize / fontinitsize) * scale, effects, 1f);
                        }
                        break;
                }
            }
        }
    }
}
