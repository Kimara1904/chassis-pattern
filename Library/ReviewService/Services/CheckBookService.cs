using Exceptions.Exeptions;
using ReviewService.Interfaces;

namespace ReviewService.Services
{
    public class CheckBookService : ICheckBookService
    {
        private readonly HttpClient _httpClient;

        public CheckBookService(HttpClient client)
        {
            _httpClient = client;
        }

        public async Task CheckBook(int bookId)
        {
            var response = await _httpClient.GetAsync(string.Format("http://host.docker.internal:8082/api/books/check/{0}", bookId));

            if (response != null)
            {
                if (response.IsSuccessStatusCode)
                {
                    return;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new NotFoundException(string.Format("There is no book with id: {0}", bookId));
                }
                else
                {
                    throw new InternalServerErrorException("Something doesn't work");
                }
            }
            else
            {
                throw new InternalServerErrorException("Something doesn't work");
            }
        }
    }
}
