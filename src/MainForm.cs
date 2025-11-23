using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace NetworkSetter
{
    public partial class MainForm : Form
    {
        private ComboBox cmbAdapters;
        private ComboBox cmbIpVersion;
        private GroupBox grpCurrentSettings;
        private Label lblCurrentIp;
        private Label lblCurrentSubnet;
        private Label lblCurrentGateway;
        private Label lblCurrentDns;
        private GroupBox grpNewSettings;
        private RadioButton rbDhcp;
        private RadioButton rbStatic;
        private TextBox txtIpAddress;
        private TextBox txtSubnetMask;
        private TextBox txtGateway;
        private TextBox txtPreferredDns;
        private TextBox txtAlternateDns;
        private Button btnApply;
        private Button btnRefresh;
        private GroupBox grpPresets;
        private ListBox lstPresets;
        private Button btnSavePreset;
        private Button btnLoadPreset;
        private Button btnDeletePreset;
        private TextBox txtPresetName;

        private MenuStrip menuStrip;

        public MainForm()
        {
            InitializeComponent();
            LoadNetworkAdapters();
            LoadPresets();
            RefreshCurrentSettings();
            
            // Apply theme
            ThemeManager.ThemeChanged += (s, e) => ThemeManager.ApplyTheme(this);
            ThemeManager.ApplyTheme(this);
        }

        private void InitializeComponent()
        {
            this.Text = "Network Setter V2 - IP Configuration Manager";
            this.Size = new Size(800, 730);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Menu Strip
            menuStrip = new MenuStrip
            {
                Dock = DockStyle.Top
            };
            this.Controls.Add(menuStrip);
            this.MainMenuStrip = menuStrip;

            // File Menu
            var fileMenu = new ToolStripMenuItem("File");
            
            var minimizeItem = new ToolStripMenuItem("Minimize to Tray");
            minimizeItem.Click += (s, e) => this.Hide();
            minimizeItem.ShortcutKeys = Keys.Control | Keys.M;
            fileMenu.DropDownItems.Add(minimizeItem);

            fileMenu.DropDownItems.Add(new ToolStripSeparator());

            var exitItem = new ToolStripMenuItem("Exit");
            exitItem.Click += (s, e) => Application.Exit();
            exitItem.ShortcutKeys = Keys.Alt | Keys.F4;
            fileMenu.DropDownItems.Add(exitItem);

            menuStrip.Items.Add(fileMenu);

            // View Menu
            var viewMenu = new ToolStripMenuItem("View");
            
            var themeMenu = new ToolStripMenuItem("Theme");
            
            var lightThemeItem = new ToolStripMenuItem("Light");
            lightThemeItem.Click += (s, e) => ChangeTheme(Theme.Light);
            themeMenu.DropDownItems.Add(lightThemeItem);

            var darkThemeItem = new ToolStripMenuItem("Dark");
            darkThemeItem.Click += (s, e) => ChangeTheme(Theme.Dark);
            themeMenu.DropDownItems.Add(darkThemeItem);

            var systemThemeItem = new ToolStripMenuItem("System Default");
            systemThemeItem.Click += (s, e) => ChangeTheme(Theme.System);
            themeMenu.DropDownItems.Add(systemThemeItem);

            viewMenu.DropDownItems.Add(themeMenu);
            menuStrip.Items.Add(viewMenu);

            // Tools Menu
            var toolsMenu = new ToolStripMenuItem("Tools");
            
            var settingsItem = new ToolStripMenuItem("Settings");
            settingsItem.Click += (s, e) => ShowSettings();
            settingsItem.ShortcutKeys = Keys.Control | Keys.S;
            toolsMenu.DropDownItems.Add(settingsItem);

            menuStrip.Items.Add(toolsMenu);

            // Help Menu
            var helpMenu = new ToolStripMenuItem("Help");
            
            var aboutItem = new ToolStripMenuItem("About");
            aboutItem.Click += (s, e) => ShowAbout();
            helpMenu.DropDownItems.Add(aboutItem);

            menuStrip.Items.Add(helpMenu);

            // Top Panel - Adapter Selection
            var pnlTop = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                Padding = new Padding(10),
                Top = menuStrip.Height
            };
            this.Controls.Add(pnlTop);

            var lblAdapter = new Label
            {
                Text = "Network Adapter:",
                Location = new Point(10, 10),
                AutoSize = true
            };
            pnlTop.Controls.Add(lblAdapter);

            cmbAdapters = new ComboBox
            {
                Location = new Point(120, 8),
                Width = 300,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbAdapters.SelectedIndexChanged += (s, e) => RefreshCurrentSettings();
            pnlTop.Controls.Add(cmbAdapters);

            var lblIpVersion = new Label
            {
                Text = "IP Version:",
                Location = new Point(440, 10),
                AutoSize = true
            };
            pnlTop.Controls.Add(lblIpVersion);

            cmbIpVersion = new ComboBox
            {
                Location = new Point(520, 8),
                Width = 100,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbIpVersion.Items.AddRange(new object[] { "IPv4", "IPv6" });
            cmbIpVersion.SelectedIndex = 0;
            cmbIpVersion.SelectedIndexChanged += (s, e) => RefreshCurrentSettings();
            pnlTop.Controls.Add(cmbIpVersion);

            btnRefresh = new Button
            {
                Text = "Refresh",
                Location = new Point(640, 7),
                Size = new Size(120, 25)
            };
            btnRefresh.Click += BtnRefresh_Click;
            pnlTop.Controls.Add(btnRefresh);

            // Current Settings Group
            grpCurrentSettings = new GroupBox
            {
                Text = "Current Network Settings",
                Location = new Point(10, 70),
                Size = new Size(760, 140),
                Padding = new Padding(10)
            };
            this.Controls.Add(grpCurrentSettings);

            lblCurrentIp = new Label
            {
                Location = new Point(15, 25),
                Size = new Size(730, 20),
                Text = "IP Address: Loading..."
            };
            grpCurrentSettings.Controls.Add(lblCurrentIp);

            lblCurrentSubnet = new Label
            {
                Location = new Point(15, 50),
                Size = new Size(730, 20),
                Text = "Subnet Mask: Loading..."
            };
            grpCurrentSettings.Controls.Add(lblCurrentSubnet);

            lblCurrentGateway = new Label
            {
                Location = new Point(15, 75),
                Size = new Size(730, 20),
                Text = "Gateway: Loading..."
            };
            grpCurrentSettings.Controls.Add(lblCurrentGateway);

            lblCurrentDns = new Label
            {
                Location = new Point(15, 100),
                Size = new Size(730, 30),
                Text = "DNS Servers: Loading..."
            };
            grpCurrentSettings.Controls.Add(lblCurrentDns);

            // New Settings Group
            grpNewSettings = new GroupBox
            {
                Text = "Configure Network Settings",
                Location = new Point(10, 220),
                Size = new Size(760, 200),
                Padding = new Padding(10)
            };
            this.Controls.Add(grpNewSettings);

            rbDhcp = new RadioButton
            {
                Text = "Obtain an IP address automatically (DHCP)",
                Location = new Point(15, 25),
                Size = new Size(350, 20),
                Checked = true
            };
            rbDhcp.CheckedChanged += RbDhcp_CheckedChanged;
            grpNewSettings.Controls.Add(rbDhcp);

            rbStatic = new RadioButton
            {
                Text = "Use the following IP address:",
                Location = new Point(15, 50),
                Size = new Size(250, 20)
            };
            grpNewSettings.Controls.Add(rbStatic);

            var lblIp = new Label { Text = "IP Address:", Location = new Point(30, 80), AutoSize = true };
            grpNewSettings.Controls.Add(lblIp);
            txtIpAddress = new TextBox { Location = new Point(150, 77), Width = 200, Enabled = false };
            grpNewSettings.Controls.Add(txtIpAddress);

            var lblSubnet = new Label { Text = "Subnet Mask:", Location = new Point(30, 110), AutoSize = true };
            grpNewSettings.Controls.Add(lblSubnet);
            txtSubnetMask = new TextBox { Location = new Point(150, 107), Width = 200, Enabled = false };
            grpNewSettings.Controls.Add(txtSubnetMask);

            var lblGateway = new Label { Text = "Default Gateway:", Location = new Point(30, 140), AutoSize = true };
            grpNewSettings.Controls.Add(lblGateway);
            txtGateway = new TextBox { Location = new Point(150, 137), Width = 200, Enabled = false };
            grpNewSettings.Controls.Add(txtGateway);

            var lblPrefDns = new Label { Text = "Preferred DNS:", Location = new Point(400, 80), AutoSize = true };
            grpNewSettings.Controls.Add(lblPrefDns);
            txtPreferredDns = new TextBox { Location = new Point(520, 77), Width = 200, Enabled = false };
            grpNewSettings.Controls.Add(txtPreferredDns);

            var lblAltDns = new Label { Text = "Alternate DNS:", Location = new Point(400, 110), AutoSize = true };
            grpNewSettings.Controls.Add(lblAltDns);
            txtAlternateDns = new TextBox { Location = new Point(520, 107), Width = 200, Enabled = false };
            grpNewSettings.Controls.Add(txtAlternateDns);

            btnApply = new Button
            {
                Text = "Apply Settings",
                Location = new Point(580, 165),
                Size = new Size(140, 30),
                Font = new Font(this.Font, FontStyle.Bold)
            };
            btnApply.Click += BtnApply_Click;
            grpNewSettings.Controls.Add(btnApply);

            // Presets Group
            grpPresets = new GroupBox
            {
                Text = "Network Presets",
                Location = new Point(10, 430),
                Size = new Size(760, 220),
                Padding = new Padding(10)
            };
            this.Controls.Add(grpPresets);

            var lblPresets = new Label
            {
                Text = "Saved Presets:",
                Location = new Point(15, 25),
                AutoSize = true
            };
            grpPresets.Controls.Add(lblPresets);

            lstPresets = new ListBox
            {
                Location = new Point(15, 50),
                Size = new Size(500, 110)
            };
            lstPresets.DoubleClick += (s, e) => BtnLoadPreset_Click(s, e);
            grpPresets.Controls.Add(lstPresets);

            btnLoadPreset = new Button
            {
                Text = "Apply Preset",
                Location = new Point(530, 50),
                Size = new Size(200, 30)
            };
            btnLoadPreset.Click += BtnLoadPreset_Click;
            grpPresets.Controls.Add(btnLoadPreset);

            btnDeletePreset = new Button
            {
                Text = "Delete Preset",
                Location = new Point(530, 90),
                Size = new Size(200, 30)
            };
            btnDeletePreset.Click += BtnDeletePreset_Click;
            grpPresets.Controls.Add(btnDeletePreset);

            var lblPresetName = new Label
            {
                Text = "Preset Name:",
                Location = new Point(15, 175),
                AutoSize = true
            };
            grpPresets.Controls.Add(lblPresetName);

            txtPresetName = new TextBox
            {
                Location = new Point(110, 172),
                Width = 405
            };
            grpPresets.Controls.Add(txtPresetName);

            btnSavePreset = new Button
            {
                Text = "Save Current as Preset",
                Location = new Point(530, 170),
                Size = new Size(200, 30)
            };
            btnSavePreset.Click += BtnSavePreset_Click;
            grpPresets.Controls.Add(btnSavePreset);
        }

        private void LoadNetworkAdapters()
        {
            try
            {
                var adapters = NetworkManager.GetNetworkAdapters();
                cmbAdapters.Items.Clear();
                cmbAdapters.Items.AddRange(adapters.ToArray());
                if (cmbAdapters.Items.Count > 0)
                    cmbAdapters.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading adapters: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPresets()
        {
            try
            {
                var presets = PresetManager.LoadPresets();
                lstPresets.Items.Clear();
                foreach (var preset in presets)
                {
                    lstPresets.Items.Add($"{preset.Name} ({preset.IpVersion} - {preset.IpAddress})");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading presets: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshCurrentSettings()
        {
            if (cmbAdapters.SelectedItem == null)
                return;

            try
            {
                var adapter = cmbAdapters.SelectedItem.ToString();
                var ipVersion = cmbIpVersion.SelectedItem?.ToString() ?? "IPv4";
                var config = NetworkManager.GetCurrentConfig(adapter!, ipVersion);

                lblCurrentIp.Text = $"IP Address: {(string.IsNullOrEmpty(config.IpAddress) ? "Not configured" : config.IpAddress)}";
                lblCurrentSubnet.Text = $"Subnet Mask: {(string.IsNullOrEmpty(config.SubnetMask) ? "Not configured" : config.SubnetMask)}";
                lblCurrentGateway.Text = $"Gateway: {(string.IsNullOrEmpty(config.Gateway) ? "Not configured" : config.Gateway)}";
                
                var dnsInfo = string.IsNullOrEmpty(config.PreferredDns) ? "Not configured" : config.PreferredDns;
                if (!string.IsNullOrEmpty(config.AlternateDns))
                    dnsInfo += $", {config.AlternateDns}";
                lblCurrentDns.Text = $"DNS Servers: {dnsInfo}";

                if (config.UseDhcp)
                {
                    rbDhcp.Checked = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing settings: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RbDhcp_CheckedChanged(object? sender, EventArgs e)
        {
            bool isStatic = !rbDhcp.Checked;
            txtIpAddress.Enabled = isStatic;
            txtSubnetMask.Enabled = isStatic;
            txtGateway.Enabled = isStatic;
            txtPreferredDns.Enabled = isStatic;
            txtAlternateDns.Enabled = isStatic;
        }

        private void BtnRefresh_Click(object? sender, EventArgs e)
        {
            LoadNetworkAdapters();
            RefreshCurrentSettings();
            MessageBox.Show("Network settings refreshed successfully!", "Success", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnApply_Click(object? sender, EventArgs e)
        {
            if (cmbAdapters.SelectedItem == null)
            {
                MessageBox.Show("Please select a network adapter.", "Warning", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var config = new NetworkConfig
                {
                    AdapterName = cmbAdapters.SelectedItem.ToString()!,
                    IpVersion = cmbIpVersion.SelectedItem?.ToString() ?? "IPv4",
                    UseDhcp = rbDhcp.Checked,
                    IpAddress = txtIpAddress.Text.Trim(),
                    SubnetMask = txtSubnetMask.Text.Trim(),
                    Gateway = txtGateway.Text.Trim(),
                    PreferredDns = txtPreferredDns.Text.Trim(),
                    AlternateDns = txtAlternateDns.Text.Trim()
                };

                if (!config.UseDhcp && string.IsNullOrEmpty(config.IpAddress))
                {
                    MessageBox.Show("Please enter an IP address.", "Warning", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var result = MessageBox.Show(
                    $"Are you sure you want to apply these settings to {config.AdapterName}?\n\n" +
                    $"IP Version: {config.IpVersion}\n" +
                    $"Mode: {(config.UseDhcp ? "DHCP" : "Static")}\n" +
                    (config.UseDhcp ? "" : $"IP Address: {config.IpAddress}\n" +
                    $"Subnet: {config.SubnetMask}\n" +
                    $"Gateway: {config.Gateway}\n" +
                    $"DNS: {config.PreferredDns}"),
                    "Confirm Changes",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    NetworkManager.ApplyConfig(config);
                    MessageBox.Show("Network settings applied successfully!\n\nNote: It may take a few seconds for the changes to take effect.", 
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    System.Threading.Thread.Sleep(2000);
                    RefreshCurrentSettings();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error applying settings: {ex.Message}\n\nMake sure the application is running as Administrator.", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSavePreset_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPresetName.Text))
            {
                MessageBox.Show("Please enter a preset name.", "Warning", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbAdapters.SelectedItem == null)
            {
                MessageBox.Show("Please select a network adapter.", "Warning", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var config = new NetworkConfig
                {
                    Name = txtPresetName.Text.Trim(),
                    AdapterName = cmbAdapters.SelectedItem.ToString()!,
                    IpVersion = cmbIpVersion.SelectedItem?.ToString() ?? "IPv4",
                    UseDhcp = rbDhcp.Checked,
                    IpAddress = txtIpAddress.Text.Trim(),
                    SubnetMask = txtSubnetMask.Text.Trim(),
                    Gateway = txtGateway.Text.Trim(),
                    PreferredDns = txtPreferredDns.Text.Trim(),
                    AlternateDns = txtAlternateDns.Text.Trim()
                };

                PresetManager.AddPreset(config);
                LoadPresets();
                txtPresetName.Clear();
                
                MessageBox.Show($"Preset '{config.Name}' saved successfully!", "Success", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving preset: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLoadPreset_Click(object? sender, EventArgs e)
        {
            if (lstPresets.SelectedItem == null)
            {
                MessageBox.Show("Please select a preset to load.", "Warning", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var selectedText = lstPresets.SelectedItem.ToString()!;
                var presetName = selectedText.Substring(0, selectedText.IndexOf(" ("));
                var preset = PresetManager.GetPreset(presetName);

                if (preset == null)
                {
                    MessageBox.Show("Preset not found.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Set adapter and IP version
                for (int i = 0; i < cmbAdapters.Items.Count; i++)
                {
                    if (cmbAdapters.Items[i].ToString() == preset.AdapterName)
                    {
                        cmbAdapters.SelectedIndex = i;
                        break;
                    }
                }

                cmbIpVersion.SelectedItem = preset.IpVersion;

                // Set configuration
                if (preset.UseDhcp)
                {
                    rbDhcp.Checked = true;
                }
                else
                {
                    rbStatic.Checked = true;
                    txtIpAddress.Text = preset.IpAddress;
                    txtSubnetMask.Text = preset.SubnetMask;
                    txtGateway.Text = preset.Gateway;
                    txtPreferredDns.Text = preset.PreferredDns;
                    txtAlternateDns.Text = preset.AlternateDns;
                }

                var result = MessageBox.Show(
                    $"Apply preset '{preset.Name}'?\n\n" +
                    $"Adapter: {preset.AdapterName}\n" +
                    $"IP Version: {preset.IpVersion}\n" +
                    $"Mode: {(preset.UseDhcp ? "DHCP" : "Static")}\n" +
                    (preset.UseDhcp ? "" : $"IP: {preset.IpAddress}\n" +
                    $"Subnet: {preset.SubnetMask}\n" +
                    $"Gateway: {preset.Gateway}\n" +
                    $"DNS: {preset.PreferredDns}"),
                    "Apply Preset",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    NetworkManager.ApplyConfig(preset);
                    MessageBox.Show($"Preset '{preset.Name}' applied successfully!\n\nNote: It may take a few seconds for the changes to take effect.", 
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    System.Threading.Thread.Sleep(2000);
                    RefreshCurrentSettings();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading preset: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDeletePreset_Click(object? sender, EventArgs e)
        {
            if (lstPresets.SelectedItem == null)
            {
                MessageBox.Show("Please select a preset to delete.", "Warning", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var selectedText = lstPresets.SelectedItem.ToString()!;
                var presetName = selectedText.Substring(0, selectedText.IndexOf(" ("));

                var result = MessageBox.Show(
                    $"Are you sure you want to delete preset '{presetName}'?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    PresetManager.DeletePreset(presetName);
                    LoadPresets();
                    MessageBox.Show($"Preset '{presetName}' deleted successfully!", "Success", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting preset: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChangeTheme(Theme theme)
        {
            ThemeManager.CurrentTheme = theme;
            var settings = SettingsManager.LoadSettings();
            settings.Theme = theme;
            SettingsManager.SaveSettings(settings);
        }

        private void ShowSettings()
        {
            var settingsForm = new SettingsForm();
            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
                // Refresh if needed
                ThemeManager.ApplyTheme(this);
            }
        }

        private void ShowAbout()
        {
            var aboutMessage = "Network Setter V2\n" +
                             "IP Configuration Manager\n\n" +
                             "Version 2.0\n\n" +
                             "Features:\n" +
                             "• Background operation with system tray\n" +
                             "• Run at startup\n" +
                             "• Light and Dark themes\n" +
                             "• Quick access popup\n" +
                             "• Network presets\n" +
                             "• IPv4 and IPv6 support\n\n" +
                             "© 2025 Network Setter";

            MessageBox.Show(aboutMessage, "About Network Setter V2", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
