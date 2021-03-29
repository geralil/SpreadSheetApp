// <copyright file="CellClass.cs" company="PlaceholderCompany">
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
    using System.Xml.Schema;
    using System.Xml.Serialization;

    /// <summary>
    /// Abstract cell class deriving from notify property changed interface.
    /// Represents the data to be stored in a single cell of the spreadsheet.
    /// </summary>
    public abstract class CellClass : INotifyPropertyChanged, IXmlSerializable
    {
        /// <summary>
        /// String storing the evaluated value of one cell.
        /// </summary>
        protected string evalValue;

        /// <summary>
        /// Row index field.
        /// </summary>
        private readonly int rowIndex;

        /// <summary>
        /// Column index field.
        /// </summary>
        private readonly int columnIndex;

        /// <summary>
        /// String storing the text values of one cell.
        /// </summary>
        private string textValue;

        /// <summary>
        /// U integer property storing the background color of cell.
        /// </summary>
        private uint backColor = 0xFFFFFFFF;

        /// <summary>
        /// Expression tree instance holding for one cell.
        /// </summary>
        private ExpressionTree expression;

        /// <summary>
        /// Initializes a new instance of the <see cref="CellClass"/> class.
        /// </summary>
        /// <param name="rowIndexValue"> row index value of one cell.</param>
        /// <param name="columnIndexValue"> column index value of one cell.</param>
        public CellClass(int rowIndexValue, int columnIndexValue)
        {
            this.rowIndex = rowIndexValue;
            this.columnIndex = columnIndexValue;
            this.textValue = string.Empty;
            this.evalValue = string.Empty;
            this.PropertyChangedValue = false;
        }

        /// <summary>
        /// Property Changed Event for outside classes to subscribe to,
        /// to be notified of any change in the data of a particular cell.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets a value indicating whether cell attributes have been changed
        /// from their default values.
        /// false - not changed.
        /// true - changed.
        /// </summary>
        public bool PropertyChangedValue { get; set; }

        /// <summary>
        /// Gets or sets text value of one cell.
        /// </summary>
        public string Text
        {
            get
            {
                return this.textValue;
            }

            set
            {
                if (value == this.textValue)
                {
                    return;
                }

                this.textValue = value;
                this.PropertyChangedValue = true;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets(not really) the evaluated Value of a cell.
        /// </summary>
        public virtual string Value
        {
            get
            {
                return this.evalValue;
            }

            set
            {
                throw new NotSupportedException("Value cannot be set here.");
            }
        }

        /// <summary>
        /// Gets or sets the Background color of cell.
        /// </summary>
        public uint BGcolor
        {
            get
            {
                return this.backColor;
            }

            set
            {
                if (value == this.backColor)
                {
                    return;
                }

                this.backColor = value;
                this.PropertyChangedValue = true;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Getter for row index value of a cell.
        /// </summary>
        /// <returns> row index value.</returns>
        public int GetRowIndexValue()
        {
            return this.rowIndex;
        }

        /// <summary>
        /// Getter for column index value of a cell.
        /// </summary>
        /// <returns> column index value.</returns>
        public int GetColumnIndexValue()
        {
            return this.columnIndex;
        }

        /// <summary>
        /// Function to obtain the Cell index value in UI's perspective.
        /// </summary>
        /// <returns> string containing cell index value.</returns>
        public string GetCellIndex()
        {
            string indexLocation = ((char)(this.columnIndex + 65)).ToString() + (this.rowIndex + 1).ToString();
            return indexLocation;
        }

        /// <summary>
        /// Function to subscribe the expression tree to changes made
        /// to cells in expression.
        /// </summary>
        /// <param name="cell"> Storing the cell that needs to be subscribed to.</param>
        public void SubscribeCellToTree(CellClass cell)
        {
            cell.PropertyChanged += this.expression.CellChanged;
            this.expression.UpdateVariableDict(cell);
        }

        /// <summary>
        /// Function initialize the expression tree
        /// and set the cell reference for the expression tree.
        /// </summary>
        /// <param name="expression"> expression that needs to be evaluated.</param>
        public void InitializeTree(string expression)
        {
            this.expression = new ExpressionTree(expression);
            this.expression.SetCellForTree(this);
        }

        /// <summary>
        /// Function to return the variable names from an expression.
        /// calls get variable names in expression tree.
        /// </summary>
        /// <returns> List of strings.</returns>
        public List<string> VariableNames()
        {
            return this.expression.GetVariableNames();
        }

        /// <summary>
        /// Returns the evaluated value of the expression associated with the cell.
        /// </summary>
        /// <returns> double value of the evaluation.</returns>
        public double EvaluateExpression()
        {
            return this.expression.Evaluate();
        }

        /// <summary>
        /// Method to trigger property changed event.
        /// </summary>
        /// <param name="propertyName"> property name that's subject to change.</param>
        public void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Gets the XML schema.
        /// </summary>
        /// <returns> XML schema.</returns>
        public XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// Function to read cell attributes from xml reader.
        /// </summary>
        /// <param name="reader"> XML stream parameter.</param>
        public void ReadXml(XmlReader reader)
        {
            while (reader.Read() && reader.Name != "cell")
            {
                if (reader.NodeType == XmlNodeType.EndElement)
                {
                    continue;
                }

                if (reader.Name == "bgcolor")
                {
                    uint bgcolor;
                    reader.Read();
                    uint.TryParse(reader.Value, out bgcolor);
                    this.BGcolor = bgcolor;
                }
                else if (reader.Name == "text")
                {
                    reader.Read();
                    this.Text = reader.Value;
                }
            }
        }

        /// <summary>
        /// Function to write cell attributes to stream parameter passed.
        /// </summary>
        /// <param name="writer"> XML stream parameter.</param>
        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("cell");
            writer.WriteAttributeString("name", this.GetCellIndex());

            writer.WriteStartElement("bgcolor");
            writer.WriteString(this.BGcolor.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("text");
            writer.WriteString(this.Text.ToString());
            writer.WriteEndElement();

            writer.WriteEndElement();
        }
    }
}