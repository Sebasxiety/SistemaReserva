using SistemaReserva.Datos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReserva.Logica
{
    internal class cls_Mesas
    {
        private readonly dto_Conexion conexion = new dto_Conexion();

        public void AgregarMesa(dto_Mesas mesa)
        {
            string cadenaConsulta = "INSERT INTO Mesas (NumeroMesa, Capacidad, Ubicacion) VALUES ("
                + mesa.NumeroMesa + ", "
                + mesa.Capacidad + ", '"
                + mesa.Ubicacion + "')";

            using (SqlConnection con = conexion.Conectarse())
            {
                SqlCommand cmd = new SqlCommand(cadenaConsulta, con);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void ActualizarMesa(int id, dto_Mesas mesaActualizada)
        {
            string cadenaConsulta = "UPDATE Mesas SET NumeroMesa = "
                + mesaActualizada.NumeroMesa + ", Capacidad = "
                + mesaActualizada.Capacidad + ", Ubicacion = '"
                + mesaActualizada.Ubicacion + "' WHERE MesaID = " + id;

            using (SqlConnection con = conexion.Conectarse())
            {
                SqlCommand cmd = new SqlCommand(cadenaConsulta, con);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<string> ConsultarMesas()
        {
            string cadenaConsulta = "SELECT MesaID, NumeroMesa, Capacidad, Ubicacion FROM Mesas";
            List<string> listaMesas = new List<string>();

            using (SqlConnection con = conexion.Conectarse())
            {
                SqlCommand cmd = new SqlCommand(cadenaConsulta, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string mesa = reader["MesaID"].ToString() + " - Mesa "
                        + reader["NumeroMesa"].ToString() + " - Capacidad: "
                        + reader["Capacidad"].ToString() + " - Ubicación: "
                        + reader["Ubicacion"].ToString();
                    listaMesas.Add(mesa);
                }
            }

            return listaMesas;
        }
    }
}
