using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;

namespace AMLScreeningWpfApp1
{
    public class MainWindowViewModel : ObservableObject
    {
        private string _searchType;
        private string _searchName;
        private ObservableCollection<OFACEntity> _searchResults;

        public MainWindowViewModel()
        {
            SearchCommand = new RelayCommand(OnSearch);
            ResetCommand = new RelayCommand(OnReset);
            SearchResults = new ObservableCollection<OFACEntity>();
        }

        public string SearchType
        {
            get => _searchType;
            set => SetProperty(ref _searchType, value);
        }

        public string SearchName
        {
            get => _searchName;
            set => SetProperty(ref _searchName, value);
        }

        public ObservableCollection<SearchResult> SearchResults
        {
            get => _searchResults;
            set => SetProperty(ref _searchResults, value);
        }

        public ICommand SearchCommand { get; }
        public ICommand ResetCommand { get; }

        private void OnSearch()
        {
            // Implement your search logic here and update SearchResults
            var searchType = (SearchType)Enum.Parse(typeof(SearchType), SearchType); // Convert string to enum
            var searchName = SearchName;
            var enableFuzzySearch = false; // Set to true or false based on your logic
            var fuzzyDistance = 0; // Set the fuzzy distance based on your logic

            // Perform search
            var results = PerformSearch(searchType, searchName, enableFuzzySearch, fuzzyDistance);

            // Assign search results to the SearchResults property
            SearchResults.Clear(); // Clear previous results
            foreach (var result in results)
            {
                SearchResults.Add(result);
            }
        }

        private void OnReset()
        {
            SearchType = string.Empty;
            SearchName = string.Empty;
            SearchResults.Clear();
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
    }
}


