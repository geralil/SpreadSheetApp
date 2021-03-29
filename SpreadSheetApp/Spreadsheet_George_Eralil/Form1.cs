// <copyright file="Form1.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// Name: George Eralil
// ID: 11588978

namespace Spreadsheet_George_Eralil
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using System.Xml;
    using Cpts321;

    /// <summary>
    /// Partial class form implementing the interface
    /// for the spreadsheet application.
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// Spreadsheet class field.
        /// </summary>
        private SpreadsheetClass spreadSheet = new SpreadsheetClass(50, 26);

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// Form Constructor initializing the data grid view
        /// to have the desired number of max rows and max columns.
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();

            this.InitializeDataGridView();
        }

        /// <summary>
        /// function to initialize the data grid view
        /// with desired max rows and columns.
        /// </summary>
        private void InitializeDataGridView()
        {
            // Adding columns from A-Z.//
            for (char headerText = 'A'; headerText <= 'A' + 25; headerText++)
            {
                this.dataGridView1.Columns.Add(headerText.ToString(), headerText.ToString());
            }

            // Adding rows from 1-50 with header value.//
            for (int i = 1; i <= 50; i++)
            {
                this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[i - 1].HeaderCell.Value = i.ToString();
            }

            // Adding default background color.//
            this.dataGridView1.BackgroundColor = Color.White;
        }

        /// <summary>
        /// Function to load the form and subscribe the handle cell property changed event
        /// to the spreadsheet property changed event. So any changes in
        /// the spreadsheet will bubble up to the UI.
        /// </summary>
        /// <param name="sender"> Sender spreadsheet class instance.</param>
        /// <param name="e"> EventArgs for more information.</param>
        private void Form1_Load(object sender, EventArgs e)
        {
            this.spreadSheet.PropertyChanged += this.HandleCellPropertyChanged;
        }

        /// <summary>
        /// Handle cell property changed event which updates changes made
        /// to the spreadsheet in the data grid view.
        /// </summary>
        /// <param name="sender"> Sender spreadsheet class instance.</param>
        /// <param name="e"> EventArgs for more information.</param>
        private void HandleCellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // parsing the custom PropertyChangedEventArgs sent by spreadsheet class.//
            string[] cellValues = e.PropertyName.Split(',');

            if (cellValues[0] != "BGcolor")
            {
                // cellValues contains the index of cell that was changed and value of the cell.//
                this.dataGridView1.Rows[Convert.ToInt32(cellValues[1])].Cells[Convert.ToInt32(cellValues[2])].Value = cellValues[3];
            }
            else
            {
                uint bgcolor = Convert.ToUInt32(cellValues[3]);
                this.dataGridView1.Rows[Convert.ToInt32(cellValues[1])].Cells[Convert.ToInt32(cellValues[2])].Style.BackColor = Color.FromArgb((int)bgcolor);
            }
        }

        /// <summary>
        /// Function to execute the run demo button.
        /// The button generates 50 strings in random locations.
        /// Fills column B with "this is cell B#" string.
        /// assigns column B to A.
        /// </summary>
        /// <param name="sender"> Sender button.</param>
        /// <param name="e"> EventArgs for more information.</param>
        private void RunDemo_Click(object sender, EventArgs e)
        {
            Random rnd1 = new Random(); // creating variable of type random.//

            for (int i = 0; i < 50; i++)
            {
                int row, col;
                row = rnd1.Next(1, 51);  // random number between 1 and 50.//
                col = rnd1.Next(1, 27);  // random number between 1 and 26.//
                this.spreadSheet.GetCell(row, col).Text = "Bow down to Geralil!";   // random string for each random cell.//
            }

            // Loop for column B
            for (int j = 1; j <= 50; j++)
            {
                this.spreadSheet.GetCell(j, 2).Text = "This is cell B" + j.ToString();
            }

            // Loop for column A
            for (int k = 1; k <= 50; k++)
            {
                // Changing the text in spreadsheet will trigger Cell_propertyChanged event in spreadsheetClass.//
                // Event will handle assignment of one cell to another.//
                this.spreadSheet.GetCell(k, 1).Text = "=B" + k.ToString();
            }
        }

        /// <summary>
        /// Cell Begin edit method that gets triggered every time
        /// the cell is being edited.
        /// </summary>
        /// <param name="sender"> sender cell.</param>
        /// <param name="e"> event args.</param>
        private void DataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            int row, col;
            row = e.RowIndex;
            col = e.ColumnIndex;
            string message = this.spreadSheet.GetCell(++row, ++col).Text;

            if (message.Length > 0)
            {
                this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = message;
            }
        }

        /// <summary>
        /// Cell end edit method that gets triggered when the cell is
        /// finished editing.
        /// </summary>
        /// <param name="sender"> sender cell.</param>
        /// <param name="e"> event args.</param>
        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int row, col;
            row = e.RowIndex;
            col = e.ColumnIndex;
            string message = this.spreadSheet.GetCell(++row, ++col).Value;

            if (message.Length > 0)
            {
                this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = message;
            }
        }

        /// <summary>
        /// Method that is triggered whenever the cell value
        /// has been changed.
        /// </summary>
        /// <param name="sender"> sender cell.</param>
        /// <param name="e"> event args.</param>
        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int row, col;
            row = e.RowIndex;
            col = e.ColumnIndex;

            if (e.ColumnIndex != -1)
            {
                if (this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    if (this.spreadSheet.GetCell(row + 1, col + 1).Value != this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString())
                    {
                        this.spreadSheet.InsertInUndoRedoForTextChange(this.spreadSheet.GetCell(row + 1, col + 1), this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                        this.spreadSheet.GetCell(row + 1, col + 1).Text = this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    }
                }
                else
                {
                    this.spreadSheet.InsertInUndoRedoForTextChange(this.spreadSheet.GetCell(row + 1, col + 1), " ");
                    this.spreadSheet.GetCell(row + 1, col + 1).Text = " ";
                }
            }
        }

        /// <summary>
        /// Event that is triggered when the change background color
        /// button is selected.
        /// </summary>
        /// <param name="sender"> tool strip item.</param>
        /// <param name="e"> event arguments.</param>
        private void ChangeCellsBackgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedCellCount = this.dataGridView1.GetCellCount(DataGridViewElementStates.Selected);    // keeps count of the number of selected cells.//

            ColorDialog cellColorDialog = new ColorDialog();    // creates new instance of color dialog box.//

            // setting properties for the color dialog box.//
            cellColorDialog.AllowFullOpen = false;

            cellColorDialog.ShowHelp = true;

            // once user has selected ok.//
            if (cellColorDialog.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < selectedCellCount; i++)
                {
                    int row, col;
                    row = this.dataGridView1.SelectedCells[i].RowIndex;
                    col = this.dataGridView1.SelectedCells[i].ColumnIndex;

                    uint newColor = (uint)cellColorDialog.Color.ToArgb();

                    // editing logic engine instance.//
                    this.spreadSheet.InsertInUndoRedoForColorChange(this.spreadSheet.GetCell(row + 1, col + 1), newColor);
                    this.spreadSheet.GetCell(row + 1, col + 1).BGcolor = newColor;
                }
            }
        }

        /// <summary>
        /// Method that gets triggered every time the edit menu tool strip is clicked on.
        /// </summary>
        /// <param name="sender"> tool strip item.</param>
        /// <param name="e"> event arguments.</param>
        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // setting the text of the undo redo buttons.//
            this.undoToolStripMenuItem.Text = "Undo ";
            this.redoToolStripMenuItem.Text = "Redo ";

            if (this.spreadSheet.UndoStackCount() != 0)
            {
                // if undo stack is not empty, enable the button and add the top most command
                // to the text of the button.//
                this.undoToolStripMenuItem.Enabled = true;
                ICommand command = this.spreadSheet.GetTopUndoCommand();

                this.undoToolStripMenuItem.Text = "Undo " + command.GetType();
            }
            else
            {
                this.undoToolStripMenuItem.Enabled = false;
            }

            if (this.spreadSheet.RedoStackCount() != 0)
            {
                // if undo stack is not empty, enable the button and add the top most command
                // to the text of the button.//
                this.redoToolStripMenuItem.Enabled = true;
                ICommand command = this.spreadSheet.GetTopRedoCommand();

                this.redoToolStripMenuItem.Text = "Redo " + command.GetType();
            }
            else
            {
                this.redoToolStripMenuItem.Enabled = false;
            }
        }

        /// <summary>
        /// Method that gets triggered when the undo button is clicked.
        /// </summary>
        /// <param name="sender"> tool strip menu item.</param>
        /// <param name="e"> event arguments.</param>
        private void UndoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.spreadSheet.Undo();
        }

        /// <summary>
        /// Method that gets triggered when the redo button is clicked.
        /// </summary>
        /// <param name="sender"> tool strip menu item.</param>
        /// <param name="e"> event arguments.</param>
        private void RedoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.spreadSheet.Redo();
        }

        /// <summary>
        /// Method to save spreadsheet to XML file generated
        /// at specified location.
        /// </summary>
        /// <param name="sender"> tool strip menu item.</param>
        /// <param name="e"> parameter arguments.</param>
        private void SaveToXMLFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog browserDialog = new SaveFileDialog();
            browserDialog.DefaultExt = "xml";

            DialogResult result = browserDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                XmlWriter xmlWriter = null;
                string path = browserDialog.FileName;
                xmlWriter = XmlWriter.Create(path);
                this.spreadSheet.SaveToXml(xmlWriter);
                xmlWriter.Close();
            }
        }

        /// <summary>
        /// Method to load spreadsheet from XML file
        /// present at specified location.
        /// </summary>
        /// <param name="sender"> tool strip menu item.</param>
        /// <param name="e"> parameter arguments.</param>
        private void LoadXMLFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog browserDialog = new OpenFileDialog();
            browserDialog.DefaultExt = "xml";

            DialogResult result = browserDialog.ShowDialog();

            if (result == DialogResult.OK && browserDialog.CheckFileExists == true)
            {
                // resetting the datagridview and spreadsheet class.//
                this.dataGridView1.Rows.Clear();
                this.dataGridView1.Columns.Clear();
                this.spreadSheet = new SpreadsheetClass(50, 26);
                this.Form1_Load(sender, e);
                this.InitializeDataGridView();

                XmlReader xmlReader = null;

                xmlReader = XmlReader.Create(browserDialog.FileName);
                this.spreadSheet.LoadFromXml(xmlReader);
                xmlReader.Close();
            }
        }
    }
}
