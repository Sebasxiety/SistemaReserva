using SistemaReserva.Datos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReserva.Logica
{
    internal class cls_Clientes
    {
        private readonly dto_Conexion conexion = new dto_Conexion();

        public void AgregarCliente(dto_Clientes cliente)
        {
            string cadenaConsulta = "INSERT INTO Clientes (Nombre, Telefono, Email) VALUES ('"
                + cliente.Nombre + "', '"
                + cliente.Telefono + "', '"
                + cliente.Email + "')";

            using (SqlConnection con = conexion.Conectarse())
            {
                SqlCommand cmd = new SqlCommand(cadenaConsulta, con);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void ActualizarCliente(int id, dto_Clientes clienteActualizado)
        {
            string cadenaConsulta = "UPDATE Clientes SET Nombre = '"
                + clienteActualizado.Nombre + "', Telefono = '"
                + clienteActualizado.Telefono + "', Email = '"
                + clienteActualizado.Email + "' WHERE ClienteID = " + id;

            using (SqlConnection con = conexion.Conectarse())
            {
                SqlCommand cmd = new SqlCommand(cadenaConsulta, con);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<dto_Clientes> ConsultarClientes()
        {
            string cadenaConsulta = "SELECT ClienteID, Nombre, Telefono, Email FROM Clientes";
            List<dto_Clientes> listaClientes = new List<dto_Clientes>();

            using (SqlConnection con = conexion.Conectarse())
            {
                SqlCommand cmd = new SqlCommand(cadenaConsulta, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    dto_Clientes cliente = new dto_Clientes
                    {
                        ClienteID = Convert.ToInt32(reader["ClienteID"]),
                        Nombre = reader["Nombre"].ToString(),
                        Telefono = reader["Telefono"].ToString(),
                        Email = reader["Email"].ToString()
                    };
                    listaClientes.Add(cliente);
                }
            }

            return listaClientes;
        }
    }
}
