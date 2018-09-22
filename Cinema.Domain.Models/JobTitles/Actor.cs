namespace Cinema.Domain.Models.JobTitles
{
    public sealed class Actor : JobTitle
    {
        public Actor(Human human) :
            base (human)
        {
        }
    }
}