namespace Panda.Site.Jobs
{
    public class PostIndexCreator
    {
        public Task Exec()
        {
            Console.WriteLine("hello world:"+DateTime.Now);
            return Task.CompletedTask;
        }
    }
}
