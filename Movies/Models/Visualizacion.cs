using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Movies.Models
{
    public class Visualizacion
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
        public List<Usuario> LeerTodo()
        {
            Conectar();
            List<Usuario> lista = new List<Usuario>();
            comando = new SqlCommand("select u.nombre, DATEDIFF(YEAR,FechaNac,GETDATE()) - (CASE WHEN DATEADD(YY, DATEDIFF(YEAR, u.FechaNac, GETDATE()), u.FechaNac) > GETDATE() THEN 1 ELSE 0 END) as Edad, " +
                "u.genero, v.periodo as periodo, v.peliculas from usuarios u, vistas v where u.id = v.id_usuarios order by v.periodo asc",
                conexion);
            conexion.Open();
            SqlDataReader registros = comando.ExecuteReader();
            while (registros.Read())
            {
                Usuario user = new Usuario
                {
                    nombre = registros["nombre"].ToString(),
                    edad = int.Parse(registros["Edad"].ToString()),
                    genero = registros["genero"].ToString(),
                    fecha = DateTime.Parse(registros["periodo"].ToString()),
                    vista = registros["peliculas"].ToString(),
                };
                lista.Add(user);
            }
            conexion.Close();
            return lista;
        }
        public List<Usuario> CountPeriod()
        {
            Conectar();
            List<Usuario> lista = new List<Usuario>();
            comando = new SqlCommand("Select year(periodo) as agno, month(periodo) as mes, count(peliculas) as cantidad from vistas " +
                "group by year(periodo), month(periodo) order by agno, mes Asc", conexion);
            conexion.Open();
            SqlDataReader registros = comando.ExecuteReader();
            while (registros.Read())
            {
                string j = registros["mes"].ToString();
                int i = int.Parse(registros["mes"].ToString());
                if (i < 10)
                {
                    j = String.Concat(0, i);
                }
                string k = registros["agno"].ToString();
                Usuario user = new Usuario
                {
                    periodo = String.Concat(j, "/", k),
                    pelis = int.Parse(registros["cantidad"].ToString()),
                };
                lista.Add(user);
            }
            conexion.Close();
            return lista;
        }
        public List<Usuario> CountAge()
        {
            Conectar();
            List<Usuario> lista = new List<Usuario>();
            comando = new SqlCommand("Select DATEDIFF(YEAR,FechaNac,GETDATE()) " +
            "- (CASE WHEN DATEADD(YY, DATEDIFF(YEAR, usuarios.FechaNac, GETDATE()), usuarios.FechaNac) > GETDATE() " +
            "THEN 1 ELSE 0 END) as Edad, " +
            "count(vistas.Peliculas) as cantidad " +
            "from usuarios, vistas " +
            "where usuarios.id = vistas.id_usuarios " +
            "group by DATEDIFF(YEAR, FechaNac, GETDATE()) - (CASE WHEN DATEADD(YY, DATEDIFF(YEAR, FechaNac, GETDATE()),FechaNac)> GETDATE() THEN 1 ELSE 0 END) " +
            "order by DATEDIFF(YEAR, FechaNac, GETDATE()) - (CASE WHEN DATEADD(YY, DATEDIFF(YEAR, FechaNac, GETDATE()),FechaNac)> GETDATE() THEN 1 ELSE 0 END)",
                conexion);
            conexion.Open();
            SqlDataReader registros = comando.ExecuteReader();
            while (registros.Read())
            {
                Usuario user = new Usuario
                {
                    edad = int.Parse(registros["Edad"].ToString()),
                    pelis = int.Parse(registros["Cantidad"].ToString()),
                };
                lista.Add(user);
            }
            conexion.Close();
            return lista;
        }
        public List<Usuario> CountGenre()
        {
            Conectar();
            List<Usuario> lista = new List<Usuario>();
            comando = new SqlCommand("Select usuarios.genero as genero, count(vistas.peliculas) as cantidad from " +
                "usuarios,vistas group by usuarios.genero", conexion);
            conexion.Open();
            SqlDataReader registros = comando.ExecuteReader();
            while (registros.Read())
            {
                Usuario user = new Usuario
                {
                    genero = registros["genero"].ToString(),
                    pelis = int.Parse(registros["cantidad"].ToString()),
                };
                lista.Add(user);
            }
            conexion.Close();
            return lista;
        }
        public List<Usuario> CountGenrePeriod()
        {
            Conectar();
            List<Usuario> lista = new List<Usuario>();
            comando = new SqlCommand("Select year(vistas.Periodo) as agno, month(vistas.Periodo) as mes, usuarios.genero as genero, " +
                "count(vistas.peliculas) as cantidad from " +
                "usuarios, vistas group by usuarios.genero, year(vistas.Periodo), month(vistas.Periodo) order by agno, mes", conexion);
            conexion.Open();
            SqlDataReader registros = comando.ExecuteReader();
            while (registros.Read())
            {
                string j = registros["mes"].ToString();
                int i = int.Parse(registros["mes"].ToString());
                if (i < 10)
                {
                    j = String.Concat(0, i);
                }
                string k = registros["agno"].ToString();
                Usuario user = new Usuario()
                {
                    periodo = String.Concat(j, "/", k),
                    genero = registros["genero"].ToString(),
                    pelis = int.Parse(registros["cantidad"].ToString()),
                };
                lista.Add(user);
            }
            conexion.Close();
            return lista;
        }
    }
}
