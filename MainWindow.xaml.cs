using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenFile(object sender, RoutedEventArgs e)
        {
            // Create an instance of OpenFileDialog for selecting the input file
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt";
            openFileDialog.Title = "Select the input file";

            if (openFileDialog.ShowDialog() == true)
            {
                // Get the selected input file path
                string inputFilePath = openFileDialog.FileName;

                // Create an instance of SaveFileDialog for selecting the output file
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text files (*.txt)|*.txt";
                saveFileDialog.Title = "Select the output file";


                if (saveFileDialog.ShowDialog() == true)
                {
                    // Get the selected output file path
                    string outputFilePath = saveFileDialog.FileName;

                    // Create an instance of TextFileAnalyzer
                    TextFileAnalyzer analyzer = new TextFileAnalyzer(inputFilePath);

                    // Analyze the file
                    analyzer.AnalyzeFile();

                    // Update the TextBox with the analysis result
                    OutText.Text = $"Total Word Count: {analyzer.TotalWordCount}\n" +
                                   $"Unique Word Count: {analyzer.UniqueWordCount}\n" +
                                   $"Total Palindrome Count: {analyzer.TotalPalindromeCount}\n\n" +
                                   "Word Counts:\n";

                    foreach (var entry in analyzer.wordCounts)
                    {
                        OutText.Text += $"{entry.Key} - {entry.Value}\n";
                    }

                    OutText.Text += "\nPalindrome Counts:\n";

                    foreach (var entry in analyzer.palindromeCounts)
                    {
                        OutText.Text += $"{entry.Key} - {entry.Value}\n";
                    }

                    OutText.Text += "\nPalindrome Sentences:\n";

                    foreach (string sentence in analyzer.palindromeSentences)
                    {
                        OutText.Text += $"{sentence}\n";
                    }

                    // Write the analysis result to the output file
                    analyzer.WriteAnalysisResult(outputFilePath);
                }



                
            }
        }





    }
}
