namespace Cinema.Domain.Models.JobTitles
{
    public sealed class Director :JobTitle
    {
        public Director(Human human) : 
            base(human)
        {
        }
    }
}