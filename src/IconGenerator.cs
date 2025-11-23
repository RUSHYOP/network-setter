using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace NetworkSetter
{
    public static class IconGenerator
    {
        public static Icon CreateAppIcon()
        {
            var bitmap = new Bitmap(32, 32);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);

                // Draw network icon
                // Background circle
                using (var brush = new LinearGradientBrush(
                    new Rectangle(0, 0, 32, 32),
                    Color.FromArgb(30, 144, 255),
                    Color.FromArgb(0, 100, 200),
                    LinearGradientMode.Vertical))
                {
                    g.FillEllipse(brush, 2, 2, 28, 28);
                }

                // Border
                using (var pen = new Pen(Color.White, 2))
                {
                    g.DrawEllipse(pen, 2, 2, 28, 28);
                }

                // Network nodes
                using (var brush = new SolidBrush(Color.White))
                {
                    g.FillEllipse(brush, 14, 6, 4, 4);   // Top
                    g.FillEllipse(brush, 8, 14, 4, 4);   // Left
                    g.FillEllipse(brush, 20, 14, 4, 4);  // Right
                    g.FillEllipse(brush, 14, 22, 4, 4);  // Bottom
                }

                // Connecting lines
                using (var pen = new Pen(Color.White, 2))
                {
                    g.DrawLine(pen, 16, 10, 16, 16);     // Top to center
                    g.DrawLine(pen, 12, 16, 16, 16);     // Left to center
                    g.DrawLine(pen, 20, 16, 16, 16);     // Right to center
                    g.DrawLine(pen, 16, 20, 16, 16);     // Bottom to center
                }

                // Center node
                using (var brush = new SolidBrush(Color.Yellow))
                {
                    g.FillEllipse(brush, 13, 13, 6, 6);
                }
            }

            return Icon.FromHandle(bitmap.GetHicon());
        }

        public static void SaveIconToFile(string filePath)
        {
            using (var icon = CreateAppIcon())
            using (var fs = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
            {
                icon.Save(fs);
            }
        }
    }
}
