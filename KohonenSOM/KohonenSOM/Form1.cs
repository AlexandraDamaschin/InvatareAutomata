﻿using System;
using System.Windows.Forms;

namespace KohonenSOM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region Points
        private void button_Points_Click(object sender, EventArgs e)
        {
            DrawPoints();
            InitializeNeuroni();
            DrawMap();
        }

        private void DrawMap()
        {
            throw new NotImplementedException();
        }

        private void InitializeNeuroni()
        {
            throw new NotImplementedException();
        }

        private void DrawPoints()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Kohonen
        private void button_Kohonen_Click(object sender, EventArgs e)
        {
            Kohonen();
        }

        private void Kohonen()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
