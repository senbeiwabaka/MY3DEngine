﻿// <copyright file="ExceptionValueDisplayForm.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Windows.Forms;

namespace MY3DEngine.GUI
{
    public partial class ExceptionValueDisplayForm : Form
    {
        public ExceptionValueDisplayForm()
        {
            InitializeComponent();
        }

        public ExceptionValueDisplayForm(object content)
            : this()
        {
            this.CellMessageContent.Text = content.ToString();
        }
    }
}