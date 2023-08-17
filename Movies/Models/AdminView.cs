using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Movies.Models
{
    public class AdminView
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        /* Fill the "Data Source" field with the server name,
          * fill the "Initial Catalog" field with the data base name,
          * and fill the "Integrated Security" (write "true" if there is no password)*/
        private string cadena = "Data Source= ;Initial Catalog= ;Integrated Security= ";
        private void Conectar()
        {
            conexion = new SqlConnection(cadena);
        }
        public void Carga(Usuario user)
        {
            Conectar();
            comando = new SqlCommand("insert into vistas (id_usuarios,peliculas,periodo) values (@id_usuarios,@peliculas,@periodo)", conexion);
            comando.Parameters.Add("@id_usuarios", SqlDbType.Int);
            comando.Parameters.Add("@peliculas", SqlDbType.VarChar);
            comando.Parameters.Add("@periodo", SqlDbType.Date);
            comando.Parameters["@id_usuarios"].Value = user.id;
            comando.Parameters["@peliculas"].Value = user.vista;
            comando.Parameters["@periodo"].Value = user.fecha;
            conexion.Open();
            comando.ExecuteNonQuery();
            conexion.Close();
        }
        public Usuario Leer(int id)
        {
            Conectar();
            comando = new SqlCommand("select v.id_usuarios as id_usuarios, u.nombre as nombre, v.peliculas as peliculas, v.periodo as periodo from vistas v join usuarios u on v.id_usuarios = u.id where v.id=@id", conexion);
            comando.Parameters.Add("@id", SqlDbType.Int);
            comando.Parameters["@id"].Value = id;
            conexion.Open();
            SqlDataReader registro = comando.ExecuteReader();
            Usuario user = new Usuario();
            if (registro.Read())
            {
                user.id = int.Parse(registro["id_usuarios"].ToString());
                user.nombre = registro["nombre"].ToString();
                user.vista = registro["peliculas"].ToString();
                user.fecha = DateTime.Parse(registro["periodo"].ToString());
                user.id2 = id;
            }
            conexion.Close();
            return user;
        }
        public Usuario Confirmar(int id)
        {
            Conectar();
            comando = new SqlCommand("select nombre from usuarios where id=@id_usuarios", conexion);
            comando.Parameters.Add("@id_usuarios", SqlDbType.Int);
            comando.Parameters["@id_usuarios"].Value = id;
            conexion.Open();
            SqlDataReader registro = comando.ExecuteReader();
            Usuario user = new Usuario();
            if (registro.Read())
            {
                user.nombre = registro["nombre"].ToString();
                user.id = id;
            }
            conexion.Close();
            return user;
        }
        public List<Usuario> LeerTodo()
        {
            Conectar();
            List<Usuario> lista = new List<Usuario>();
            comando = new SqlCommand("select v.id as id, u.id as id_usuario, u.nombre as usuario, v.peliculas as pelicula, v.periodo as periodo from vistas v join usuarios u on v.id_usuarios = u.id", conexion);
            conexion.Open();
            SqlDataReader registros = comando.ExecuteReader();
            while (registros.Read())
            {
                Usuario user = new Usuario
                {
                    id2 = int.Parse(registros["id"].ToString()),
                    id = int.Parse(registros["id_usuario"].ToString()),
                    nombre = registros["usuario"].ToString(),
                    vista = registros["pelicula"].ToString(),
                    fecha = DateTime.Parse(registros["periodo"].ToString()),
                };
                lista.Add(user);
            }
            conexion.Close();
            return lista;
        }
        public void Modificar(Usuario user)
        {
            Conectar();
            comando = new SqlCommand("update vistas set id_usuarios=@id_usuarios,peliculas=@peliculas,periodo=@periodo where id=@id", conexion);
            comando.Parameters.Add("@id", SqlDbType.Int);
            comando.Parameters.Add("@id_usuarios", SqlDbType.Int);
            comando.Parameters.Add("@peliculas", SqlDbType.VarChar);
            comando.Parameters.Add("@periodo", SqlDbType.Date);
            comando.Parameters["@id"].Value = user.id2;
            comando.Parameters["@id_usuarios"].Value = user.id;
            comando.Parameters["@peliculas"].Value = user.vista;
            comando.Parameters["@periodo"].Value = user.fecha;
            conexion.Open();
            comando.ExecuteNonQuery();
            conexion.Close();
        }
        public void Borrar(int id)
        {
            Conectar();
            comando = new SqlCommand("delete from vistas where id = @id", conexion);
            comando.Parameters.Add("@id", SqlDbType.Int);
            comando.Parameters["@id"].Value = id;
            conexion.Open();
            comando.ExecuteNonQuery();
            conexion.Close();
        }
    }
}
