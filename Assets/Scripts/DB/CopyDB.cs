using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Diagnostics;
using MiniJSON;
using UnityEngine.SceneManagement;
//Top of the script
#pragma warning disable 0414

public class CopyDB : MonoBehaviour {
	
	private SQLiteDB db = null;
	public string dbfilename;
	private string log;
	public int versionDB;
	public string next_scene;
	public string ipServer;
	private string json_url;
	
	// Use this for initialization
	void Start(){
	#if UNITY_EDITOR
		ipServer = "localhost";
	#endif

		json_url = "http://" + ipServer + "/manar/company_json.php?date=";
		
		if(!File.Exists(Application.persistentDataPath + "/" + dbfilename)) {
			print ("No existe la base de datos");
			StartCoroutine(CopiaDB());
		}else{
			print("Ya existe la base de datos");
			//Checando si es la version correcta
			if(!checkVersion(versionDB))
			{
				//Borrando base de datos
				if(Directory.Exists(Application.persistentDataPath + "/Imagenes")){
					print ("existe la carpeta de imagenes");
					Directory.Delete(Application.persistentDataPath + "/Imagenes", true);
				}
				File.Delete(Application.persistentDataPath + "/" + dbfilename);
				//Copiando nueva base de datos
				StartCoroutine(CopiaDB());
			}else
                SceneManager.LoadScene(next_scene);
		}
        //StartCoroutine(CheckUpdate());
	}
	
	bool checkVersion(int version)
	{
		SQLiteDB db = new SQLiteDB();
		string filename = Application.persistentDataPath + "/" + dbfilename;
		int ver = 0;
		try{
			db.Open(filename);
			SQLiteQuery qr = null;
			try{
				qr = new SQLiteQuery(db, "SELECT ver_db FROM Version WHERE id = 1");
				qr.Step();
				ver = qr.GetInteger("ver_db");
				qr.Release();
			
			}catch(Exception e){
				print ("Error: " + e.ToString());
				if(qr != null){
					qr.Release();
					qr = null;
				}
			}
			
		}catch(Exception e){
			print ("Error: " + e.ToString());
			if(db != null){
				db.Close();
				db = null;
			}
		}
		
		db.Close();
		db = null;
		print ("VersionDB_Streaming: " + version + " VersionDB_Persistent: " + ver);
		
		if(ver == version)
			return true;
		else
			return false;
	}
	
	IEnumerator CopiaDB () {
		db = new SQLiteDB();
			
			log = "";

			byte[] bytes = null;				
			
			
#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
		string dbpath = "file://" + Application.streamingAssetsPath + "/" + dbfilename; log += "asset path is: " + dbpath;
		WWW www = new WWW(dbpath);
		yield return www;
		bytes = www.bytes;
#elif UNITY_WEBPLAYER
		string dbpath = "StreamingAssets/" + dbfilename;								log += "asset path is: " + dbpath;
		WWW www = new WWW(dbpath);
		yield return www;
		bytes = www.bytes;
#elif UNITY_IPHONE
		string dbpath = Application.dataPath + "/Raw/" + dbfilename;					log += "asset path is: " + dbpath;					
		try{	
			using ( FileStream fs = new FileStream(dbpath, FileMode.Open, FileAccess.Read, FileShare.Read) ){
				bytes = new byte[fs.Length];
				fs.Read(bytes,0,(int)fs.Length);
			}			
		} catch (Exception e){
			log += 	"\nTest Fail with Exception " + e.ToString();
			log += 	"\n";
		}
		yield return null;
#elif UNITY_ANDROID
		string dbpath = Application.streamingAssetsPath + "/" + dbfilename;	            log += "asset path is: " + dbpath;
		WWW www = new WWW(dbpath);
		yield return www;
		bytes = www.bytes;
#endif
		if ( bytes != null )
		{
			try{	
				
				string filename = Application.persistentDataPath + "/" + dbfilename;

				//
				//
				// copy database to real file into cache folder
				using( FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write) )
				{
					fs.Write(bytes,0,bytes.Length);             log += "\nCopy database from streaminAssets to persistentDataPath: " + filename;
				}
				
#if UNITY_IPHONE
				UnityEngine.iOS.Device.SetNoBackupFlag(filename);
#endif			
			} catch (Exception e){
				log += 	"\nTest Fail with Exception " + e.ToString();
				log += 	"\n\n Did you copy test.db into StreamingAssets ?\n";
			}
		}
				SceneManager.LoadScene(next_scene);
	}

    //IEnumerator CheckUpdate()
    //{
    //    DB db_access = GameObject.FindGameObjectWithTag("Backend").GetComponent<DB>();
    //    json_url += db_access.getDate("company");
    //    print(json_url);
    //    WWW www = new WWW(json_url);
    //    float elapsedTime = 0.0f;
    //    while (!www.isDone)
    //    {
    //        elapsedTime += Time.deltaTime;
    //        if (elapsedTime >= 10.0f) break;
    //        yield return null;
    //    }
    //    if (!www.isDone || !string.IsNullOrEmpty(www.error) || elapsedTime >= 10.0f)
    //    {
    //        if (string.IsNullOrEmpty(www.error))
    //            UnityEngine.Debug.LogWarning("Warning: too long to access");
    //        else
    //            UnityEngine.Debug.LogWarning("Warning: " + www.error);
    //        GameObject.FindGameObjectWithTag("Backend").GetComponent<DB>().lastScene = Application.loadedLevelName;
    //        Application.LoadLevelAsync(next_scene);
    //        yield return null;
    //    }
    //    else
    //    {
    //        string response = www.text;
    //        IDictionary update = (IDictionary)Json.Deserialize(response);
    //        //INSERTS
    //        if (update.Contains("insert"))
    //        {
    //            string date = update["date"].ToString();
    //            IList inserts = (IList)update["insert"];
    //            List<Company> list_companies = new List<Company>();
    //            foreach (IDictionary insert in inserts)
    //            {
    //                Company c = new Company((int)insert["id"], (string)insert["name"]);
    //                list_companies.Add(c);
    //            }
    //            db_access.updateCompanies(list_companies, date);
    //        }
    //        //DELETES
    //        if (update.Contains("delete"))
    //        {
    //            string date = update["date"].ToString();
    //            IList deletes = (IList)update["delete"];
    //            List<int> list_companies = new List<int>();
    //            foreach (IDictionary delete in deletes)
    //            {
    //                list_companies.Add((int)delete["id"]);
    //            }
    //            db_access.deleteCompanies(list_companies, date);
    //        }
    //        GameObject.FindGameObjectWithTag("Backend").GetComponent<DB>().lastScene = Application.loadedLevelName;
    //        Application.LoadLevelAsync(next_scene);
    //        yield return null;
    //    }
    //}
}