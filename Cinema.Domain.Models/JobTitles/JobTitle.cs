namespace Cinema.Domain.Models.JobTitles
{
    public abstract class JobTitle
    {
        private Human human;

        public JobTitle(Human human)
        {
            this.human = human;
        }

        public Human Human => human;
    }
}