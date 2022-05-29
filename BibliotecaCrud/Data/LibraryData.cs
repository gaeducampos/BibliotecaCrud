using BibliotecaCrud.Models;
using System.Data.SqlClient;
using System.Data;
namespace BibliotecaCrud.Data
{
    public class LibraryData
    {

        public bool isRegister;
        public string Message;
        public List<LibraryModel> addToList()
        {
            var listObject = new List<LibraryModel>();

            var connection = new Conecction();

            using (var newConnection = new SqlConnection(connection.getStringSql()))
            {
                newConnection.Open();
                SqlCommand cmd = new SqlCommand("sp_Listar", newConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        listObject.Add(new LibraryModel() {
                            IdBook = Convert.ToInt32(dr["Id"]),
                            Title = dr["Titulo"].ToString(),
                            Category = dr["Categoria"].ToString(),
                            Author = dr["Autor"].ToString(),
                            Editorial = dr["Editorial"].ToString(),
                            Amount = Convert.ToInt32(dr["Cantidad"])
                        });
                    }
                }
            }

            return listObject;
        }



        public LibraryModel getBookData(int IdBook)
        {
            var getObjectLibrary = new LibraryModel();

            var connection = new Conecction();

            using (var newConnection = new SqlConnection(connection.getStringSql()))
            {
                newConnection.Open();
                SqlCommand cmd = new SqlCommand("sp_Obtener", newConnection);
                cmd.Parameters.AddWithValue("Id", IdBook);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        getObjectLibrary.IdBook = Convert.ToInt32(dr["Id"]);
                        getObjectLibrary.Title = dr["Titulo"].ToString();
                        getObjectLibrary.Category = dr["Categoria"].ToString();
                        getObjectLibrary.Author = dr["Autor"].ToString();
                        getObjectLibrary.Editorial = dr["Editorial"].ToString();
                        getObjectLibrary.Amount = Convert.ToInt32(dr["Cantidad"]);
                    
                    }
                }
            }

            return getObjectLibrary;
        }

        public bool save(LibraryModel libraryObject)
        {
            bool isValid;

            try
            {
                var connection = new Conecction();

                using (var newConnection = new SqlConnection(connection.getStringSql()))
                {
                    newConnection.Open();
                    SqlCommand cmd = new SqlCommand("sp_Guardar", newConnection);
                    cmd.Parameters.AddWithValue("Titulo", libraryObject.Title);
                    cmd.Parameters.AddWithValue("Categoria", libraryObject.Category);
                    cmd.Parameters.AddWithValue("Autor", libraryObject.Author);
                    cmd.Parameters.AddWithValue("Editorial", libraryObject.Editorial);
                    cmd.Parameters.AddWithValue("Cantidad", libraryObject.Amount);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                }

                isValid = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                isValid = false;
            }

            return isValid;
        }


        public bool Edit(LibraryModel libraryObject)
        {
            bool isValid;

            try
            {
                var connection = new Conecction();

                using (var newConnection = new SqlConnection(connection.getStringSql()))
                {
                    newConnection.Open();
                    SqlCommand cmd = new SqlCommand("sp_Editar", newConnection);
                    cmd.Parameters.AddWithValue("Id", libraryObject.IdBook);
                    cmd.Parameters.AddWithValue("Titulo", libraryObject.Title);
                    cmd.Parameters.AddWithValue("Categoria", libraryObject.Category);
                    cmd.Parameters.AddWithValue("Autor", libraryObject.Author);
                    cmd.Parameters.AddWithValue("Editorial", libraryObject.Editorial);
                    cmd.Parameters.AddWithValue("Cantidad", libraryObject.Amount);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                }

                isValid = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                isValid = false;
            }

            return isValid;
        }


        public bool delete(int IdBook)
        {
            bool isValid;
            try
            {
                var connection = new Conecction();

                using (var newConnection = new SqlConnection(connection.getStringSql()))
                {
                    newConnection.Open();
                    SqlCommand cmd = new SqlCommand("sp_customDelete", newConnection);
                    cmd.Parameters.AddWithValue("Id", IdBook);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                }

                isValid = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                isValid = false;
            }

            return isValid;
        }


        public void RegisterStoredProcedure(User userObject)
        {
            var connection = new Conecction();

            using (var newConnection = new SqlConnection(connection.getStringSql()))
            {
                newConnection.Open();
                SqlCommand cmd = new SqlCommand("sp_RegisterUser", newConnection);
                cmd.Parameters.AddWithValue("Email", userObject.Email);
                cmd.Parameters.AddWithValue("UserPassword", userObject.Password);
                cmd.Parameters.Add("IsRegister", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("Message", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();

                isRegister = Convert.ToBoolean(cmd.Parameters["IsRegister"].Value);
                Message = cmd.Parameters["Message"].Value.ToString();
            }
        }


        public void LogInStoredProcedure(User userObject)
        {
            var connection = new Conecction();

            using (var newConnection = new SqlConnection(connection.getStringSql()))
            {
                newConnection.Open();
                SqlCommand cmd = new SqlCommand("sp_UsuarioValido", newConnection);
                cmd.Parameters.AddWithValue("Email", userObject.Email);
                cmd.Parameters.AddWithValue("UserPassword", userObject.Password);
                cmd.CommandType = CommandType.StoredProcedure;

                userObject.Id = Convert.ToInt32(cmd.ExecuteScalar());

                

            }
        }


    }
}
