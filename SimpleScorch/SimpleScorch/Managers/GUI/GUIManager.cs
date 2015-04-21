using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleScorch.Managers.GUI
{
    public class GUIManager : ManagerBase
    {
        private static Texture2D alphabet;
        private static Texture2D nums;
        public static Texture2D textWindow;
        public static int NUM_ROWS = 4;
        public static int NUM_COLM = 8;
        public static int NUM_COLR = 2;
        public Dictionary<string, WindowBase> currentWindows;

        public GUIManager() : base(true)
        {
            currentWindows = new Dictionary<string, WindowBase>();
        }

        public override void OnLoad(ContentManager content)
        {
            base.OnLoad(content);
            alphabet = content.Load<Texture2D>("letters");
            nums = content.Load<Texture2D>("additional");
            textWindow = content.Load<Texture2D>("messageBox");
        }
        
        public override void UpdateManager(GameTime gameTime)
        {
            base.UpdateManager(gameTime);     
        }

        public override void RenderManager(SpriteBatch spriteBatch)
        {
            base.RenderManager(spriteBatch);
            foreach(WindowBase wb in currentWindows.Values)
            {
                wb.RenderWindow(spriteBatch);
            }
        }

        /// <summary>
        /// Internal Method - PLEASE don't use this unless there's a very good reason to.
        /// You can ALWAYS use a window object!
        /// </summary>
        /// <param name="spriteBatch">Instance of SpriteBatch to render to.</param>
        /// <param name="toRender">String to render out.</param>
        /// <param name="startAt">Where to begin rendering the string.</param>
        /// <param name="white">White chars?</param>
        /// <param name="textScale">X and Y scaling of the resulting text.</param>
        public void RenderStringRaw(SpriteBatch spriteBatch, string toRender, Vector2 startAt, bool white, Vector2 textScale)
        {
            Vector2 currentPos = startAt;
            spriteBatch.Begin();
            if (toRender.Length > 62 || toRender.Contains('\n'))
            {
                List<string> splitString = new List<string>();
                string stringBuffer = "";
                for(int i = 0; i < toRender.Length;i++)
                {
                    if(i % 62 == 0 && i > 0)
                    {
                        if (stringBuffer[61] != ' ')
                        {
                            char res = stringBuffer[61];
                            stringBuffer = stringBuffer.Substring(0, 61) + '-';
                            splitString.Add(stringBuffer);
                            stringBuffer = res.ToString();
                        }
                        else
                        {
                            splitString.Add(stringBuffer);
                            stringBuffer = "";
                        }
                    }
                    if(toRender[i] == '\n')
                    {
                        i += 1;
                        splitString.Add(stringBuffer);
                        stringBuffer = "";
                    }
                    stringBuffer += toRender[i];
                }
                splitString.Add(stringBuffer);
                for (int s = 0; s < splitString.Count;s++ )
                {
                    int i = 0;
                    for (i = 0; i < splitString[s].Length;i++ )
                    {
                        Rectangle charSpot = CharToAlphabetRender(splitString[s][i], white);
                        if (charSpot == Rectangle.Empty)
                        {
                            if (splitString[s][i]!= ' ') continue;
                        }
                        spriteBatch.Draw((GetSheetToRender(splitString[s][i]) == 0 ? alphabet : nums), new Rectangle((int)currentPos.X, (int)currentPos.Y + (s * 16), (int)textScale.X, (int)textScale.Y), charSpot, Color.White);
                        currentPos.X += (int)textScale.X;
                    }
                    currentPos.X -= ((int)textScale.X)*i;
                }
            }
            else
            {
                foreach (char i in toRender)
                {
                    Rectangle charSpot = CharToAlphabetRender(i, white);
                    if (charSpot == Rectangle.Empty)
                    {
                        if (i != ' ') continue;
                    }
                    spriteBatch.Draw((GetSheetToRender(i) == 0 ? alphabet : nums), new Rectangle((int)currentPos.X, (int)currentPos.Y, (int)textScale.X, (int)textScale.Y), charSpot, Color.White);
                    currentPos.X += (int)textScale.X;
                }
            }
            spriteBatch.End();
        }

        public Rectangle CharToAlphabetRender(char character, bool white)
        {
            int yOffset = white ? 64 : 0;
            if (Char.IsLetter(character))
            {
                character = Char.ToLower(character);
                int finalNum = (int)character - 97;
                int fRow = (int)Math.Floor((double)(finalNum / 8));
                int fCol = finalNum % 8;
                return new Rectangle(fCol * 16, fRow * 16 + yOffset, 16, 16);
            }
            else if(Char.IsDigit(character))
            {
                int finalNum = ((int)Char.GetNumericValue(character)) - 1;
                if (finalNum == -1) finalNum = 9;
                int fRow = (int)Math.Floor((double)(finalNum / 8));
                int fCol = finalNum % 8;
                return new Rectangle(fCol * 16, fRow * 16 + yOffset, 16, 16);
            }
            else switch(character)
                {
                    case '.':
                        return new Rectangle(2 * 16, 3 * 16 + yOffset, 16, 16);
                    case '!':
                        return new Rectangle(3 * 16, 3 * 16 + yOffset, 16, 16);
                    case '?':
                        return new Rectangle(4 * 16, 3 * 16 + yOffset, 16, 16);
                    case '(':
                        return new Rectangle(5 * 16, 3 * 16 + yOffset, 16, 16);
                    case ')':
                        return new Rectangle(6 * 16, 3 * 16 + yOffset, 16, 16);
                    case '$':
                        return new Rectangle(7 * 16, 3 * 16 + yOffset, 16, 16);
                    case '*':
                        return new Rectangle(2 * 16, 1 * 16 + yOffset, 16, 16);
                    case '@':
                        return new Rectangle(3 * 16, 1 * 16 + yOffset, 16, 16);
                    case '#':
                        return new Rectangle(4 * 16, 1 * 16 + yOffset, 16, 16);
                    case '%':
                        return new Rectangle(5 * 16, 1 * 16 + yOffset, 16, 16);
                    case '^':
                        return new Rectangle(6 * 16, 1 * 16 + yOffset, 16, 16);
                    case '&':
                        return new Rectangle(7 * 16, 1 * 16 + yOffset, 16, 16);
                    case '[':
                        return new Rectangle(0 * 16, 2 * 16 + yOffset, 16, 16);
                    case ']':
                        return new Rectangle(1 * 16, 2 * 16 + yOffset, 16, 16);
                    case '{':
                        return new Rectangle(2 * 16, 2 * 16 + yOffset, 16, 16);
                    case '}':
                        return new Rectangle(3 * 16, 2 * 16 + yOffset, 16, 16);
                    case ':':
                        return new Rectangle(4 * 16, 2 * 16 + yOffset, 16, 16);
                    case '"':
                        return new Rectangle(5 * 16, 2 * 16 + yOffset, 16, 16);
                    case '\'':
                        return new Rectangle(6 * 16, 2 * 16 + yOffset, 16, 16);
                    case '-':
                        return new Rectangle(7 * 16, 2 * 16 + yOffset, 16, 16);
                    default:
                        return Rectangle.Empty;
                }
        }

        public int GetSheetToRender(char charToRender)
        {
            if (Char.IsLetter(charToRender)) return 0;
            else if (Char.IsNumber(charToRender)) return 1;
            else
            {
                switch(charToRender)
                {
                    case '.':
                    case '!':
                    case '?':
                    case '(':
                    case ')':
                    case '$':
                        return 0;
                    default:
                        return 1;
                }
            }
        }
    }
}
