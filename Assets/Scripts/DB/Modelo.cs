using System.Collections;

public enum Animacion {
	None = 0,
	Once = 1,
	Loop = 2,
	PingPong = 3
};

public class Modelo {
	
	public Modelo (int num_btn, string btn_text, string archivo, string estatico, string texto, bool megafiers) {
		this.num_btn = num_btn;
		this.btn_text = btn_text;
		this.archivo = archivo;
        this.estatico = estatico;
		this.texto = texto;
        this.megafiers = megafiers;
	}
	
	public int num_btn {get; set;}
	public string btn_text {get; set;}
	public string archivo {get; set;}
	public string estatico {get; set;}
	public string texto {get; set;}
    public bool megafiers { get; set; }
}
