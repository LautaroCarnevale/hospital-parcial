namespace parcial.Models
{
	public class Paciente
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public string IdObraSocial { get; set; }
		public int Edad { get; set; }
		public string Sintomas { get; set; }
		public ObraSocial? ObraSocial { get; set; }

	}
}
