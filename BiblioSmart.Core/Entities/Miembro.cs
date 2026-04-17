namespace BiblioSmart.Core.Entities
{
    public class Miembro
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string Cedula { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
    }
}
