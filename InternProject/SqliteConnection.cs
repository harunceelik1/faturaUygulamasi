
namespace InternProject
{
    internal class SqliteConnection : IDisposable
    {
        private string v;

        public SqliteConnection(string v)
        {
            this.v = v;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}