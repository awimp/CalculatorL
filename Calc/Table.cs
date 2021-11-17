using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc
{
    public class Table
    {
        private const int defaultCol = 30;
        private const int defaultRow = 10;
        public int colCount;
        public int rowCount;
        public static List<List<Cell>> grid = new List<List<Cell>>();
        public Dictionary<string, string> dictionary = new Dictionary<string, string>();
        public Table()

        {
            setTable(defaultCol, defaultRow);
        }

        public void setTable(int col, int row)
        {
            Clear();
            colCount = col;
            rowCount = row;
            for (int i = 0; i < rowCount; i++)
            {
                List<Cell> newRow = new List<Cell>();
                for (int j = 0; j < colCount; j++)
                {
                    newRow.Add(new Cell(i, j));
                    dictionary.Add(newRow.Last().getName(), "");
                }
                grid.Add(newRow);
            }
        }

        public void Clear()
        {

            foreach (List<Cell> list in grid)
            {
                list.Clear();
            }
            grid.Clear();
            dictionary.Clear();
            rowCount = 0;
            colCount = 0;
        }
    }
    /*private string FullName(int row, int col)
    {

        Cell cell = new Cell(row, col);
        return cell.getName();

    }
    public void ChangeCellwithAllPointers(int row, int col, string expression, System.Windows.Forms.DataGridView dataGridView1)
    {
        grid[row][col].DeletePointersAndReferences();
        grid[row][col].expression = expression;
        grid[row][col].new_referencesFromThis.Clear();

        if (expression != "")

        {
            if (expression[0] != '=')
            {
                grid[row][col].value = expression;
                dictionary[FullName(row, col)] = expression;
                foreach (Cell cell in grid[row][col].pointersToThis)
                {
                    RefreshCellandPointers(cell, dataGridView1);
                }
                return;
            }
        }


        string new_expression = ConvertReferences(row, col, expression);
        if (new_expression != "")

        {
            new_expression = new_expression.Remove(0, 1);
        }
        if (!grid[row][col].CheckLoop(grid[row][col].new_referencesFromThis))
        {

            System.Windows.Forms.MessageBox.Show("There is a loop! Change the expression.");
            grid[row][col].expression = "";

            grid[row][col].value = "0";

            dataGridView1[col, row].Value = "0";

            return;

        }

        grid[row][col].AddPointersAndReferences();
        string val = Calculate(new_expression);
        if (val == "Error")

        {
            System.Windows.Forms.MessageBox.Show("Error in cell " + FullName(row, col));
            grid[row][col].expression = "";
            grid[row][col].value = "0";
            dataGridView1[col, row].Value = "@";
            return;
        }

        grid[row][col].value = val;

        dictionary[FullName(row, col)] = val;

        foreach (Cell cell in grid[[row][col].pointersToThis)
            RefreshCellAndPointers(cell, dataGridView1);
    }
    

    public bool RefreshCellAndPointers(Cell cell, System.Windows.Forms.DataGridView dataGridView1)
    {
        cell.new_referencesFromThis.Clear();
        string new_expression = ConvertReferences(cell.row, cell.column, cell.expression);
        new_expression = new_expression.Remove(0, 1);
        string Value = Calculate(new_expression);

        if (Value == “Error”) 

            {
            System.Windows.Forms.MessageBox.Show("Error in cell " + cell.getName());
            cell.expression = "";
            cell.value = "0";
            dataGridView1[cell.column, cell.row].Value = "0";
            return false;
        }

        grid[cell.row][cell.column].value = Value;
        dictionary[FullName(cell.row, cell.column)] = Value;
        dataGridView1[cell.column, cell.row].Value = Value;

        foreach (Cell point in cell.pointersToThis)

        {
            if (!RefreshCellAndPointers(point, dataGridView1))
                return false;
        }
        return true;
    }


    public string ConvertReferences(int row, int col, string expr)
    {
        string cellPattern = @"[A-Z]+[0-9]+";
        Regex regex = new Regex(cellPattern, RegexOptions.IgnoreCase);
        Index nums;

        foreach (Match match in regex.Matches(expr))

        {

            if (dictionary.ContainsKey(match.Value))

            {
                nums = NumberConverter.From26System(match.Value);
                grid[row][col].new_referencesFromThis.Add(grid[nums.row][nums.column]);

            }
        }

        MatchEvaluator evaluator = new MatchEvaluator(referenceToValue);
        string new_expression = regex.Replace(expr, evaluator);
        return new_expression;

    }

    public string referenceToValue(Match m)

    {
        if (dictionary.ContainsKey(m.Value))

            if (dictionary[m.Value] == "")
                return "0";
            else
                return dictionary[m.Value];
        return m.Value;
    }


    

    public string Calculate(string expression)

    {

        string res = null;

        try

        {
            res = Convert.ToString(TableManager.Calculator.Evaluate(expression));
            if (res == "infinity")
            {

                res = "Division by zero error";
            }

            return res;

        }

        catch

        {
            return "Error";

        }
    }
    public void AddRow(System.Windows.Forms.DataGridView dataGridView1)

    {

        List<Cell> newRow = new List<Cell>();

        for (int j = 0; j < colCount; j++)

        {
            newRow.Add(new TableEditor.Cell(rowCount, j));
            dictionary.Add(newRow.Last().getName(), "");

        }

        grid.Add(newRow);

        RefreshReferences();
        foreach (List<Cell> list in grid)

        {
            foreach (Cell cell in list)
            {
                if (cell.referencesFromThis != null)
                {
                    foreach (Cell cell_ref in cell.referencesFromThis)
                    {
                        if (cell_ref.row == rowCount)
                        {
                            if (!cell_ref.pointersToThis.Contains(cell))
                                cell_ref.pointersToThis.Add(cell);
                        }
                    }
                }
            }
        }
        for (int j = 0; j < colCount; j++)
        {
            ChangeCellwithAllPointers(rowCount, j, "", dataGridView1);
        }
        rowCount++;
    }
    public void RefreshReferences()
    {
        foreach (List<Cell> list in grid)
        {
            foreach (Cell cell in list)
            {
                if (cell.referencesfromThis != null)
                    cell.referencesfromThis.Clear();
                if (cell.new_referencesfromThis != null)
                    cell.new_referencesFromThis.Clear();
                if (cell.expression == "")
                    continue;
                string new_expession = cell.expression;
                if (cell.expression[0] == '=')
                {

                    new_expession = ConvertReferences(cell.row, cell.column, cell.expression);
                    cell.referencesFromThis.AddRange(cell.new_referencesFromThis);


                }

            }

        }*/
}
    
      
    