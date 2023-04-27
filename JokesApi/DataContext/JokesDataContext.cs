using JokesApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


namespace JokesApi.DataContext
{
    public class JokesDataContext: DbContext
    {
        public JokesDataContext(DbContextOptions<JokesDataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthorModel>().Property(mod => mod.RegistrationDate)
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            modelBuilder.Entity<JokeModel>().Property(mod => mod.DateAdded)
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

        }

        public DbSet<AuthorModel> Authors { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<JokeModel> Jokes { get; set; }
        public DbSet<LanguageModel> Languages { get; set; }
    }
}
