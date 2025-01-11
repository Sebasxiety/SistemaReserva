using SistemaReserva.Datos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReserva.Logica
{
    internal class cls_Reservaciones
    {
        private readonly dto_Conexion conexion = new dto_Conexion();

        public void AgregarReservacion(dto_Reservaciones reservacion)
        {
            string cadenaConsulta = "INSERT INTO Reservaciones (MesaID, ClienteID, FechaHora, NumeroPersonas) VALUES ("
                + reservacion.MesaID + ", "
                + reservacion.ClienteID + ", '"
                + reservacion.FechaHora + "', "
                + reservacion.NumeroPersonas + ")";

            using (SqlConnection con = conexion.Conectarse())
            {
                SqlCommand cmd = new SqlCommand(cadenaConsulta, con);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void ActualizarReservacion(int id, dto_Reservaciones reservacionActualizada)
        {
            string cadenaConsulta = "UPDATE Reservaciones SET MesaID = "
                + reservacionActualizada.MesaID + ", ClienteID = "
                + reservacionActualizada.ClienteID + ", FechaHora = '"
                + reservacionActualizada.FechaHora + "', NumeroPersonas = "
                + reservacionActualizada.NumeroPersonas + " WHERE ReservacionID = " + id;

            using (SqlConnection con = conexion.Conectarse())
            {
                SqlCommand cmd = new SqlCommand(cadenaConsulta, con);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<dto_Reservaciones> Listar()
        {
            string cadenaConsulta = "SELECT ReservacionID, MesaID, ClienteID, FechaHora, NumeroPersonas FROM Reservaciones";
            List<dto_Reservaciones> listaReservaciones = new List<dto_Reservaciones>();

            using (SqlConnection con = conexion.Conectarse())
            {
                SqlCommand cmd = new SqlCommand(cadenaConsulta, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    dto_Reservaciones reservacion = new dto_Reservaciones
                    {
                        ReservacionID = Convert.ToInt32(reader["ReservacionID"]),
                        MesaID = Convert.ToInt32(reader["MesaID"]),
                        ClienteID = Convert.ToInt32(reader["ClienteID"]),
                        FechaHora = Convert.ToDateTime(reader["FechaHora"]),
                        NumeroPersonas = Convert.ToInt32(reader["NumeroPersonas"])
                    };
                    listaReservaciones.Add(reservacion);
                }
            }

            return listaReservaciones;
        }
    }
}
