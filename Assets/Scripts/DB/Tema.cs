using System;
using System.Collections;
using System.Collections.Generic;

public class Tema {

	public Tema (int id_tema, string nombre, string texto1, string texto2, string texto3, string video, int tiempo1, int tiempo2, float escala, bool note, List<Modelo> modelos) {
		this.id_tema = id_tema;
		this.nombre = nombre;
		this.texto1 = texto1;
		this.texto2 = texto2;
        this.texto3 = texto3;
		this.video = video;
		this.tiempo1 = tiempo1;
		this.tiempo2 = tiempo2;
		this.escala = escala;
        this.note = note;
		this.modelos = modelos;
	}
	
	public int id_tema {get; set;}
	public string nombre {get; set;}
	public string texto1 {get; set;}
	public string texto2 {get; set;}
    public string texto3 { get; set; }
	public string video {get; set;}
	public int tiempo1 {get; set;}
	public int tiempo2 {get; set;}
	public float escala {get; set;}
    public bool note { get; set; }
	public List<Modelo> modelos {get; set;}
}