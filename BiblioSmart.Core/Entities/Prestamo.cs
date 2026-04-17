namespace BiblioSmart.Core.Entities
{
    public class Prestamo
    {
        public int Id { get; set; }
        public int LibroId { get; set; }
        public Libro Libro { get; set; } = null!;
        public int MiembroId { get; set; }
        public Miembro Miembro { get; set; } = null!;
        public DateTime FechaPrestamo { get; set; } = DateTime.Now;
        public DateTime FechaDevolucionEsperada { get; set; }
        public DateTime? FechaDevolucionReal { get; set; }
        public string Estado { get; set; } = "Activo";
    }
}
