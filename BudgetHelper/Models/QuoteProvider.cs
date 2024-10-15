using System.Text.Json;

namespace BudgetHelper.Models
{
    public class QuoteProvider
    {
        private static readonly HttpClient client = new HttpClient();
        private string currentQuote;
        public bool isLoaded { get;  set; }
        public QuoteProvider() { }

        public string CurrentQuote => currentQuote;

        public async Task UpdateQuote()
        {
            if (isLoaded == false)
            {
                currentQuote = await GetRandomQuoteAsync();
                isLoaded = true;
            }
        }

        private async Task<string> GetRandomQuoteAsync()
        {

            try
            {
                var response = await client.GetAsync("https://api.adviceslip.com/advice");
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var quoteObj = JsonSerializer.Deserialize<AdviceSlipResponse>(jsonResponse);

                    return  $"{quoteObj.Slip.Advice}";
                }
                return  $"No quote available at the moment. Status {response.StatusCode}";
            }
            catch (HttpRequestException ex)
            {

                return ex.Message;
            }


        }
    }
}
