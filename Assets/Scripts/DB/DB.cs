using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class DB : MonoBehaviour {
	
	public string db_nombre = "base_datos.db";
	public SQLiteDB db = null;
	string filename;
	
	void Awake () {
		filename = Application.persistentDataPath + "/" + db_nombre;
#if UNITY_EDITOR
		print (filename);
#endif
		db = new SQLiteDB();
	}

    public void setCameraDevice(int numCamera)
    {
        //1: back camera
        //2: front camera
        string query = "UPDATE Version SET camara = ? WHERE id = 1";
        try
        {
            db.Open(filename);
            SQLiteQuery qr = null;
            try
            {
                qr = new SQLiteQuery(db, query);
                qr.Bind(numCamera);
                qr.Step(); //Save int
                qr.Release();
            }
            catch (Exception e)
            {
                if (qr != null)
                    qr.Release();
                Debug.LogError(e.ToString());
            }
        }
        catch (Exception e)
        {
            db.Close();
            Debug.LogError(e.ToString());
        }
        finally
        {
            db.Close();
        }
    }

    public int getCameraDevice()
    {
        string query = "SELECT camara FROM Version WHERE id = 1";
        int res = 0; //default value for error
        try
        {
            db.Open(filename); // Abriendo base de datos
            SQLiteQuery qr = null;
            try
            {
                qr = new SQLiteQuery(db, query);
                qr.Step();
                res = qr.GetInteger("camara");
                qr.Release();
            }
            catch (Exception e)
            {
                if (qr != null)
                    qr.Release();
                Debug.LogError(e.ToString());
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.ToString());
            db.Close();
        }
        finally
        {
            db.Close();
        }

        //1: back, 2: front, 0: can't get it the value
        return res;
    }
	
	public List<Resultado> getMaterias ()
	{
		string query = "SELECT id_materia, nombre, carpeta FROM Materia";
		string tipo = "materia";
		
		try{
			db.Open(filename);  //Abriendo base de datos
		}catch(IOException){
			Debug.LogError ("No se pudo abrir la base de datos correctamente");
		}catch(Exception){
			Debug.LogError ("base de datos ya abierta");
			db.Close(); //cerrando
			db.Open(filename); //abriendo de nuevo
		}
		
		List<Resultado> lista_resultados = new List<Resultado>();
		
		try{
			SQLiteQuery qr =new SQLiteQuery(db, query);
			while(qr.Step()) {
				int id_materia = qr.GetInteger("id_materia");
				string nombre = qr.GetString("nombre");
                string directory = qr.GetString("carpeta");
				Resultado resultado = new Resultado(id_materia, nombre, "", tipo, directory, 1);
				lista_resultados.Add(resultado);
			}
			qr.Release();
		}catch(Exception ex){
			Debug.LogError ("Error query: " + ex);
		}finally{
			db.Close();
		}
		return lista_resultados;
	}
	
	public List<Resultado> getTemasTipo(int id_materia, int id_padre = -1)
	{
		string query;
		if(id_padre == -1) {
            query = @"SELECT id_tematipo, nombre, carpeta
								FROM TemaTipo 
								WHERE id_padre IS NULL AND id_materia = ?";
		}else{
            query = @"SELECT id_tematipo, nombre, carpeta
								FROM TemaTipo 
								WHERE id_padre = ? AND id_materia = ?";
		}
		
		string tipo = "tema_tipo";
		
		try{
			db.Open(filename);  //Abriendo base de datos
		}catch(Exception e){
			Debug.LogError ("Error: " + e.ToString());
			if(db != null){
				db.Close(); //cerrando
				db = null;
			}
			db.Open(filename); //intentando abrir de nuevo la base de datos
		}
		
		List<Resultado> lista_resultados = new List<Resultado>();
		
		try{
            SQLiteQuery qr = new SQLiteQuery(db, query);
            if (id_padre == -1)
                qr.Bind(id_materia);
            else
            {
                qr.Bind(id_padre);
                qr.Bind(id_materia);
            }
			while(qr.Step()) {
				int id_tematipo = qr.GetInteger("id_tematipo");
				string nombre = qr.GetString("nombre");
                string directory = qr.GetString("carpeta");
				Resultado resultado = new Resultado(id_tematipo, nombre, "", tipo, directory, 1);
				lista_resultados.Add(resultado);
			}
			qr.Release();
		}catch(Exception ex){
			Debug.LogError ("Error query: " + ex);
		}finally{
			db.Close();
		}
		
		return lista_resultados;
	}
	
	public List<Resultado> getTemas (int id_tematipo)
	{
        string query = @"SELECT t.id_tema, t.nombre, tt.subtitulo AS subtitulo, t.escala
									FROM Tema t INNER JOIN TemaTipo tt ON tt.id_tematipo = t.id_tematipo
									WHERE t.id_tematipo = ?";
		string tipo = "tema";
		
		try{
			db.Open(filename);  //Abriendo base de datos
		}catch(IOException){
			Debug.LogError ("No se pudo abrir la base de datos correctamente");
		}catch(Exception){
			Debug.LogError ("base de datos ya abierta");
			db.Close(); //cerrando
			db.Open(filename); //abriendo de nuevo
		}
		
		List<Resultado> lista_resultados = new List<Resultado>();
		
		try{
			SQLiteQuery qr =new SQLiteQuery(db, query);
            qr.Bind(id_tematipo);
			while(qr.Step()) {
				int id_tema = qr.GetInteger("id_tema");
				string nombre = qr.GetString("nombre");
				string subtitulo = qr.GetString("subtitulo");
				float escala = (float) qr.GetDouble("escala");
				Resultado resultado = new Resultado(id_tema, nombre, subtitulo, tipo, "", escala);
				lista_resultados.Add(resultado);
			}
			qr.Release();
		}catch(Exception ex){
			Debug.LogError ("Error query: " + ex);
		}finally{
			db.Close();
		}
		
		return lista_resultados;
	}
	
	public Tema getTemaDetail (int id_tema)
	{
        string query = @"SELECT nombre, texto1, texto2, texto3, video, btn1_text, btn2_text, btn3_text, btn4_text, tiempo1, tiempo2, escala, nota
						FROM Tema
						WHERE id_tema = ?";
		
		try{
			db.Open(filename);  //Abriendo base de datos
		}catch(Exception e){
			Debug.LogError ("Error: " + e.ToString());
			db.Close(); //cerrando
			db.Open(filename); //abriendo de nuevo
		}
		Tema tema = null;
		try{
			SQLiteQuery qr =new SQLiteQuery(db, query);
            qr.Bind(id_tema);
            if (qr.Step())
            {
                string nombre = qr.GetString("nombre");
                string texto1 = qr.GetString("texto1");
                string texto2 = qr.GetString("texto2");
                string texto3 = qr.GetString("texto3");
                string video = qr.GetString("video");
                string[] btn_text = new string[4];
                btn_text[0] = qr.GetString("btn1_text");
                btn_text[1] = qr.GetString("btn2_text");
                btn_text[2] = qr.GetString("btn3_text");
                btn_text[3] = qr.GetString("btn4_text");
                int tiempo1 = qr.GetInteger("tiempo1");
                int tiempo2 = qr.GetInteger("tiempo2");
                float escala = (float)qr.GetDouble("escala");
                bool note = qr.GetInteger("nota") == 1? true : false;

                qr.Release();
                //Obteniendo los modelos del Tema/Experiencia
                query = @"SELECT m.btn_num, m.archivo, m.estatico, m.texto, m.megafiers
								FROM Tema_Modelo tm INNER JOIN Modelo m
								ON tm.id_modelo = m.id_modelo
								WHERE tm.id_tema = ?";
                List<Modelo> lista_modelos = new List<Modelo>();

                qr = new SQLiteQuery(db, query);
                qr.Bind(id_tema);
                while (qr.Step())
                {
                    int num_btn = qr.GetInteger("btn_num");
                    string archivo = qr.GetString("archivo");
                    string estatico = qr.GetString("estatico");
                    string texto = qr.GetString("texto");
                    bool megafiers = qr.GetInteger("megafiers") == 1 ? true : false;
                    Modelo modelo = new Modelo(num_btn, btn_text[num_btn - 1], archivo, estatico, texto, megafiers);
                    lista_modelos.Add(modelo);
                }
                //Setting up Tema
                tema = new Tema(id_tema, nombre, texto1, texto2, texto3, video, tiempo1, tiempo2, escala, note, lista_modelos);
            }
            else
                tema = null; //In case doesn't return results
			qr.Release();
			
		}catch(Exception ex){
			Debug.LogError("Error query: " + ex);
		}finally{
			db.Close();
		}
		return tema;
	}
	
	//Obteniendo nombre del subtema de experiencias
	public string getNombre (int id_tematipo) {
        string query = @"SELECT nombre 
									FROM TemaTipo 
									WHERE id_tematipo = ?";
		string nombre = "";
		
		try{
			db.Open(filename); //Abriendo base de datos
			SQLiteQuery qr = null;
			try{
				qr = new SQLiteQuery(db, query);
                qr.Bind(id_tematipo);
				qr.Step();
				nombre = qr.GetString("nombre");
				qr.Release();
			}catch(Exception e){
				Debug.LogError ("Error: " + e.ToString());
				if(qr != null){
					qr.Release();
					qr = null;
				}
			}
		}catch(Exception e){
			Debug.LogError ("Error: " + e.ToString());
			if(db != null){
				db.Close();
				db = null;
			}
		}finally{
			db.Close();
		}
		return nombre;
	}
	
	//Obteniendo subtitulo del subtema de experiencias
	public string getSubtitulo (int id_tematipo) {
        string query = @"SELECT subtitulo 
									FROM TemaTipo 
									WHERE id_tematipo = ?";
		string subtitulo = "";
		
		try{
			db.Open(filename); //Abriendo base de datos
			SQLiteQuery qr = null;
			try{
				qr = new SQLiteQuery(db, query);
                qr.Bind(id_tematipo);
				qr.Step();
				subtitulo = qr.GetString("subtitulo");
				qr.Release();
			}catch(Exception e){
				Debug.LogError ("Error: " + e.ToString());
				if(qr != null){
					qr.Release();
					qr = null;
				}
			}
		}catch(Exception e){
			Debug.LogError ("Error: " + e.ToString());
			if(db != null){
				db.Close();
				db = null;
			}
		}finally{
			db.Close();
		}
		return subtitulo;
	}
	
	//Obteniendo nombre imagen para experiencias
	public Imagen getImagen(int id_tematipo) {
        string query = @"SELECT imagen, imagen_escala
								FROM TemaTipo 
								WHERE id_tematipo = ?";
		
		try{
			db.Open(filename);  //Abriendo base de datos
		}catch(IOException){
			Debug.LogError ("No se pudo abrir la base de datos correctamente");
		}catch(Exception){
			Debug.LogError ("base de datos ya abierta");
			db.Close(); //cerrando
			db.Open(filename); //abriendo de nuevo
		}
		
		string archivo = "";
		float escala = 0.0f;
		Imagen imagen;
		try{
			SQLiteQuery qr =new SQLiteQuery(db, query);
            qr.Bind(id_tematipo);
			qr.Step();
			archivo = qr.GetString("imagen");
			escala = (float)qr.GetDouble("imagen_escala");
			qr.Release();
		}catch(Exception ex){
			Debug.LogError ("Error query: " + ex);
		}finally{
			db.Close();
		}
		imagen = new Imagen(archivo, escala);
		return imagen;
	}
}
