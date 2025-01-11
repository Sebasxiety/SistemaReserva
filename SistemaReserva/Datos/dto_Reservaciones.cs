using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReserva.Datos
{
    internal class dto_Reservaciones
    {
        public int ReservacionID { get; set; }
        public int MesaID { get; set; }
        public int ClienteID { get; set; }
        public DateTime FechaHora { get; set; }
        public int NumeroPersonas { get; set; }
    }
}
