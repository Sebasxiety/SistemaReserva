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

        public List<string> ConsultarClientes()
        {
            string cadenaConsulta = "SELECT ClienteID, Nombre, Telefono, Email FROM Clientes";
            List<string> listaClientes = new List<string>();

            using (SqlConnection con = conexion.Conectarse())
            {
                SqlCommand cmd = new SqlCommand(cadenaConsulta, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string cliente = reader["ClienteID"].ToString() + " - "
                        + reader["Nombre"].ToString() + " - "
                        + reader["Telefono"].ToString() + " - "
                        + reader["Email"].ToString();
                    listaClientes.Add(cliente);
                }
            }

            return listaClientes;
        }
    }
}
