namespace Calc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void InitializeDataGridView(int rows, int columns)

        {
            dataGridView1.ColumnHeadersVisible = true;
            dataGridView1.RowHeadersVisible = true;
            dataGridView1.ColumnCount = columns;

            for (int i = 0; i < columns; i++)
            {
                string columnName = NumberConverter.To26System(i);
                dataGridView1.Columns[i].Name = columnName;
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            for (int i = 0; i < rows; i++)
            {
                dataGridView1.Rows.Add("");
                dataGridView1.Rows[i].HeaderCell.Value = (i).ToString();
            }

            /*dataGridView1.AutoResizeRowHeadersWidth(DataGridViewRowHeaderswWidthSizeMode.AutoSizeToAllHeaders);
            table.setTable(columns, rows);*/
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int col = dataGridView1.SelectedCells[0].ColumnIndex;

            int row = dataGridView1.SelectedCells[0].RowIndex;

            string expression = Table.grid[row][col].expression;

            string value = Table.grid[row][col].value;

            textBox.Text = expression;

            textBox.Focus();
        }
        private void buttonCalculate_Click(object sender, EventArgs e)

        {

            int col = dataGridView1.SelectedCells[0].ColumnIndex;

            int row = dataGridView1.SelectedCells[0].RowIndex;

            string expression = textBox.Text;

            if (expression == "") return;

            /*table.ChangeCellwithAllPointers(row, col, expression, dataGridView1);
            dataGridView1[col, row].Value = Table.grid[row][col].value;*/
        }


       
    }
    /*private void buttonAR_Click(object sender, EventArgs e)

      {
        DataGridViewRow row = new System.Windows.Forms.DataGridViewRow();
        if (dataGridView1.Columns.Count == 0)
        {
            MessageBox.Show("There are no columns!");
            return;
        }
        dataGridView1.Rows.Add(row);
        dataGridView1.Rows[table.rowCount].HeaderCell.Value = (table.rowCount).ToString();
        table.AddRow(dataGridView1);
    }

    private void buttonDR_Click(object sender, EventArgs e)
    {
        if (!table.DeleteRow(dataGridView1))
            return;
        dataGridView1.Rows.RemoveAt(table.rowCount);
    }

    private void buttonAC_Click(object sender, EventArgs e)

    {
        string name = NumberConverter.To26System(table.colCount);
        dataGridView1.Columns.Add(name, name);
        table.AddColumn(dataGridView1);

    }

    private void buttonDC_Click(object sender, EventArgs e)

    {
        if (!table.DeleteColumn(dataGridView1))

            return;
        dataGridView1.Columns.RemoveAt(table.colCount);
    }*/

}