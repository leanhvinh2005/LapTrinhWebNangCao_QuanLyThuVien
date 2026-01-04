namespace Website.Services.Other
{
    public class SearchService
    {
        public string CurrentQuery { get; private set; } = "";

        public event Action? OnSearchChanged;

        public void UpdateQuery(string query)
        {
            CurrentQuery = query;
            OnSearchChanged?.Invoke();
        }
    }
}
