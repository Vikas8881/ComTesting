using Microsoft.EntityFrameworkCore;
using Ecommerce.Data;
using Ecommerce.Model;

namespace Ecommerce.Seed
{
    public class SeedCity
    {
        private readonly ModelBuilder modelBuilder;


        public SeedCity(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }
        public void Seed()
        {
        

        }
    }
}
