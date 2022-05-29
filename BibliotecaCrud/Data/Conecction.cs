using System.Data.SqlClient;
namespace BibliotecaCrud.Data

{
    public class Conecction
    {
        private string stringSQL = string.Empty;

        public Conecction()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            stringSQL = builder.GetSection("ConnectionStrings:StringSql").Value;

        }

        public string getStringSql()
        {
            return stringSQL;
        }
    }
}
