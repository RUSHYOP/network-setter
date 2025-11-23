using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace NetworkSetter
{
    internal static class Program
    {
        private static Mutex? _mutex;
        private static NotifyIcon? _trayIcon;
        private static MainForm? _mainForm;
        private static TrayPopupForm? _trayPopupForm;
        private static bool _startMinimized = false;

        [STAThread]
        static void Main(string[] args)
        {
            // Check for command line arguments
            _startMinimized = args.Contains("--minimized");

            // Single instance check
            _mutex = new Mutex(true, "NetworkSetterV2SingleInstance", out bool isNewInstance);
            
            if (!isNewInstance)
            {
                MessageBox.Show("Network Setter is already running. Check the system tray.", 
                    "Already Running", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ApplicationConfiguration.Initialize();

            // Load settings and apply theme
            var settings = SettingsManager.LoadSettings();
            ThemeManager.CurrentTheme = settings.Theme;

            // Create system tray icon
            CreateTrayIcon();

            // Create main form but don't show it yet
            _mainForm = new MainForm();
            _mainForm.FormClosing += MainForm_FormClosing;

            // Create tray popup form
            _trayPopupForm = new TrayPopupForm();
            _trayPopupForm.OpenMainFormRequested += (s, e) => ShowMainForm();
            _trayPopupForm.SettingsRequested += (s, e) => ShowSettingsForm();

            // Show or minimize based on settings
            if (!_startMinimized && !settings.StartMinimized)
            {
                _mainForm.Show();
            }
            else
            {
                _mainForm.WindowState = FormWindowState.Minimized;
                _mainForm.Hide();
                
                if (settings.ShowNotifications)
                {
                    _trayIcon?.ShowBalloonTip(2000, "Network Setter", 
                        "Application is running in the background. Click the tray icon to access.", 
                        ToolTipIcon.Info);
                }
            }

            Application.Run();

            _mutex?.ReleaseMutex();
            _mutex?.Dispose();
        }

        private static void CreateTrayIcon()
        {
            _trayIcon = new NotifyIcon
            {
                Text = "Network Setter - Right-click for options",
                Visible = true
            };

            // Create a simple icon (you can replace this with a custom icon file)
            _trayIcon.Icon = CreateNetworkIcon();

            // Tray icon click events
            _trayIcon.Click += TrayIcon_Click;
            _trayIcon.DoubleClick += (s, e) => ShowMainForm();

            // Context menu
            var contextMenu = new ContextMenuStrip();
            
            var openItem = new ToolStripMenuItem("Open Network Setter");
            openItem.Click += (s, e) => ShowMainForm();
            openItem.Font = new Font(contextMenu.Font, FontStyle.Bold);
            contextMenu.Items.Add(openItem);

            contextMenu.Items.Add(new ToolStripSeparator());

            var quickAccessItem = new ToolStripMenuItem("Quick Access");
            quickAccessItem.Click += (s, e) => ShowTrayPopup();
            contextMenu.Items.Add(quickAccessItem);

            var settingsItem = new ToolStripMenuItem("Settings");
            settingsItem.Click += (s, e) => ShowSettingsForm();
            contextMenu.Items.Add(settingsItem);

            contextMenu.Items.Add(new ToolStripSeparator());

            var exitItem = new ToolStripMenuItem("Exit");
            exitItem.Click += (s, e) => ExitApplication();
            contextMenu.Items.Add(exitItem);

            _trayIcon.ContextMenuStrip = contextMenu;
        }

        private static Icon CreateNetworkIcon()
        {
            return IconGenerator.CreateAppIcon();
        }

        private static void TrayIcon_Click(object? sender, EventArgs e)
        {
            if (e is MouseEventArgs me && me.Button == MouseButtons.Left)
            {
                ShowTrayPopup();
            }
        }

        private static void ShowTrayPopup()
        {
            if (_trayPopupForm?.Visible == true)
            {
                _trayPopupForm.Hide();
            }
            else
            {
                _trayPopupForm?.Show();
            }
        }

        private static void ShowMainForm()
        {
            if (_mainForm != null)
            {
                if (_mainForm.WindowState == FormWindowState.Minimized)
                    _mainForm.WindowState = FormWindowState.Normal;
                
                _mainForm.Show();
                _mainForm.Activate();
                _mainForm.BringToFront();
            }
        }

        private static void ShowSettingsForm()
        {
            var settingsForm = new SettingsForm();
            settingsForm.ShowDialog();
        }

        private static void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (_mainForm == null) return;

            var settings = SettingsManager.LoadSettings();
            
            if (settings.MinimizeToTray && e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                _mainForm.Hide();
                
                if (settings.ShowNotifications)
                {
                    _trayIcon?.ShowBalloonTip(2000, "Network Setter", 
                        "Application minimized to tray. Double-click the tray icon to restore.", 
                        ToolTipIcon.Info);
                }
            }
        }

        private static void ExitApplication()
        {
            var result = MessageBox.Show(
                "Are you sure you want to exit Network Setter?",
                "Exit Application",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                _trayIcon?.Dispose();
                _trayPopupForm?.Close();
                _mainForm?.Close();
                Application.Exit();
            }
        }
    }
}
