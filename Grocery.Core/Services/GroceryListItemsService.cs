using System.Diagnostics;
using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.Core.Services
{
    public class GroceryListItemsService : IGroceryListItemsService
    {
        private readonly IGroceryListItemsRepository _groceriesRepository;
        private readonly IProductRepository _productRepository;

        public GroceryListItemsService(IGroceryListItemsRepository groceriesRepository, IProductRepository productRepository)
        {
            _groceriesRepository = groceriesRepository;
            _productRepository = productRepository;
        }

        public List<GroceryListItem> GetAll()
        {
            List<GroceryListItem> groceryListItems = _groceriesRepository.GetAll();
            FillService(groceryListItems);
            return groceryListItems;
        }

        public List<GroceryListItem> GetAllOnGroceryListId(int groceryListId)
        {
            List<GroceryListItem> groceryListItems = _groceriesRepository.GetAll().Where(g => g.GroceryListId == groceryListId).ToList();
            FillService(groceryListItems);
            return groceryListItems;
        }

        public GroceryListItem Add(GroceryListItem item)
        {
            return _groceriesRepository.Add(item);
        }

        public GroceryListItem? Delete(GroceryListItem item)
        {
            throw new NotImplementedException();
        }

        public GroceryListItem? Get(int id)
        {
            return _groceriesRepository.Get(id);
        }

        public GroceryListItem? Update(GroceryListItem item)
        {
            return _groceriesRepository.Update(item);
        }

        public List<BestSellingProducts> GetBestSellingProducts(int topX = 5) // Geeft de topX aantal bestverkopende producten
        {   
            // Maak een lijst en voeg alle prucducten met een vooraad hieraan toe
            List<GroceryListItem> valideProducten = new();
            foreach (GroceryListItem item in _groceriesRepository.GetAll())
            {
                if (item.Amount > 0)
                {
                    valideProducten.Add(item);
                }
            }
            
            // Pak de 5 best verkochte producten
            var productSales = valideProducten
                .GroupBy(item => item.ProductId)
                .Select(group => new
                {
                    ProductId = group.Key,
                    TotalSold = group.Sum(item => item.Amount)
                })
                .OrderByDescending(x => x.TotalSold)
                .Take(topX)
                .ToList();
            
            List<BestSellingProducts> gesorteerdeItems = new();
            int rank = 1;
            
            // Maak van de producten BestSellingProducts met een ranking
            foreach (var productSale in productSales)
            {
                Product product = _productRepository.Get(productSale.ProductId);
                if (product != null)
                {
                    gesorteerdeItems.Add(new BestSellingProducts(
                        product.Id,
                        product.Name, 
                        product.Stock,
                        productSale.TotalSold,
                        rank
                    ));
                    rank++;
                }
            }

            foreach (var product in gesorteerdeItems)
            {
                Debug.WriteLine($"\n {product.ranking}: {product.Name}, {product.Id}, {product.NrOfSells}"); // debugregel om te kijken of de juiste producten in de lijst zitten
            }
            return gesorteerdeItems;
        }

        private void FillService(List<GroceryListItem> groceryListItems)
        {
            foreach (GroceryListItem g in groceryListItems)
            {
                g.Product = _productRepository.Get(g.ProductId) ?? new(0, "", 0);
            }
        }
    }
}
