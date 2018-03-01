namespace moviecruiser.Data
{
    //Seeder class for DbContext
    public static class DbSeeder
    {
        public static void Seed(MoviesDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
