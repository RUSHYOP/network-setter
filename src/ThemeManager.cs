using System;
using System.Drawing;
using System.Windows.Forms;

namespace NetworkSetter
{
    public enum Theme
    {
        Light,
        Dark,
        System
    }

    public static class ThemeManager
    {
        private static Theme _currentTheme = Theme.System;

        public static event EventHandler? ThemeChanged;

        public static Theme CurrentTheme
        {
            get => _currentTheme;
            set
            {
                if (_currentTheme != value)
                {
                    _currentTheme = value;
                    ThemeChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public static Theme GetEffectiveTheme()
        {
            if (CurrentTheme == Theme.System)
            {
                return IsSystemDarkMode() ? Theme.Dark : Theme.Light;
            }
            return CurrentTheme;
        }

        private static bool IsSystemDarkMode()
        {
            try
            {
                using (var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(
                    @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize"))
                {
                    var value = key?.GetValue("AppsUseLightTheme");
                    return value is int intValue && intValue == 0;
                }
            }
            catch
            {
                return false;
            }
        }

        public static void ApplyTheme(Form form)
        {
            var theme = GetEffectiveTheme();
            
            if (theme == Theme.Dark)
            {
                ApplyDarkTheme(form);
            }
            else
            {
                ApplyLightTheme(form);
            }
        }

        private static void ApplyLightTheme(Control control)
        {
            control.BackColor = Color.White;
            control.ForeColor = Color.Black;

            foreach (Control child in control.Controls)
            {
                if (child is GroupBox || child is Panel)
                {
                    ApplyLightTheme(child);
                }
                else if (child is Button btn)
                {
                    btn.BackColor = Color.FromArgb(240, 240, 240);
                    btn.ForeColor = Color.Black;
                    btn.FlatStyle = FlatStyle.Standard;
                }
                else if (child is TextBox txt)
                {
                    txt.BackColor = Color.White;
                    txt.ForeColor = Color.Black;
                    txt.BorderStyle = BorderStyle.Fixed3D;
                }
                else if (child is ComboBox cmb)
                {
                    cmb.BackColor = Color.White;
                    cmb.ForeColor = Color.Black;
                }
                else if (child is ListBox lst)
                {
                    lst.BackColor = Color.White;
                    lst.ForeColor = Color.Black;
                }
                else if (child is Label || child is RadioButton || child is CheckBox)
                {
                    child.BackColor = Color.Transparent;
                    child.ForeColor = Color.Black;
                }
                else if (child is MenuStrip menu)
                {
                    menu.BackColor = Color.FromArgb(240, 240, 240);
                    menu.ForeColor = Color.Black;
                    foreach (ToolStripMenuItem item in menu.Items)
                    {
                        ApplyLightThemeToMenuItem(item);
                    }
                }
            }
        }

        private static void ApplyDarkTheme(Control control)
        {
            control.BackColor = Color.FromArgb(32, 32, 32);
            control.ForeColor = Color.White;

            foreach (Control child in control.Controls)
            {
                if (child is GroupBox || child is Panel)
                {
                    child.BackColor = Color.FromArgb(32, 32, 32);
                    child.ForeColor = Color.White;
                    ApplyDarkTheme(child);
                }
                else if (child is Button btn)
                {
                    btn.BackColor = Color.FromArgb(64, 64, 64);
                    btn.ForeColor = Color.White;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderColor = Color.FromArgb(100, 100, 100);
                }
                else if (child is TextBox txt)
                {
                    txt.BackColor = Color.FromArgb(45, 45, 45);
                    txt.ForeColor = Color.White;
                    txt.BorderStyle = BorderStyle.FixedSingle;
                }
                else if (child is ComboBox cmb)
                {
                    cmb.BackColor = Color.FromArgb(45, 45, 45);
                    cmb.ForeColor = Color.White;
                    cmb.FlatStyle = FlatStyle.Flat;
                }
                else if (child is ListBox lst)
                {
                    lst.BackColor = Color.FromArgb(45, 45, 45);
                    lst.ForeColor = Color.White;
                    lst.BorderStyle = BorderStyle.FixedSingle;
                }
                else if (child is Label || child is RadioButton || child is CheckBox)
                {
                    child.BackColor = Color.Transparent;
                    child.ForeColor = Color.White;
                }
                else if (child is MenuStrip menu)
                {
                    menu.BackColor = Color.FromArgb(45, 45, 45);
                    menu.ForeColor = Color.White;
                    menu.Renderer = new DarkMenuRenderer();
                    foreach (ToolStripMenuItem item in menu.Items)
                    {
                        ApplyDarkThemeToMenuItem(item);
                    }
                }
            }
        }

        private static void ApplyLightThemeToMenuItem(ToolStripMenuItem item)
        {
            item.BackColor = Color.FromArgb(240, 240, 240);
            item.ForeColor = Color.Black;
            foreach (ToolStripItem subItem in item.DropDownItems)
            {
                if (subItem is ToolStripMenuItem menuItem)
                {
                    ApplyLightThemeToMenuItem(menuItem);
                }
                else
                {
                    subItem.BackColor = Color.FromArgb(240, 240, 240);
                    subItem.ForeColor = Color.Black;
                }
            }
        }

        private static void ApplyDarkThemeToMenuItem(ToolStripMenuItem item)
        {
            item.BackColor = Color.FromArgb(45, 45, 45);
            item.ForeColor = Color.White;
            foreach (ToolStripItem subItem in item.DropDownItems)
            {
                if (subItem is ToolStripMenuItem menuItem)
                {
                    ApplyDarkThemeToMenuItem(menuItem);
                }
                else
                {
                    subItem.BackColor = Color.FromArgb(45, 45, 45);
                    subItem.ForeColor = Color.White;
                }
            }
        }

        private class DarkMenuRenderer : ToolStripProfessionalRenderer
        {
            public DarkMenuRenderer() : base(new DarkColorTable()) { }
        }

        private class DarkColorTable : ProfessionalColorTable
        {
            public override Color MenuItemSelected => Color.FromArgb(70, 70, 70);
            public override Color MenuItemSelectedGradientBegin => Color.FromArgb(70, 70, 70);
            public override Color MenuItemSelectedGradientEnd => Color.FromArgb(70, 70, 70);
            public override Color MenuItemBorder => Color.FromArgb(100, 100, 100);
            public override Color MenuItemPressedGradientBegin => Color.FromArgb(80, 80, 80);
            public override Color MenuItemPressedGradientEnd => Color.FromArgb(80, 80, 80);
            public override Color ToolStripDropDownBackground => Color.FromArgb(45, 45, 45);
            public override Color ImageMarginGradientBegin => Color.FromArgb(45, 45, 45);
            public override Color ImageMarginGradientMiddle => Color.FromArgb(45, 45, 45);
            public override Color ImageMarginGradientEnd => Color.FromArgb(45, 45, 45);
        }
    }
}
