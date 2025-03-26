namespace PruebaHP.Model
{
    public class Personaje
    {
        public Personaje(int personajeID, string? nombre, string? apodo, string? imagen)
        {
            PersonajeID = personajeID;
            Nombre = nombre;
            Apodo = apodo;
            Imagen = imagen;
        }

        public Personaje() { }

        public int PersonajeID { get; set; }
        public string? Nombre { get; set; }
        public string? Apodo { get; set; }
        public string? Imagen { get; set; }
    }
}
