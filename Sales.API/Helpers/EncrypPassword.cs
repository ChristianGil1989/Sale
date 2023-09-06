using XSystem.Security.Cryptography;

namespace Sales.API.Helpers
{
    public class EncrypPassword : IEncrypPassword
    {
        public string Obtenermd5(string text)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(text);
            data = x.ComputeHash(data);
            string resp = "";
            for (int i = 0; i < data.Length; i++) 
            {
                resp += data[i].ToString("x2").ToLower();
            }
            return resp;
        }
    }
}
