using AGDAR.Models;

namespace AGDAR.Seeder
{
    public class AGDARSeeder
    {
        private readonly AGDARDbContext _dbContext;
        public AGDARSeeder(AGDARDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Seed ()
        {
            if(_dbContext.Database.CanConnect())
            {
                if(!_dbContext.Products.Any())
                {
                    var products = GetProducts();
                    _dbContext.Products.AddRange(products);
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.Categories.Any())
                {
                    var categorys = GetCategorys();
                    _dbContext.Categories.AddRange(categorys);
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.States.Any())
                {
                    var states = GetStates();
                    _dbContext.States.AddRange(states);
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.Parts.Any())
                {
                    var parts = GetParts();
                    _dbContext.Parts.AddRange(parts);
                    _dbContext.SaveChanges();
                }
            }
        }
        private IEnumerable<Product> GetProducts()
        {
            return new List<Product>()
            {
                new Product
                {
                    Name = "Wiertarka",
                    Price = 20000,
                    Description = "Simple wiertarka",
                    Brand = "Makita"
                },
                new Product
                {
                    Name = "Wkrętarka",
                    Price = 21000,
                    Description = "Simple Wkrętarka",
                    Brand = "Verkato"
                },
                new Product
                {
                    Name = "Zakrętarka",
                    Price = 22000,
                    Description = "Simple Zakretarka",
                    Brand = "Grapite"
                },
                new Product
                {
                    Name = "Szlifierka",
                    Price = 21000,
                    Description = "Simple Szlifierka",
                    Brand = "Dewalt"
                },
            };           
        }
        private IEnumerable<Role> GetRoles()
        {
            return new List<Role>()
            {
                new Role
                {
                    Name = "Admin"                   
                },
                new Role
                {
                    Name = "Seller"
                },
                new Role
                {
                    Name = "Mechanic"
                },               
            };
        }
        private IEnumerable<Category> GetCategorys()
        {
            return new List<Category>()
            {
                new Category
                {
                    Name = "Battery"
                },
                new Category
                {
                    Name = "Current"
                }
            };
        }
        private IEnumerable<State> GetStates()
        {
            return new List<State>()
            {
                new State
                {
                    Name = "New"
                },
                new State
                {
                    Name = "To repair"
                },
            };
        }
        private IEnumerable<Part> GetParts()
        {
            return new List<Part>()
            {
                new Part
                {
                    Name = "Głowica",
                    Description = "a"
                },
                new Part
                {
                    Name = "Battery 4ah",
                    Description = "a"
                },
                new Part
                {
                    Name = "Battery 2ah",
                    Description = "a"
                },
                new Part
                {
                    Name = "Rączka",
                    Description = "a"
                },
                new Part
                {
                    Name = "Osłona",
                    Description = "a"
                },
            };
        }
    }
}