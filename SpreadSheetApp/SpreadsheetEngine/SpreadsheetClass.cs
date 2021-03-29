// <copyright file="SpreadsheetClass.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// Name: George Eralil
// ID: 11588978

namespace Cpts321
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using static System.Exception;

    /// <summary>
    /// Spreadsheet class which stores a 2D array of cells
    /// that represents the entire spreadsheet application.
    /// Contains events to trigger property changes and getters for max rows and max columns.
    /// </summary>
    public class SpreadsheetClass
    {
        /// <summary>
        /// Maximum rows.
        /// </summary>
        private readonly int maxRow;

        /// <summary>
        /// Maximum columns.
        /// </summary>
        private readonly int maxColumn;

        /// <summary>
        /// 2D array of cells, which is a list of cells.
        /// </summary>
        private List<Cell[]> cells = new List<Cell[]>();

        /// <summary>
        /// Field denoting the stack of commands for the undo function.
        /// </summary>
        private Stack<ICommand> undoCommands = new Stack<ICommand>();

        /// <summary>
        /// Field denoting the stack of commands for the redo function.
        /// </summary>
        private Stack<ICommand> redoCommands = new Stack<ICommand>();

        /// <summary>
        /// Initializes a new instance of the <see cref="SpreadsheetClass"/> class.
        /// Constructor for spreadsheet class.
        /// Initializes the 2D array of cells and max rows and columns.
        /// Subscribes the cell_property Changed event to the property changed event from cellClass.
        /// </summary>
        /// <param name="maxRows"> Maximum number of rows.</param>
        /// <param name="maxColumns"> Maximum number of columns.</param>
        public SpreadsheetClass(int maxRows, int maxColumns)
        {
            this.maxRow = maxRows;
            this.maxColumn = maxColumns;

            for (int i = 0; i < this.maxRow; i++)
            {
                // Add row of cells to the list.//
                this.cells.Add(new Cell[this.maxColumn]);

                for (int j = 0; j < this.maxColumn; j++)
                {
                    this.cells[i][j] = new Cell(i, j);  // initializing each cell with rowIndex and colIndex.//
                    this.cells[i][j].PropertyChanged += this.Cell_PropertyChanged;  // subscribing to property change event.//
                }
            }
        }

        /// <summary>
        /// Property changed event for outside classes to subscribe to,
        /// to be notified of any changes to spreadsheet array.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        /// <summary>
        /// Cell property changed event that gets notified every time a cell data is edited.
        /// Based on the change made, performs certain functions.
        /// </summary>
        /// <param name="sender"> Sender cell.</param>
        /// <param name="e"> EventArgs for more information.</param>
        public void Cell_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Cell cell = sender as Cell; // initializing new cell as sender cell.//
            bool badReference = false;
            bool selfReference = false;

            if (e.PropertyName == "Text")
            {
                string textValue = cell.Text;

                if (textValue.Length != 0)
                {
                    if (textValue[0] != '=')
                    {
                        cell.Value = cell.Text;
                        this.PropertyChanged(this, new PropertyChangedEventArgs(e.PropertyName + "," + cell.GetRowIndexValue().ToString() + "," + cell.GetColumnIndexValue().ToString() + "," + cell.Value));
                    }
                    else if (textValue[0] == '=')
                    {
                        cell.InitializeTree(textValue.Substring(1));

                        List<string> variables = cell.VariableNames();

                        foreach (var item in variables)
                        {
                            if (this.GetCellFromIndex(item) == cell)
                            {
                                selfReference = true;
                            }
                            else if (this.GetCellFromIndex(item) == null)
                            {
                                badReference = true;
                            }
                            else
                            {
                                cell.SubscribeCellToTree(this.GetCellFromIndex(item));
                            }
                        }

                        if (badReference == true)
                        {
                            cell.Value = "!(bad reference)";
                        }
                        else if (selfReference == true)
                        {
                            cell.Value = "!(self reference)";
                        }
                        else if (this.CircularReferenceChecker(cell, new List<CellClass>()))
                        {
                            cell.Value = "!(circular reference)";
                        }
                        else
                        {
                            double value = cell.EvaluateExpression();

                            cell.Value = value.ToString();
                        }

                        // trigger property changed event in spreadsheet class so outside listeners can be notified.//
                        this.PropertyChanged(this, new PropertyChangedEventArgs(e.PropertyName + "," + cell.GetRowIndexValue().ToString() + "," + cell.GetColumnIndexValue().ToString() + "," + cell.Value));
                    }
                }
                else
                {
                    cell.Value = cell.Text;
                }
            }

            if (e.PropertyName == "Value")
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(e.PropertyName + "," + cell.GetRowIndexValue().ToString() + "," + cell.GetColumnIndexValue().ToString() + "," + cell.Value)); // Send information to form to update dataviewgrid.
                return;
            }

            if (e.PropertyName == "BGcolor")
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(e.PropertyName + "," + cell.GetRowIndexValue().ToString() + "," + cell.GetColumnIndexValue().ToString() + "," + cell.BGcolor.ToString()));
                return;
            }
        }

        /// <summary>
        /// Method to get a particular cell
        /// given the rowIndex and columnIndex values.
        /// </summary>
        /// <param name="rowIndexValue"> row index.</param>
        /// <param name="columnIndexValue"> column index.</param>
        /// <returns> Object of abstract cell class.</returns>
        public CellClass GetCell(int rowIndexValue, int columnIndexValue)
        {
            // in our list of cells, index values start from 0, so decrement passed params by 1//
            rowIndexValue--;
            columnIndexValue--;

            // checking if within range.//
            if (rowIndexValue >= this.maxRow || columnIndexValue >= this.maxColumn)
            {
                return null;
            }
            else if (rowIndexValue < 0 || columnIndexValue < 0)
            {
                return null;
            }

            // since within range.//
            return this.cells[rowIndexValue][columnIndexValue];
        }

        /// <summary>
        /// Function to get the cell from the index number of the cell in UI.
        /// </summary>
        /// <param name="name"> name storing the cell index.</param>
        /// <returns> an object of cell class.</returns>
        public CellClass GetCellFromIndex(string name)
        {
            int rowIndexValue = -1, columnIndexValue = -1;

            columnIndexValue = name[0] - 65;

            try
            {
                rowIndexValue = int.Parse(name.Substring(1)) - 1;
            }
            catch (FormatException)
            {
                Console.WriteLine("Variable value not supported.");
                return null;
            }

            if (rowIndexValue > this.maxRow || columnIndexValue > this.maxColumn)
            {
                return null;
            }

            return this.cells[rowIndexValue][columnIndexValue];
        }

        /// <summary>
        /// Getter function for max columns.
        /// </summary>
        /// <returns> max columns.</returns>
        public int ColumnCount()
        {
            return this.maxColumn;
        }

        /// <summary>
        /// Getter function for max rows.
        /// </summary>
        /// <returns> max rows.</returns>
        public int RowCount()
        {
            return this.maxRow;
        }

        /// <summary>
        /// Redo function that implements the redo functionality to the spreadsheet class.
        /// Function pops from the redo stack, executes the command and then pushes it onto the undo stack.
        /// </summary>
        public void Redo()
        {
            if (this.redoCommands.Count != 0)
            {
                ICommand command = this.redoCommands.Pop();
                command.Execute();
                this.undoCommands.Push(command);
            }
        }

        /// <summary>
        /// Undo function that implements the undo functionality to the spreadsheet class.
        /// Function pops from the undo stack, un-executes it and then pushes it onto the redo stack.
        /// </summary>
        public void Undo()
        {
            if (this.undoCommands.Count != 0)
            {
                ICommand command = this.undoCommands.Pop();
                command.UnExecute();
                this.redoCommands.Push(command);
            }
        }

        /// <summary>
        /// Function to insert the text change to cell onto the undo redo stack.
        /// </summary>
        /// <param name="cell"> Cell that is to be changed.</param>
        /// <param name="newText"> string denoting new text of the cell.</param>
        public void InsertInUndoRedoForTextChange(CellClass cell, string newText)
        {
            ICommand command = new TextChangeCommand(cell, newText);
            this.undoCommands.Push(command);
            this.redoCommands.Clear();
        }

        /// <summary>
        /// Function to insert the background color change to cell onto the undo redo stack.
        /// </summary>
        /// <param name="cell"> Cell that is to be changed.</param>
        /// <param name="newColor"> integer denoting the new color of the cell.</param>
        public void InsertInUndoRedoForColorChange(CellClass cell, uint newColor)
        {
            ICommand command = new ColorChangeCommand(cell, newColor);
            this.undoCommands.Push(command);
            this.redoCommands.Clear();
        }

        /// <summary>
        /// Function to return the count of the undo stack.
        /// </summary>
        /// <returns> integer denoting size of stack.</returns>
        public int UndoStackCount()
        {
            return this.undoCommands.Count;
        }

        /// <summary>
        /// Function to return the count of the redo stack.
        /// </summary>
        /// <returns> integer denoting size of stack.</returns>
        public int RedoStackCount()
        {
            return this.redoCommands.Count;
        }

        /// <summary>
        /// Function to peek at the top most command on the undo stack.
        /// </summary>
        /// <returns> Command object.</returns>
        public ICommand GetTopUndoCommand()
        {
            return this.undoCommands.Peek();
        }

        /// <summary>
        /// Function to peek at the top most command on the redo stack.
        /// </summary>
        /// <returns> Command object.</returns>
        public ICommand GetTopRedoCommand()
        {
            return this.redoCommands.Peek();
        }

        /// <summary>
        /// Function to save spreadsheet contents to xml file.
        /// </summary>
        /// <param name="writer"> Xml stream writer.</param>
        public void SaveToXml(XmlWriter writer)
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("spreadsheet");

            for (int i = 0; i < this.maxRow; i++)
            {
                for (int j = 0; j < this.maxColumn; j++)
                {
                    if (this.cells[i][j].PropertyChangedValue == true)
                    {
                        this.cells[i][j].WriteXml(writer);
                    }
                }
            }

            writer.WriteEndElement();
        }

        /// <summary>
        /// Function to load spreadsheet contents to xml file.
        /// </summary>
        /// <param name="reader"> Xml reader stream.</param>
        public void LoadFromXml(XmlReader reader)
        {
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "cell")
                {
                    string cellIndex = reader.GetAttribute("name");

                    this.GetCellFromIndex(cellIndex).ReadXml(reader);
                }
            }
        }

        private bool CircularReferenceChecker(CellClass cell, List<CellClass> cells)
        {
            if (cell.Text == string.Empty)
            {
                return false;
            }

            if (cell.Text[0] != '=')
            {
                return false;
            }

            ExpressionTree expression = new ExpressionTree(cell.Text.Substring(1));
            List<string> variableList = expression.GetVariableNames();

            if (cells.Contains(cell))
            {
                return true;
            }

            cells.Add(cell);

            List<CellClass> cellList = new List<CellClass>();

            foreach (string variable in variableList)
            {
                cellList.Add(this.GetCellFromIndex(variable));
            }

            foreach (CellClass cellClass in cellList)
            {
                if (cellClass != null)
                {
                    if (cellClass.Text != " " || cellClass.Text != null)
                    {
                        var newCellList = new List<CellClass>(cells);

                        if (this.CircularReferenceChecker(cellClass, newCellList))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
