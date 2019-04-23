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

    public partial class SF_Error : Form
    {
        //Defining private properties.
        private string Title;
        private string ErrorText;
        public bool call = false;
        SF.SF_ErrorCallback callback;

        public SF_Error(string title_, string errorText_, SF.SF_ErrorCallback call_=null)
        {
            //Casting to properties.
            //args[0] - Title of Form
            //args[1] - Error Text
            //args[2] - Callback (optional)
            Title = title_;
            ErrorText = errorText_;

            //Checking for callback.
            if (call_!=null)
            {
                call = true;
                callback = call_;
            }
            InitializeComponent();

            //Showing self.
            this.Show();
        }

        private void SF_Error_Load(object sender, EventArgs e)
        {
            //Setting parameters.
            this.Text = Title;
            errorTxt.Text = ErrorText;

            //Setting form height to conform to text box.
            this.Height = errorTxt.Height + 90;
            this.Width = errorTxt.Width + 50;

            //Creating submit button with callback if enabled.
            Button submit = new Button();
            submit.AutoSize = true;
            submit.Location = new Point(this.Width - 105, this.Height - 70);
            submit.Click += handleCallback;
            submit.Text = "OK";
            this.Controls.Add(submit);
        }

        private void handleCallback(object sender, EventArgs e)
        {
            //Checking if callbacks enabled.
            if (call)
            {
                //Yes, hide, callback and dispose.
                this.Hide();
                callback();
                this.Dispose();
            } else
            {
                //Hide and dispose.
                this.Hide();
                this.Dispose();
            }
        }
    }
}
