namespace Cinema.Domain.Models
{
    public sealed class Film
    {
        private readonly string name;

        public Film(string name)
        {
            this.name = name;
        }
    }
}