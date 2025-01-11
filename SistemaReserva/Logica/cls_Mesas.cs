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


        public List<dto_Mesas> Listar()
        {
            string cadenaConsulta = "SELECT MesaID, NumeroMesa, Capacidad, Ubicacion FROM Mesas";
            List<dto_Mesas> listaMesas = new List<dto_Mesas>();

            using (SqlConnection con = conexion.Conectarse())
            {
                SqlCommand cmd = new SqlCommand(cadenaConsulta, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    dto_Mesas mesa = new dto_Mesas
                    {
                        MesaID = Convert.ToInt32(reader["MesaID"]),
                        NumeroMesa = Convert.ToInt32(reader["NumeroMesa"]),
                        Capacidad = Convert.ToInt32(reader["Capacidad"]),
                        Ubicacion = reader["Ubicacion"].ToString()
                    };
                    listaMesas.Add(mesa);
                }
            }

            return listaMesas;
        }
    }
}
