using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleForms
{
    public partial class SF_Menu : Form
    {
        //Private properties for the form.
        public string Title;
        public string TitleText;
        public string ButtonPressed = "";
        public int NumButtons = 0;
        object[] arguments;

        //Constructors with optional parameters for buttons.
        //Format:
        //args[0]: Window Title
        //args[1]: Text above buttons
        //args[2-..] Buttons with text.
        public SF_Menu(params object[] args)
        {
            //Getting amount of buttons.
            if (args.Count()<2) { throw new Exception("No name or parameters included."); }
            for (int i=2; i<args.Count(); i++)
            {
                NumButtons++;
            }

            //Setting title.
            Title = (string)args[0];
            TitleText = (string)args[1];

            //Setting argument array.
            arguments = args;

            //Initializing form.
            InitializeComponent();
        }

        private void SF_Menu_Load(object sender, EventArgs e)
        {
            //Setting window title.
            this.Text = Title;
            this.AutoSize = false;

            //Setting size of the form.
            this.Width = 300;
            this.Height = 400;
        
            //Placing title text onto the form.
            Label titleText = new Label();
            titleText.Location = new Point(10, 10);
            titleText.AutoSize = true;
            titleText.Text = TitleText;
            this.Controls.Add(titleText);

            //Placing buttons onto the form, and setting up events.
            int buttonHeight = 20 + titleText.Height;
            for (int i = 0; i<NumButtons; i++)
            {
                Button currentBtn = new Button();

                //Setting location.
                currentBtn.Location = new Point(10, buttonHeight);
                currentBtn.Width = this.Width-37;
                currentBtn.Name = "btn" + (i + 1);
                currentBtn.Click += switchVis;
                buttonHeight += 30;

                //Setting text.
                currentBtn.Text = (string)arguments[2 + i];

                //Adding button to panel.
                this.Controls.Add(currentBtn);
            }

            //Readjusting height to compensate for buttons.
            this.Height = buttonHeight + 50;
        }

        private void switchVis(object sender, EventArgs e)
        {
            //Setting clicked button number.
            Button send = (Button)sender;
            ButtonPressed = send.Name;
            this.Visible = false;
        }

        public static string GetClickedButton(object sfmenu)
        {
            SF_Menu send = (SF_Menu)sfmenu;
            return send.ButtonPressed;
        }
    }
}
