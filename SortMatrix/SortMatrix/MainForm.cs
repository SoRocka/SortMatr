using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SortMatrix
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private int rows;
        private int columns;
        private int[,] matrix;

        private void LoadFromMatrixToDgv()
        {
            dgv.Rows.Clear();
            int height = matrix.GetLength(0);
            int width = matrix.GetLength(1);

            dgv.ColumnCount = width;

            for (int r = 0; r < height; r++)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dgv);

                for (int c = 0; c < width; c++)
                {
                    row.Cells[c].Value = matrix[r, c];
                }

                dgv.Rows.Add(row);
            }
            //dgv.Rows.RemoveAt(height);
        }



        private void BubbleSort()
        {
            int size = rows * columns;
            int[] array = new int[size];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    array[i * rows + j] = matrix[i,j];
                }
            }
            int temp;
            for (int write = 0; write < array.Length; write++)
            {
                for (int sort = 0; sort < array.Length - 1; sort++)
                {
                    if (array[sort] > array[sort + 1])
                    {
                        temp = array[sort + 1];
                        array[sort + 1] = array[sort];
                        array[sort] = temp;
                    }
                }
            }
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    matrix[i, j] = array[i * rows + j];
                }
            }
            LoadFromMatrixToDgv();

        }


        private void FileOpen_Click(object sender, EventArgs e)
        {
            string[] fileContent = null;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (StreamReader reader = new StreamReader(openFileDialog.OpenFile()))
                    {
                        fileContent = File.ReadAllLines(openFileDialog.FileName);
                    }
                    try
                    {
                        int[] dimensions = Array.ConvertAll(fileContent[0].Split(' '), int.Parse);
                        rows = dimensions[0];
                        columns = dimensions[1];
                        matrix = new int[rows, columns];
                        int rowsAmount = 0;
                        foreach (string str in fileContent.Skip(1).ToArray())
                        {
                            int[] row = Array.ConvertAll(str.Split(' '), int.Parse);
                            if (row.Length != columns)
                            {
                                throw new Exception("Количество чисел в строке не соответствут заявленному");
                            }
                            for (int i = 0; i < columns; i++)
                            {
                                matrix[rowsAmount, i] = row[i];
                            }
                            rowsAmount++;
                        }
                        if (rowsAmount != rows)
                        {
                            throw new Exception("Количество строк не соответствут заявленному. Заявлено: " + rows.ToString() + "; Фактически: " + rowsAmount.ToString());
                        }
                        LoadFromMatrixToDgv();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка, проверьте файл на правильность введенных данных.\nПодробности: " + ex.Message);
                    }
                }
            }
        }

        private void SortMenuItem_Click(object sender, EventArgs e)
        {
            if(dgv.Rows.Count == 0)
            {
                MessageBox.Show("Матрица пустая!");
                return;
            }
            BubbleSort();
        }
    }
}
