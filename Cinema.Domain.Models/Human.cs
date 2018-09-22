namespace Cinema.Domain.Models
{
    public sealed class Human
    {
        private string name;
        private string surname;

        public Human(string name, string surname)
        {
            this.name = name;
            this.surname = surname;
        }

        public string Name => name;

        public string Surname => surname;
    }
}