﻿using MY3DEngine;
using System;
using System.IO;
using System.Windows.Forms;

namespace MY3DEngineGUI.HelperForms
{
    public delegate void ClosingSetNameForm(object sender, ClosingSetNameEventArg args);

    public class ClosingSetNameEventArg : EventArgs
    {
        public readonly string ClassName;

        public ClosingSetNameEventArg(string className)
        {
            this.ClassName = className;
        }
    }

    public partial class SetNameForm : Form
    {
        public SetNameForm()
        {
            InitializeComponent();
        }

        internal event ClosingSetNameForm ClosingSetNameForm;

        private void BCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

            this.Close();
        }

        private void BCreate_Click(object sender, EventArgs e)
        {
            File.Create($"{Engine.GameEngine.FolderLocation}\\{tbName.Text}.cs");

            this.DialogResult = DialogResult.OK;

            this.ClosingSetNameForm?.Invoke(this, new ClosingSetNameEventArg(tbName.Text));

            this.Close();
        }

        private void TbName_TextChanged(object sender, EventArgs e)
        {
            this.bCreate.Enabled = false;

            if (!string.IsNullOrWhiteSpace(this.tbName.Text))
            {
                this.bCreate.Enabled = true;
            }
        }
    }
}