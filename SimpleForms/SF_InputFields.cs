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
    public partial class SF_InputFields : Form
    {
        //Private properties for the form.
        public string Title;
        public string TitleText;
        public int NumFields = 0;
        public List<object> inputs = new List<object>();
        object[] arguments;
        List<string> inputFieldNames = new List<string>();

        public SF_InputFields(params object[] args)
        {
            //Error checking.
            if (args.Count() < 3) { throw new Exception("Missing parameters."); }

            //Setting arguments array.
            arguments = args;

            //Setting title and title text.
            Title = (string)args[0];
            TitleText = (string)args[1];

            //Getting array of all input fields.
            for (int i=2; i<args.Count(); i++)
            {
                inputFieldNames.Add((string)args[i]);
            }
            Console.WriteLine(inputFieldNames.Count);
            InitializeComponent();
        }

        private void SF_InputFields_Load(object sender, EventArgs e)
        {
            //Setting window height, width, name.
            this.Width = 400;
            this.Height = 30 + (80 * inputFieldNames.Count);
            this.Text = Title;

            //Adding title text to the form.
            Label titleText = new Label();
            titleText.Location = new Point(10, 10);
            titleText.AutoSize = true;
            titleText.Text = TitleText;
            this.Controls.Add(titleText);

            //Placing labels and text boxes onto form.
            int labelHeight = 20 + titleText.Height;
            int fieldHeight = 40 + titleText.Height;
            foreach (var f in inputFieldNames)
            {
                //Creating label.
                Label fieldText = new Label();
                fieldText.Location = new Point(10, labelHeight);
                fieldText.AutoSize = true;
                fieldText.Text = f;
                fieldText.Name = f + "label";
                Console.WriteLine(fieldText.Location);
                this.Controls.Add(fieldText);

                //Creating input fields.
                TextBox fieldTBox = new TextBox();
                fieldTBox.Location = new Point(10, fieldHeight);
                fieldTBox.Name = "input"+(inputFieldNames.IndexOf(f)+1);
                fieldTBox.Size = new Size(this.Width - 38, 20);
                this.Controls.Add(fieldTBox);

                //Incrementing heights.
                labelHeight += 50;
                fieldHeight += 50;
            }

            //Creating final button.
            Button submitButton = new Button();
            submitButton.Location = new Point(10, fieldHeight-20);
            submitButton.Size = new Size(this.Width - 38, 35);
            submitButton.Text = "Submit";
            submitButton.Click += handleInputFinished;
            this.Controls.Add(submitButton);
        }

        private void handleInputFinished(object sender, EventArgs e)
        {
            //Setting public properties.
            for (int i=1; i<=inputFieldNames.Count; i++)
            {
                inputs.Add(this.GetType().GetField("input"+i).GetValue(this));
            }

            //Changing visibility.
            this.Visible = false;
        }

        public static List<object> SF_GetInputFields(object s)
        {
            //Returning fields.
            SF_InputFields send = (SF_InputFields)s;
            return send.inputs;
        }
    }
}
