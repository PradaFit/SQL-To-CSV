using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLConverter
{
    public partial class Form1 : Form
    {
        private string selectedFilePath;

        public Form1()
        {
            InitializeComponent();
            MessageBox.Show("Made By: PradaFit");
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
        }

        private void btn_insert_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "SQL Files (*.sql)|*.sql",
                Title = "Select a SQL File"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                selectedFilePath = openFileDialog.FileName;
                MessageBox.Show($"Selected file: {selectedFilePath}");
            }
        }

        private async void btn_convert_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedFilePath))
            {
                MessageBox.Show("Please select a .sql file first.");
                return;
            }

            try
            {
                string csvFilePath = Path.ChangeExtension(selectedFilePath, ".csv");

                int totalLines = File.ReadAllLines(selectedFilePath).Length;
                int processedLines = 0;

                progressBar1.Minimum = 0;
                progressBar1.Maximum = totalLines;
                progressBar1.Value = 0;

                using (StreamReader reader = new StreamReader(selectedFilePath))
                using (StreamWriter writer = new StreamWriter(csvFilePath, false))
                {
                    string line;
                    string insertBuffer = "";

                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        processedLines++;

                        Invoke(new Action(() =>
                        {
                            progressBar1.Value = processedLines;
                        }));

                        // Process only INSERT INTO statements, skip everything else
                        if (line.StartsWith("INSERT INTO", StringComparison.OrdinalIgnoreCase))
                        {
                            insertBuffer = line.Trim();
                        }
                        else if (!string.IsNullOrEmpty(insertBuffer))
                        {
                            insertBuffer += line.Trim();

                            if (line.EndsWith(");"))
                            {
                                await Task.Run(() => ProcessInsertStatement(insertBuffer, writer));
                                insertBuffer = "";
                            }
                        }
                        else if (line.StartsWith("--") || string.IsNullOrWhiteSpace(line))
                        {
                            // Skip comments and blank lines
                            continue;
                        }
                    }
                }

                MessageBox.Show($"File converted successfully to {csvFilePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error converting file: {ex.Message}");
            }
        }

        private void ProcessInsertStatement(string insertStatement, StreamWriter writer)
        {
            try
            {
                int valuesIndex = insertStatement.IndexOf("VALUES", StringComparison.OrdinalIgnoreCase);
                if (valuesIndex >= 0)
                {
                    string valuesPart = insertStatement.Substring(valuesIndex + 6).Trim();

                    valuesPart = valuesPart.Trim(';', ' ');

                    string[] rows = valuesPart.Split(new[] { "),(" }, StringSplitOptions.None);

                    foreach (var row in rows)
                    {
                        string cleanedRow = row.Trim('(', ')').Replace("'", "").Replace("::", "").Trim();
                        string combinedRow = string.Join(",", cleanedRow.Split(',').Select(value => value.Trim()));

                        if (!string.IsNullOrEmpty(combinedRow))
                        {
                            lock (writer)
                            {
                                writer.WriteLine(combinedRow); // Thread safety for writing
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing INSERT INTO statement: {ex.Message}");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            MessageBox.Show("Made By PradaFit", "Enjoy", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
            // You can add code here for future
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
