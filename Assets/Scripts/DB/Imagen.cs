using System.Collections;

public class Imagen {

	public Imagen (string archivo, float escala) {
		this.archivo = archivo;
		this.escala = escala;
	}
	
	public string archivo {get; set;}
	public float escala {get; set;}
}
