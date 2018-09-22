namespace Cinema.Domain.Models
{
    public sealed class Film
    {
        private readonly string title;

        public Film(string title)
        {
            this.title = title;
        }

        public string Title => title;
    }
}