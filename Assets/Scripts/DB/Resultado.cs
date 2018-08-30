using UnityEngine;
using System.Collections;

public class Resultado {

	public Resultado (int id_resultado, string nombre, string subtitulo, string tipo, string directory, float escala)
	{
		this.id_resultado = id_resultado;
		this.nombre = nombre;
		this.subtitulo = subtitulo;
		this.tipo = tipo;
        this.directory = directory;
		this.escala = escala;
	}
	
	public int id_resultado {get; set;}
	public string nombre {get; set;}
	public string subtitulo {get; set;}
	public string tipo {get; set;}
    public string directory { get; set; }
	public float escala {get; set;}
}
