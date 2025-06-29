using LearnShop.Api.Services.Interfaces;
using LearnShop.Infra.Interfaces;
using LearnShop.Model.Products;

namespace LearnShop.Api.Services
{
    public class EbookService : IEbookService
    {
        private readonly IEbookRepository _ebookRepository;

        public EbookService(IEbookRepository ebookRepository)
        {
            _ebookRepository = ebookRepository;
        }

        public async Task<List<Ebook>> GetEbooksAsync()
        {
            var ebooks = await _ebookRepository.GetAllAsync();
            return ebooks.Where(e => e != null).ToList();
        }

        public async Task<Ebook?> GetEbookByIdAsync(long id)
        {
            return await _ebookRepository.GetByIdAsync(id);
        }

        public async Task<Ebook?> CreateEbookAsync(Ebook ebook)
        {
            if (ebook == null)
            {
                throw new ArgumentNullException(nameof(ebook), "O ebook não pode ser nulo");
            }

            if (string.IsNullOrWhiteSpace(ebook.Title) ||
                string.IsNullOrWhiteSpace(ebook.Author) ||
                string.IsNullOrWhiteSpace(ebook.Publisher) ||
                string.IsNullOrWhiteSpace(ebook.Category))
            {
                throw new ArgumentException("Campos obrigatórios do ebook não podem estar vazios");
            }

            if (ebook.Price <= 0)
            {
                throw new ArgumentException("O preço do ebook deve ser maior que zero", nameof(ebook.Price));
            }

            return await _ebookRepository.InsertAsync(ebook);
        }

        public async Task<Ebook?> UpdateEbookAsync(long id, Ebook ebook)
        {
            if (ebook == null)
            {
                throw new ArgumentNullException(nameof(ebook), "O ebook não pode ser nulo");
            }

            if (string.IsNullOrWhiteSpace(ebook.Title) ||
                string.IsNullOrWhiteSpace(ebook.Author) ||
                string.IsNullOrWhiteSpace(ebook.Publisher) ||
                string.IsNullOrWhiteSpace(ebook.Category))
            {
                throw new ArgumentException("Campos obrigatórios do ebook não podem estar vazios");
            }

            if (ebook.Price <= 0)
            {
                throw new ArgumentException("O preço do ebook deve ser maior que zero", nameof(ebook.Price));
            }

            return await _ebookRepository.UpdateAsync(id, ebook);
        }

        public async Task DeleteEbookAsync(long id)
        {
            var existingEbook = await _ebookRepository.GetByIdAsync(id);
            if (existingEbook == null)
            {
                throw new ArgumentException($"Ebook com ID {id} não encontrado", nameof(id));
            }

            await _ebookRepository.DeleteAsync(id);
        }

        public async Task<List<Ebook>> GetEbooksByCategoryAsync(string category)
        {
            var ebooks = await _ebookRepository.GetAllAsync();

            return ebooks
                .Where(e => e != null && e.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}
