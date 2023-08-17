using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Movies.Models
{
    public class AdminUser
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        /* Fill the "Data Source" field with the server name,
         * fill the "Initial Catalog" field with the data base name,
         * and fill the "Integrated Security" (write "true" if there is no password)*/
        private string cadena = "Data Source= ;Initial Catalog= ;Integrated Security=";
        private void Conectar()
        {
            conexion = new SqlConnection(cadena);
        }
        public void Carga(Usuario user)
        {
            Conectar();
            comando = new SqlCommand("insert into usuarios (nombre,fechanac,genero) values (@nombre,@fechanac,@genero)", conexion);
            comando.Parameters.Add("@nombre", SqlDbType.VarChar);
            comando.Parameters.Add("@fechanac", SqlDbType.Date);
            comando.Parameters.Add("@genero", SqlDbType.VarChar);
            comando.Parameters["@nombre"].Value = user.nombre;
            comando.Parameters["@fechanac"].Value = user.fecha;
            comando.Parameters["@genero"].Value = user.genero;
            conexion.Open();
            comando.ExecuteNonQuery();
            conexion.Close();
        }
        public Usuario Leer(int id)
        {
            Conectar();
            comando = new SqlCommand("select nombre,fechanac,year(fechanac) as agno,month(fechanac) as mes, day(fechanac) as dia,genero from usuarios where id=@id", conexion);
            comando.Parameters.Add("@id", SqlDbType.Int);
            comando.Parameters["@id"].Value = id;
            conexion.Open();
            Usuario user = new Usuario();
            SqlDataReader registro = comando.ExecuteReader();
            if (registro.Read())
            {
                user.nombre = registro["nombre"].ToString();
                user.genero = registro["genero"].ToString();
                user.fecha = DateTime.Parse(registro["fechanac"].ToString());
                user.id = id;
            }
            conexion.Close();
            return user;
        }
        public Usuario LeerName(string name)
        {
            Conectar();
            comando = new SqlCommand("select id, fechanac, genero from usuarios where nombre=@nombre", conexion);
            comando.Parameters.Add("@nombre", SqlDbType.VarChar);
            comando.Parameters["@nombre"].Value = name;
            conexion.Open();
            Usuario user = new Usuario();
            SqlDataReader registro = comando.ExecuteReader();
            if (registro.Read())
            {
                user.id = int.Parse(registro["id"].ToString());
                user.genero = registro["genero"].ToString();
                user.fecha = DateTime.Parse(registro["fechanac"].ToString());
                user.nombre = name;
            }
            conexion.Close();
            return user;
        }
        public List<Usuario> LeerTodo()
        {
            Conectar();
            List<Usuario> lista = new List<Usuario>();
            comando = new SqlCommand("select id,nombre,fechanac,genero from usuarios", conexion);
            conexion.Open();
            SqlDataReader registros = comando.ExecuteReader();
            while (registros.Read())
            {
                Usuario user = new Usuario
                {
                    id = int.Parse(registros["id"].ToString()),
                    nombre = registros["nombre"].ToString(),
                    fecha = DateTime.Parse(registros["fechanac"].ToString()),
                    genero = registros["genero"].ToString(),
                };
                lista.Add(user);
            }
            conexion.Close();
            return lista;
        }
        public void Modificar(Usuario user)
        {
            Conectar();
            comando = new SqlCommand("update usuarios set nombre=@nombre,genero=@genero,fechanac=@fechanac where id=@id", conexion);
            comando.Parameters.Add("@id", SqlDbType.Int);
            comando.Parameters.Add("@nombre", SqlDbType.VarChar);
            comando.Parameters.Add("@genero", SqlDbType.VarChar);
            comando.Parameters.Add("@fechanac", SqlDbType.Date);
            comando.Parameters["@id"].Value = user.id;
            comando.Parameters["@nombre"].Value = user.nombre;
            comando.Parameters["@genero"].Value = user.genero;
            comando.Parameters["@fechanac"].Value = user.fecha;
            conexion.Open();
            comando.ExecuteNonQuery();
            conexion.Close();
        }
    }
}
