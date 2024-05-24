using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AMLScreeningWpfApp1
{
    public class SearchResult
    {
        public string UniqueID { get; set; }
        public string NameAndSurname { get; set; }
        public string SanctionsRegime { get; set; }
        public string SanctionsStatus { get; set; }
        public string SanctionsType { get; set; }
        public string Nationality { get; set; }
        public string Address { get; set; }
        public string PostZipCode { get; set; }
        public string Country { get; set; }
        public string DateOfBirth { get; set; }
        public string TownOfBirth { get; set; }
        public string CountryOfBirth { get; set; }
        public string PositionDesignation { get; set; }
        public string PassportDetails { get; set; }
        public string NationalIDNo { get; set; }
        public string SanctionsReference { get; set; }
        public string DateListed { get; set; }
        public string OtherInformationRemarks { get; set; }
        public string Source { get; set; }
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(); // Set the DataContext
            LoadSearchTypes();
            searchTypeComboBox.SelectedIndex = 0; // Set default selection
        }

        private void LoadSearchTypes()
        {
            searchTypeComboBox.ItemsSource = Enum.GetValues(typeof(SearchType)).Cast<SearchType>();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var searchType = (SearchType)searchTypeComboBox.SelectedItem;
            var searchName = searchNameTextBox.Text;
            var enableFuzzySearch = fuzzySearchCheckBox.IsChecked ?? false;
            var fuzzyDistance = (int)fuzzyDistanceSlider.Value;

            // Perform search
            var results = PerformSearch(searchType, searchName, enableFuzzySearch, fuzzyDistance);

            if (results.Count == 0)
            {
                nilResultsTextBox.Visibility = Visibility.Visible;
                nilResultsLabel.Visibility = Visibility.Visible;
                nilResultsTextBox.Text = "Your search has not returned any results."; // Populate the textbox with the text
            }
            else
            {
                nilResultsTextBox.Visibility = Visibility.Collapsed;
                nilResultsLabel.Visibility = Visibility.Collapsed;
            }

            // Display results in DataGrid
            resultsDataGrid.ItemsSource = results;
        }

        private List<SearchResult> PerformSearch(SearchType searchType, string searchName, bool enableFuzzySearch, int fuzzyDistance)
        {
            // Mock search implementation
            var results = new List<SearchResult>
    {
        new SearchResult { UniqueID = "1", NameAndSurname = "John Doe", SanctionsRegime = "Regime1", SanctionsStatus = "Active", SanctionsType = "Type1", Nationality = "USA", Address = "123 Main St", PostZipCode = "12345", Country = "USA", DateOfBirth = "01/01/1980", TownOfBirth = "Town1", CountryOfBirth = "USA", PositionDesignation = "Position1", PassportDetails = "123456789", NationalIDNo = "987654321", SanctionsReference = "Ref1", DateListed = "01/01/2020", OtherInformationRemarks = "Remarks1", Source = "Source1" }
        // Add more mock results as needed
    };

            if (!string.IsNullOrEmpty(searchName))
            {
                results = results.FindAll(r => r.NameAndSurname.Contains(searchName, StringComparison.OrdinalIgnoreCase));
            }

            if (enableFuzzySearch)
            {
                // Implement fuzzy search logic here
                results = results.FindAll(r => r.NameAndSurname.Length <= searchName.Length + fuzzyDistance);
            }

            return results;
        }


        private void FuzzySearchCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var isChecked = fuzzySearchCheckBox.IsChecked ?? false;
            fuzzyDistanceSlider.Visibility = isChecked ? Visibility.Visible : Visibility.Collapsed;
            fuzzyDistanceLabel.Visibility = isChecked ? Visibility.Visible : Visibility.Collapsed;
        }

        private void SearchTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handle selection change logic if needed
        }
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            // Clear the selection in the ComboBox
            searchTypeComboBox.SelectedIndex = -1;

            // Clear the text in the TextBox
            searchNameTextBox.Text = String.Empty;

            // Clear the items in the DataGrid
            resultsDataGrid.ItemsSource = null;

            // Clear the test in the Nil Results textbox
            nilResultsTextBox.Text = string.Empty;

            // Collapse the Visibility of the "Nil Results" Label and TextBox
            nilResultsTextBox.Visibility = Visibility.Collapsed;
            nilResultsLabel.Visibility = Visibility.Collapsed;
        }

    }

    public enum SearchType
    {
        Individual,
        Entity,
        PEP,
        VesselShip,
        Aircraft,
        All
    }
}


