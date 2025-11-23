using System;
using System.Drawing;
using System.Windows.Forms;

namespace NetworkSetter
{
    public partial class SettingsForm : Form
    {
        private CheckBox chkRunAtStartup;
        private CheckBox chkMinimizeToTray;
        private CheckBox chkStartMinimized;
        private CheckBox chkShowNotifications;
        private RadioButton rbThemeLight;
        private RadioButton rbThemeDark;
        private RadioButton rbThemeSystem;
        private Button btnSave;
        private Button btnCancel;
        private AppSettings _settings;

        public SettingsForm()
        {
            _settings = SettingsManager.LoadSettings();
            InitializeComponent();
            LoadSettings();
            ThemeManager.ThemeChanged += (s, e) => ThemeManager.ApplyTheme(this);
            ThemeManager.ApplyTheme(this);
        }

        private void InitializeComponent()
        {
            this.Text = "Settings - Network Setter";
            this.Size = new Size(450, 350);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // General Settings Group
            var grpGeneral = new GroupBox
            {
                Text = "General Settings",
                Location = new Point(15, 15),
                Size = new Size(405, 130)
            };
            this.Controls.Add(grpGeneral);

            chkRunAtStartup = new CheckBox
            {
                Text = "Run at Windows startup",
                Location = new Point(15, 25),
                Size = new Size(370, 20)
            };
            grpGeneral.Controls.Add(chkRunAtStartup);

            chkMinimizeToTray = new CheckBox
            {
                Text = "Minimize to system tray (instead of taskbar)",
                Location = new Point(15, 55),
                Size = new Size(370, 20)
            };
            grpGeneral.Controls.Add(chkMinimizeToTray);

            chkStartMinimized = new CheckBox
            {
                Text = "Start minimized to tray",
                Location = new Point(15, 85),
                Size = new Size(370, 20)
            };
            grpGeneral.Controls.Add(chkStartMinimized);

            chkShowNotifications = new CheckBox
            {
                Text = "Show notifications when settings are applied",
                Location = new Point(15, 115),
                Size = new Size(370, 20),
                Checked = true
            };
            grpGeneral.Controls.Add(chkShowNotifications);

            // Theme Settings Group
            var grpTheme = new GroupBox
            {
                Text = "Theme",
                Location = new Point(15, 155),
                Size = new Size(405, 100)
            };
            this.Controls.Add(grpTheme);

            rbThemeLight = new RadioButton
            {
                Text = "☀️ Light",
                Location = new Point(15, 25),
                Size = new Size(120, 20)
            };
            grpTheme.Controls.Add(rbThemeLight);

            rbThemeDark = new RadioButton
            {
                Text = "🌙 Dark",
                Location = new Point(15, 55),
                Size = new Size(120, 20)
            };
            grpTheme.Controls.Add(rbThemeDark);

            rbThemeSystem = new RadioButton
            {
                Text = "💻 System Default",
                Location = new Point(15, 85),
                Size = new Size(150, 20),
                Checked = true
            };
            grpTheme.Controls.Add(rbThemeSystem);

            // Buttons
            btnSave = new Button
            {
                Text = "Save",
                Location = new Point(240, 270),
                Size = new Size(90, 30),
                DialogResult = DialogResult.OK
            };
            btnSave.Click += BtnSave_Click;
            this.Controls.Add(btnSave);

            btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(335, 270),
                Size = new Size(90, 30),
                DialogResult = DialogResult.Cancel
            };
            this.Controls.Add(btnCancel);

            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;
        }

        private void LoadSettings()
        {
            chkRunAtStartup.Checked = _settings.RunAtStartup;
            chkMinimizeToTray.Checked = _settings.MinimizeToTray;
            chkStartMinimized.Checked = _settings.StartMinimized;
            chkShowNotifications.Checked = _settings.ShowNotifications;

            switch (_settings.Theme)
            {
                case Theme.Light:
                    rbThemeLight.Checked = true;
                    break;
                case Theme.Dark:
                    rbThemeDark.Checked = true;
                    break;
                case Theme.System:
                    rbThemeSystem.Checked = true;
                    break;
            }
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            try
            {
                _settings.RunAtStartup = chkRunAtStartup.Checked;
                _settings.MinimizeToTray = chkMinimizeToTray.Checked;
                _settings.StartMinimized = chkStartMinimized.Checked;
                _settings.ShowNotifications = chkShowNotifications.Checked;

                if (rbThemeLight.Checked)
                    _settings.Theme = Theme.Light;
                else if (rbThemeDark.Checked)
                    _settings.Theme = Theme.Dark;
                else
                    _settings.Theme = Theme.System;

                SettingsManager.SaveSettings(_settings);
                ThemeManager.CurrentTheme = _settings.Theme;

                MessageBox.Show("Settings saved successfully!", "Success", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving settings: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
