using System.Security.Cryptography;
using System.Text;

namespace BibliotecaCrud.Data
{
    public class sha256
    {
        public  string convertToSha256(string data)
        {
            StringBuilder builder = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding encoding = Encoding.UTF8;
                byte[] result = hash.ComputeHash(encoding.GetBytes(data));

                foreach (byte items in result)
                {
                    builder.Append(items.ToString("x2"));
                }
            } 

            return builder.ToString();
        }
    }
}
