// CustomMessageBoxForm.cs
using System;
using System.Drawing;
using System.Linq; // Added for LINQ
using System.Windows.Forms;
using AudioConverter.Helpers; // Assuming ImageUtils is here
using AudioConverter; // Assuming LocalizationManager is here

namespace AudioConverter
{
    public partial class CustomMessageBoxForm : Form
    {
        public CustomMessageBoxForm()
        {
            InitializeComponent();
            SetupAppearance();
            this.Load += CustomMessageBoxForm_Load;

            // Load the icon from the constructor, just like the working AboutBoxForm.
            try
            {
                var assembly = System.Reflection.Assembly.GetExecutingAssembly();
                const string resourceName = "AudioConverter.images.icon.ico";

                using Stream? stream = assembly.GetManifestResourceStream(resourceName);
                if (stream != null)
                {
                    this.Icon = new Icon(stream);
                }
                else
                {
                    Logger.Log($"Embedded icon not found for MessageBox: {resourceName}", true);
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"Error loading MessageBox icon: {ex.Message}", true);
            }
        }

        private void CustomMessageBoxForm_Load(object? sender, EventArgs e)
        {
            // LoadImages(); // No specific images for message box buttons yet.
        }

        private void SetupAppearance()
        {
            this.BackColor = System.Drawing.ColorTranslator.FromHtml("#023047");
            this.ForeColor = Color.White;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ControlBox = true; // Show the control box (title bar with icon and close button)

            // Apply font to messageLabel
            this.messageLabel.Font = new Font("Comic Sans MS", 14F, FontStyle.Bold); // Increased font size and made it bold
            this.messageLabel.ForeColor = Color.White;
            
            LocalizationManager.ApplyLocalization(this); // Moved here
        }

        // Overload for calls without an owner (e.g., from Program.cs)
        public static DialogResult Show(string message, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            using (var msgBox = new CustomMessageBoxForm())
            {
                msgBox.Text = LocalizationManager.GetString(caption) ?? caption;
                msgBox.messageLabel.Text = LocalizationManager.GetString(message) ?? message;
                msgBox.messageLabel.Padding = new Padding(10);

                msgBox.AddButtons(buttons);

                msgBox.AutoSize = true;
                msgBox.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                
                msgBox.ClientSize = new Size(msgBox.ClientSize.Width, msgBox.messageLabel.Height + msgBox.buttonsPanel.Height + 20);

                return msgBox.ShowDialog();
            }
        }

        // Static method to show the custom message box, accepting an owner
        public static DialogResult Show(Form owner, string message, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            using (var msgBox = new CustomMessageBoxForm())
            {
                msgBox.Text = LocalizationManager.GetString(caption) ?? caption; // Localize caption
                msgBox.messageLabel.Text = LocalizationManager.GetString(message) ?? message; // Localize message
                msgBox.messageLabel.Padding = new Padding(10); // Add padding

                // Determine buttons and add them
                msgBox.AddButtons(buttons);

                // Adjust form size based on message length and button count
                msgBox.AutoSize = true;
                msgBox.AutoSizeMode = AutoSizeMode.GrowAndShrink;

                // Recalculate size after adding controls
                msgBox.ClientSize = new Size(msgBox.ClientSize.Width, msgBox.messageLabel.Height + msgBox.buttonsPanel.Height + 20);

                return msgBox.ShowDialog(owner);
            }
        }

        private void AddButtons(MessageBoxButtons buttons)
        {
            this.buttonsPanel.Controls.Clear(); // Clear any existing buttons
            
            var buttonsToAdd = new List<Button>();

            Action<DialogResult, string> createButton = (dialogResult, text) =>
            {
                var button = new NoFocusButton
                {
                    Text = LocalizationManager.GetString(text) ?? text,
                    DialogResult = dialogResult,
                    Size = new Size(100, 30),
                    Font = new Font("Comic Sans MS", 10F, FontStyle.Bold),
                    BackgroundImage = ImageUtils.CargarImagenDesdeRecurso("boton.png"),
                    BackgroundImageLayout = ImageLayout.Stretch,
                    BackColor = Color.Transparent,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat
                };
                button.FlatAppearance.BorderSize = 0;
                button.FlatAppearance.MouseOverBackColor = Color.Transparent;
                button.FlatAppearance.MouseDownBackColor = Color.Transparent;
                buttonsToAdd.Add(button);
            };

            switch (buttons)
            {
                case MessageBoxButtons.OK:
                    createButton(DialogResult.OK, "Button_OK");
                    break;
                case MessageBoxButtons.OKCancel:
                    createButton(DialogResult.OK, "Button_OK");
                    createButton(DialogResult.Cancel, "Button_Cancel");
                    break;
                case MessageBoxButtons.YesNo:
                    createButton(DialogResult.Yes, "Button_Yes");
                    createButton(DialogResult.No, "Button_No");
                    break;
                // Add other cases as needed, reversing order for right-to-left feel
            }
            
            // Manual positioning logic for centering
            int buttonMargin = 10;
            int totalButtonWidth = buttonsToAdd.Sum(b => b.Width) + (buttonsToAdd.Count - 1) * buttonMargin;
            int currentLeft = (this.buttonsPanel.ClientSize.Width - totalButtonWidth) / 2;

            foreach (var btn in buttonsToAdd)
            {
                btn.Left = currentLeft;
                this.buttonsPanel.Controls.Add(btn);
                currentLeft += btn.Width + buttonMargin;
            }
        }
    }
}

