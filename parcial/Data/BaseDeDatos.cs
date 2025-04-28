using Microsoft.EntityFrameworkCore;
using parcial.Models;
using System.Data.SqlClient;


namespace parcial.Data
{
	public class BaseDeDatos
	{
		SqlConnection conn = new SqlConnection("Data Source = DESKTOP-Q6T9G0H\\SQLEXPRESS01;Initial Catalog=hospital; Integrated Security=True;");
		public List<Paciente> ListarPacientes()
		{
			List<Paciente> Pacientes = new List<Paciente>();

			string query = "select Pacientes.Nombre, ObrasSociales.Nombre as ObraSocial, Pacientes.Edad, Pacientes.Sintomas from Pacientes JOIN ObrasSociales ON Pacientes.IdObraSocial = ObrasSociales.Id";
			conn.Open();
			SqlCommand comand = new SqlCommand(query, conn);
			SqlDataReader reader = comand.ExecuteReader();

			while (reader.Read())
			{
				ObraSocial obraSocial = new ObraSocial();
				Paciente Paciente = new Paciente();

				Paciente.Nombre = reader.GetString(0);
				obraSocial.Nombre = reader.GetString(1);
				Paciente.Edad = reader.GetInt32(2);
				Paciente.Sintomas = reader.GetString(3);
				Paciente.ObraSocial = obraSocial;


				Pacientes.Add(Paciente);

			}
			conn.Close();
			return Pacientes;
		}



		public void CrearPaciente(Paciente paciente)
		{
			string query = $"insert into Pacientes (Nombre, IdObraSocial, Edad, Sintomas) values ('{paciente.Nombre}', {paciente.IdObraSocial}, {paciente.Edad}, '{paciente.Sintomas}')";

			Console.Write(query);
			conn.Open();
			SqlCommand comand = new SqlCommand(query, conn);
			comand.ExecuteNonQuery();
			conn.Close();
		}
		public class Context : DbContext
		{


			protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
			{
				optionsBuilder.UseSqlServer(@"Data Source = DESKTOP-Q6T9G0H\SQLEXPRESS01;Initial Catalog=hospital; Integrated Security=True; TrustServerCertificate=True;");
			}


			public DbSet<ObraSocial> ObrasSociales { get; set; }

			public DbSet<Paciente> Pacientes { get; set; }
		}
	}
}
