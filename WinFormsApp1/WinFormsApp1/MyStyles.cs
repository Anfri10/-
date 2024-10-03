using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public class MyStyles
    {
        int theme;//тема
        int font;//шрифт
        int fontSize;//размер шрифта
        List<string> nameTheme = new List<string> { "Gold","Цветущая вишня", "Лягушачье болото", "Небесная синь" };
        List<Color> themeColor = new List<Color> { Color.Gold, Color.MistyRose, Color.DarkKhaki, Color.DeepSkyBlue };
        List<Color> buttonColor = new List<Color> { Color.Black, Color.Salmon, Color.Olive, Color.AliceBlue };
        List<Color> foreColor = new List<Color> { Color.Black, Color.Black, Color.Beige, Color.DarkSlateGray };
        FontFamily[] fonts = new FontFamily[] { new FontFamily("Candara"), new FontFamily("Comic Sans MS"), new FontFamily("Impact") };
        int[] fontSizeArray = new int[] { 9, 12, 14 };
        public int Theme { get => theme; set => theme = value; }
        public int Font { get => font; set => font = value; }
        public int FontSize { get => fontSize; set => fontSize = value; }
        public FontFamily[] Fonts { get => fonts; }
        public List<string> NameTheme { get => nameTheme; set => nameTheme = value; }

        private MyStyles(int theme, int font, int fontSize)
        {
            this.theme = theme;
            this.font = font;
            this.fontSize = fontSize;
        }

        private MyStyles()
        {

        }
        static MyStyles myStyles = new MyStyles(0, 0, 0);
        public static MyStyles GetInstance()
        {
             return myStyles; 
        }
        /// <summary>
        /// Сброс настроек отображения
        /// </summary>
        public void Reset()
        {
            theme = 0;
            font = 0;
            fontSize = 0;
        }
        /// <summary>
        /// Установка настроек отображения на форме
        /// </summary>
        /// <param name="form"></param>
        public void SetTheme(Form form)
        {
            form.BackColor = themeColor[theme];
            form.Font = new Font(fonts[font], fontSizeArray[fontSize]);
            form.ForeColor = foreColor[theme];
            foreach (Control c in form.Controls)
            {
                if (c is Button)
                {
                    c.BackColor = buttonColor[theme];
                    (c as Button).FlatStyle = FlatStyle.Flat;
                    (c as Button).ForeColor = Color.White;
                }
                else if (c is TabControl)
                {
                    foreach (TabPage p in ((TabControl)c).Controls)
                    {
                        p.BackColor = themeColor[theme];
                    }
                }
                //else if (c is DataGridView)
                //{
                //    (c as DataGridView).BackColor = Color.LightGray;
                //}
            }

        }
        public void CreateTheme(string name, Color backColor, Color buttonColor, Color foreColor)
        {
            nameTheme.Add(name);
            themeColor.Add(backColor);
            this.buttonColor.Add(buttonColor);
            this.foreColor.Add(foreColor);
        }
    }
}
