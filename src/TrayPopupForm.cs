using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace NetworkSetter
{
    public partial class TrayPopupForm : Form
    {
        private ComboBox cmbQuickAdapter;
        private Button btnQuickDhcp;
        private Button btnOpenFull;
        private Button btnSettings;
        private Button btnExit;
        private Label lblStatus;
        private Panel pnlPresets;
        private const int FormWidth = 280;
        private const int FormHeight = 400;

        public event EventHandler? OpenMainFormRequested;
        public event EventHandler? SettingsRequested;

        public TrayPopupForm()
        {
            InitializeComponent();
            LoadQuickSettings();
            ThemeManager.ThemeChanged += (s, e) => ThemeManager.ApplyTheme(this);
            ThemeManager.ApplyTheme(this);
        }

        private void InitializeComponent()
        {
            this.Text = "Network Setter";
            this.Size = new Size(FormWidth, FormHeight);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.Manual;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.Padding = new Padding(10);

            // Position in bottom-right corner
            var workingArea = Screen.PrimaryScreen.WorkingArea;
            this.Location = new Point(
                workingArea.Right - this.Width - 10,
                workingArea.Bottom - this.Height - 10
            );

            this.Deactivate += (s, e) => this.Hide();

            // Title Label
            var lblTitle = new Label
            {
                Text = "⚡ Network Setter",
                Font = new Font(this.Font.FontFamily, 12, FontStyle.Bold),
                Location = new Point(10, 10),
                Size = new Size(FormWidth - 20, 25),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(lblTitle);

            // Quick Adapter Selection
            var lblAdapter = new Label
            {
                Text = "Adapter:",
                Location = new Point(10, 45),
                Size = new Size(60, 20)
            };
            this.Controls.Add(lblAdapter);

            cmbQuickAdapter = new ComboBox
            {
                Location = new Point(75, 43),
                Width = FormWidth - 95,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            this.Controls.Add(cmbQuickAdapter);

            // Quick DHCP Button
            btnQuickDhcp = new Button
            {
                Text = "🔄 Enable DHCP",
                Location = new Point(10, 75),
                Size = new Size(FormWidth - 30, 35),
                Font = new Font(this.Font.FontFamily, 9, FontStyle.Regular)
            };
            btnQuickDhcp.Click += BtnQuickDhcp_Click;
            this.Controls.Add(btnQuickDhcp);

            // Status Label
            lblStatus = new Label
            {
                Text = "Ready",
                Location = new Point(10, 120),
                Size = new Size(FormWidth - 30, 20),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Gray
            };
            this.Controls.Add(lblStatus);

            // Separator
            var separator1 = new Panel
            {
                Location = new Point(10, 145),
                Size = new Size(FormWidth - 30, 1),
                BackColor = Color.Gray
            };
            this.Controls.Add(separator1);

            // Presets Section
            var lblPresets = new Label
            {
                Text = "Quick Presets:",
                Location = new Point(10, 155),
                Size = new Size(FormWidth - 30, 20)
            };
            this.Controls.Add(lblPresets);

            pnlPresets = new Panel
            {
                Location = new Point(10, 180),
                Size = new Size(FormWidth - 30, 120),
                AutoScroll = true
            };
            this.Controls.Add(pnlPresets);

            LoadPresetButtons();

            // Separator
            var separator2 = new Panel
            {
                Location = new Point(10, 310),
                Size = new Size(FormWidth - 30, 1),
                BackColor = Color.Gray
            };
            this.Controls.Add(separator2);

            // Open Full App Button
            btnOpenFull = new Button
            {
                Text = "📋 Open Full Interface",
                Location = new Point(10, 320),
                Size = new Size(FormWidth - 30, 30)
            };
            btnOpenFull.Click += (s, e) => OpenMainFormRequested?.Invoke(this, EventArgs.Empty);
            this.Controls.Add(btnOpenFull);

            // Settings Button
            btnSettings = new Button
            {
                Text = "⚙️ Settings",
                Location = new Point(10, 355),
                Size = new Size((FormWidth - 40) / 2, 30)
            };
            btnSettings.Click += (s, e) => SettingsRequested?.Invoke(this, EventArgs.Empty);
            this.Controls.Add(btnSettings);

            // Exit Button
            btnExit = new Button
            {
                Text = "❌ Exit",
                Location = new Point((FormWidth - 30) / 2 + 15, 355),
                Size = new Size((FormWidth - 40) / 2, 30)
            };
            btnExit.Click += (s, e) => Application.Exit();
            this.Controls.Add(btnExit);
        }

        private void LoadQuickSettings()
        {
            try
            {
                var adapters = NetworkManager.GetNetworkAdapters();
                cmbQuickAdapter.Items.Clear();
                cmbQuickAdapter.Items.AddRange(adapters.ToArray());
                if (cmbQuickAdapter.Items.Count > 0)
                    cmbQuickAdapter.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"Error: {ex.Message}";
                lblStatus.ForeColor = Color.Red;
            }
        }

        private void LoadPresetButtons()
        {
            try
            {
                pnlPresets.Controls.Clear();
                var presets = PresetManager.LoadPresets();
                
                int yPos = 0;
                foreach (var preset in presets.Take(4)) // Show only first 4 presets
                {
                    var btnPreset = new Button
                    {
                        Text = $"▶ {preset.Name}",
                        Location = new Point(0, yPos),
                        Size = new Size(pnlPresets.Width - 5, 25),
                        Tag = preset,
                        TextAlign = ContentAlignment.MiddleLeft
                    };
                    btnPreset.Click += BtnPreset_Click;
                    pnlPresets.Controls.Add(btnPreset);
                    yPos += 28;
                }

                if (presets.Count == 0)
                {
                    var lblNoPresets = new Label
                    {
                        Text = "No presets saved",
                        Location = new Point(0, 10),
                        Size = new Size(pnlPresets.Width - 5, 20),
                        TextAlign = ContentAlignment.MiddleCenter,
                        ForeColor = Color.Gray
                    };
                    pnlPresets.Controls.Add(lblNoPresets);
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"Error loading presets: {ex.Message}";
                lblStatus.ForeColor = Color.Red;
            }
        }

        private void BtnQuickDhcp_Click(object? sender, EventArgs e)
        {
            if (cmbQuickAdapter.SelectedItem == null)
            {
                lblStatus.Text = "Please select an adapter";
                lblStatus.ForeColor = Color.Orange;
                return;
            }

            try
            {
                var config = new NetworkConfig
                {
                    AdapterName = cmbQuickAdapter.SelectedItem.ToString()!,
                    IpVersion = "IPv4",
                    UseDhcp = true
                };

                NetworkManager.ApplyConfig(config);
                lblStatus.Text = "✓ DHCP enabled successfully";
                lblStatus.ForeColor = Color.Green;

                // Reset status after 3 seconds
                var timer = new System.Windows.Forms.Timer { Interval = 3000 };
                timer.Tick += (s, args) =>
                {
                    lblStatus.Text = "Ready";
                    lblStatus.ForeColor = Color.Gray;
                    timer.Stop();
                    timer.Dispose();
                };
                timer.Start();
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"Error: {ex.Message}";
                lblStatus.ForeColor = Color.Red;
            }
        }

        private void BtnPreset_Click(object? sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is NetworkConfig preset)
            {
                try
                {
                    NetworkManager.ApplyConfig(preset);
                    lblStatus.Text = $"✓ {preset.Name} applied";
                    lblStatus.ForeColor = Color.Green;

                    // Reset status after 3 seconds
                    var timer = new System.Windows.Forms.Timer { Interval = 3000 };
                    timer.Tick += (s, args) =>
                    {
                        lblStatus.Text = "Ready";
                        lblStatus.ForeColor = Color.Gray;
                        timer.Stop();
                        timer.Dispose();
                    };
                    timer.Start();
                }
                catch (Exception ex)
                {
                    lblStatus.Text = $"Error: {ex.Message}";
                    lblStatus.ForeColor = Color.Red;
                }
            }
        }

        public void RefreshData()
        {
            LoadQuickSettings();
            LoadPresetButtons();
        }

        public new void Show()
        {
            RefreshData();
            base.Show();
            this.Activate();
        }
    }
}
