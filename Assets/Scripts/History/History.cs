using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class History : MonoBehaviour {
	
	private List<Package> packageList = new List<Package>();
	private List<string> nameList = new List<string>();

    public List<GameObject> references = new List<GameObject>();
    public List<Texture> backgrounds = new List<Texture>();
    //public GameObject ar_camera;
    //public GameObject target_cuchilla;
    //public GameObject target_modelo;
    //public GameObject luces;

    public Vector2 getScreenSize()
    {
        UITexture background = getReference("Navigation").transform.Find("Background").GetComponent<UITexture>();
        Vector2 size = new Vector2(background.width, background.height);
        return size;
    }

    public void changeBackground(int index)
    {
        getReference("Navigation").transform.Find("Background").GetComponent<UITexture>().mainTexture = backgrounds[index];
    }

    public GameObject getReference(string nameReference)
    {
        return references.Find(item => item.transform.name == nameReference);
    }

	public int getLength()
	{
        return packageList.Count;
	}
	public int hasElement(string nombre)
	{
        return nameList.IndexOf(nombre);
	}
	
	public Package getPackage(int index){
        if (index < 0)
            index = packageList.Count + index;
        return packageList[index];
	}
	
	public string getName(int index){
        if (index < 0)
            index = nameList.Count + index;
        return nameList[index];
	}
	
    /// <summary>
    /// Add the package to history
    /// </summary>
    /// <param name="package"></param>
    /// <param name="nombre"></param>
	public int addPackage(Package package, string nombre){
        packageList.Add(package);
        nameList.Add(nombre);
        return packageList.Count; //return the number of packages
	}

    /// <summary>
    /// Remove package from history
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public int removePackage(int pos)
    {
        if (pos < 0)
        {
            try
            {
                packageList.RemoveAt(packageList.Count + pos);
                nameList.RemoveAt(nameList.Count + pos);
            }
            catch   {   Debug.LogError("Wrong index on list");  }
        }
        else
        {
            try
            {
                packageList.RemoveAt(pos);
                nameList.RemoveAt(pos);
            }
            catch   {   Debug.LogError("Wrong index on list");  }
        }
        return packageList.Count;
    }

    /// <summary>
    /// Add and move package the determined position
    /// </summary>
    /// <param name="package"></param>
    /// <param name="name"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    public int addPackageMove(Package package, string name)
    {
        int num; //Number of objects on the history list

        //Resseting the tweenmanager
        if (GetComponent<TweenManager>())
            Destroy(GetComponent<TweenManager>());
        TweenManager tweenManager = gameObject.AddComponent<TweenManager>(); //Agregando a la lista de animaciones los tweens

        //Check if already exist a package on history
        if (getLength() > 0)
        {
            Package ultimo_package = getPackage(-1);

            //Check LAST PACKAGE
            //Check if has just one tween position
            if (ultimo_package.tween != null)
            {
                EventDelegate.Add(ultimo_package.tween.onFinished, DisableObject, true); //Adding disable gameobject event when finish to move, just one shot
                tweenManager.moveLeft = new List<TweenPosition>();
                tweenManager.moveLeft.Add(ultimo_package.tween);
            }
            //Check if has a list of tweens
            else if (ultimo_package.tweens != null)
            {
                //Adding disable gameobject event when finish to move, just one shot
                foreach (TweenPosition tween in ultimo_package.tweens)
                    EventDelegate.Add(tween.onFinished, DisableObject, true);
                tweenManager.moveLeft = ultimo_package.tweens;
            }
            else
                Debug.LogError("Last package error with tweens");


            //NEXT PACKAGE
            num = addPackage(package, name); //Adding package to the list

            //Check the next package
            if (package.tween != null)
                tweenManager.moveLeft.Add(package.tween);
            else if (package.tweens != null)
                tweenManager.moveLeft.AddRange(package.tweens);
            else
            {
                Debug.LogError("Problem with tween on added package");
                removePackage(-1); //Reverting the to the last state
            }
        }
        else
        {
            num = addPackage(package, name); //Agregando package a la lista
            //Check the next package
            if (package.tween != null)
            {
                tweenManager.moveLeft = new List<TweenPosition>();
                tweenManager.moveLeft.Add(package.tween);
            }
            else if (package.tweens != null)
                tweenManager.moveLeft.AddRange(package.tweens);
            else
            {
                Debug.LogError("Problem with tween on added package");
                removePackage(-1); //Reverting the to the last state
            }
        }

        tweenManager.playTweens(); //Play the tweens
        return num;
    }

    public int removePackageMove()
    {
        TweenManager tweenManager;
        int num;
        //Checando si existe algun TweenManager previo, para eliminar
        if (gameObject.GetComponent<TweenManager>())
            Destroy(GetComponent<TweenManager>());
        tweenManager = gameObject.AddComponent<TweenManager>();

        if (getLength() > 1)
        {
            Package penultimo_package = getPackage(-2);
            Package ultimo_package = getPackage(-1);
            num = removePackage(-1);
            //NGUITools.SetActive(ultimo_package.padre, false);
            //ultimo_package.padre.transform.localPosition = new Vector3(2000f, 0, 0);
            NGUITools.SetActive(penultimo_package.padre, true); //Activando el penultimo objeto

            //Animando a la derecha el penultimo
            if (penultimo_package.tweens != null)
                tweenManager.moveRight = ultimo_package.tweens;
            else
            {
                tweenManager.moveRight = new List<TweenPosition>();
                tweenManager.moveRight.Add(penultimo_package.tween);
            }

            if (ultimo_package.tweens != null)
            {
                //Agregando DisablePanel Delegate one shot
                foreach (TweenPosition tween in ultimo_package.tweens)
                    EventDelegate.Add(tween.onFinished, DisableObject, true);
                tweenManager.moveRight.AddRange(ultimo_package.tweens);
            }
            else
            {
                //Agregando DisablePanel Delegate one shot
                EventDelegate.Add(ultimo_package.tween.onFinished, DisableObject, true);
                tweenManager.moveRight.Add(ultimo_package.tween);
            }
            tweenManager.playTweens();
        }
        else
        {
            Package ultimo_package = getPackage(-1);
            num = removePackage(-1);

            //Agregando DisablePanel Delegate one shot
            foreach (TweenPosition tween in ultimo_package.tweens)
                EventDelegate.Add(tween.onFinished, DisableObject, true);

            //Animando a la derecha el penultimo
            if (ultimo_package.tweens != null)
            {
                foreach (TweenPosition tween in ultimo_package.tweens)
                    EventDelegate.Add(tween.onFinished, DisableObject, true);
                tweenManager.moveRight = ultimo_package.tweens;
            }
            else
            {
                //Agregando DisablePanel Delegate one shot
                EventDelegate.Add(ultimo_package.tween.onFinished, DisableObject, true);
                tweenManager.moveRight = new List<TweenPosition>();
                tweenManager.moveRight.Add(ultimo_package.tween);
            }
            tweenManager.playTweens();
        }

        return num; //return num of elements on history list
    }

    private void DisableObject()
    {
        NGUITools.SetActive(TweenPosition.current.gameObject, false);
    }
	
//    public void addElementMove (Package package, string nombre, int tipo) {
//        //tipo = 0 normal
//        //tipo = 1 experiencias
//        //tipo = 2 RA
		
//        //Borrando ultima animacion
//            if(gameObject.GetComponent<TweenMenus>() != null)
//                Destroy(GetComponent<TweenMenus>());
//            TweenMenus tween = gameObject.AddComponent<TweenMenus>();
		
//        //ajustando escala
//        float aspect = (float) Screen.width/Screen.height;
//        UIAnchor[] anchores = package.padre.GetComponentsInChildren<UIAnchor>();
//        foreach(UIAnchor anchor in anchores)
//            anchor.gameObject.transform.localScale = new Vector3(aspect/1.6f, aspect/1.6f, 1);
//        //Si ya hay un elemento obten el elemento anterior
//        if(historyList.Count > 0){
//            //Si ya estaba un menu de experiencias cargado y se agrega otro de experiencias
//            int index = hasElement("MenuExperiencias");
//            if(tipo == 1 && index > -1) {
//                Package p = historyList[index];
//                historyList.RemoveAt(index); //Removiendo de la lista de historial el panel de experiencias previo
//                nombreList.RemoveAt(index); //Sacando de la lista de nombres el panel de experiencias previo
//                Destroy(p.padre); //Eliminando padre
//                Resources.UnloadUnusedAssets(); //liberando memoria
//                //Agregando paquete nuevo
//                historyList.Add(package);
//                nombreList.Add(nombre);
//                tween.pos = new float[1];
//                tween.pos[0] = 450;//(450.0f * Mathf.Max(factor_width1,factor_width2)); //(450.0f * aspect) / 1.6f;
//                tween.moveLeft = new GameObject[1];
//                tween.moveLeft[0] = package.panelTween;
//                //Reseteando el color de los botones a blanco
//                UIButton[] scripts_buttons = historyList[historyList.Count-2].padre.GetComponentsInChildren<UIButton>();
//                foreach(UIButton boton_script in scripts_buttons){
//                    boton_script.defaultColor = Color.white;
//                    boton_script.UpdateColor(true, false);
//                }
//            }else if(tipo == 1 && index == -1) { //No habia otro panel de experiencia previo, agregando el panel de experiencia nuevo
//                Package penultimo_package = historyList[historyList.Count-1];
//                historyList.Add(package);
//                nombreList.Add(nombre);
//                tween.pos = new float[2];
//                tween.pos[0] = 220.0f;// * Mathf.Min(factor_width1,factor_width2));// / 1.6f;
//                tween.pos[1] = 450;//(450.0f * Mathf.Max(factor_width1,factor_width2));// / 1.6f;
//                tween.moveLeft = new GameObject[2];
//                tween.moveLeft[0] = penultimo_package.panelTween;
//                tween.moveLeft[1] = package.panelTween;
//                //Reseteando el color de los botones a blanco
//                UIButton[] scripts_buttons = historyList[historyList.Count-2].padre.GetComponentsInChildren<UIButton>();
//                foreach(UIButton boton_script in scripts_buttons){
//                    boton_script.defaultColor = Color.white;
//                    boton_script.UpdateColor(true, false);
//                }
//            }else if(tipo == 2){
//                Package penultimo_package = historyList[historyList.Count-1];
//                historyList.Add(package);
//                nombreList.Add(nombre);
//                int count = package.panelesTween.Count + 1;
//                tween.pos = new float[count];
//                tween.moveLeft = new GameObject[count];
//                for(int i=0; i< count-1; i++){
//                    tween.pos[i] = 1280.0f;
//                    tween.moveLeft[i] = package.panelesTween[i];
//                }
//                tween.moveLeft[count-1] = penultimo_package.panelTween; //agregando el penultimo package para moverlo
//                tween.pos[count-1] = 1280.0f;
				
//            }else{//Agregando menu normal
//                Package penultimo_package = historyList[historyList.Count-1];
//                historyList.Add(package);
//                nombreList.Add(nombre);
//                tween.pos = new float[2];
//                tween.pos[0] = 1280.0f;
//                tween.pos[1] = 1280.0f;
//                tween.moveLeft = new GameObject[2];
//                tween.moveLeft[0] = penultimo_package.panelTween;
//                tween.moveLeft[1] = package.panelTween;
//            }
//        }else{
//            //Agregando package
//            historyList.Add(package);
//            nombreList.Add(nombre);
//            tween.moveLeft = new GameObject[1];
//            tween.pos = new float[1];
//            tween.pos[0] = 1280.0f;//Screen.width * Mathf.Max(factor_width1,factor_width2);
//            tween.moveLeft[0] = package.panelTween;
//        }
//        //Activo Animacion
//        tween.OnClick();
//    }
	
	
	
//    public void removeLastElementMove ()  {
//        //Limpiando TweenMenus
//        if(gameObject.GetComponent<TweenMenus>() != null)
//                Destroy(GetComponent<TweenMenus>());
//        TweenMenus tween = gameObject.AddComponent<TweenMenus>();
		
//        int num = historyList.Count;
//        //Checar si hay minimo 2 menus
//        if(num >= 2)
//        {
//            num--; //moviendome al ultimo elemento
//            string nombre = nombreList[num];
//            Package package_ultimo = historyList[num];
//            Package package_penultimo = historyList[num-1];
//            historyList.RemoveAt(num);
//            nombreList.RemoveAt(num);
//            if(nombre == "Menu_RA"){
////				NGUITools.SetActive(ar_camera, false);
////				NGUITools.SetActive(target_cuchilla, false);
////				NGUITools.SetActive(target_modelo, false);
////				NGUITools.SetActive(luces, false);
//                //target_modelo.GetComponent<ModelController>().setModelo(Resources.Load("models/texto") as GameObject, "");
//                int count = package_ultimo.panelesTween.Count;
//                tween.moveRight = new GameObject[count+1];
//                tween.pos = new float[count+1];
				
//                for(int i=0; i<count-1; i++){
//                    tween.moveRight[i] = package_ultimo.panelesTween[i];
//                    tween.pos[i] = 1280.0f;
//                }
//                tween.moveRight[count-1] = package_penultimo.panelTween;
//                tween.pos[count-1] = 1280.0f;
//                tween.moveRight[count] = historyList[num-2].panelTween;
//                tween.pos[count] = 1280.0f;
//            }else{
//                tween.moveRight = new GameObject[1];
//                tween.pos = new float[1];
				
//                //Cambiando fondo
//                if(nombreList[num-1] == "MenuPrincipal")
//                    GameObject.Find("PanelFondo").GetComponentInChildren<UISprite>().spriteName = "background2";
				
//                if(nombre == "MenuExperiencias"){
//                    tween.pos[0] = 220;
//                    //Reseteando el color de los botones a blanco
//                    UIButton[] scripts_buttons = package_penultimo.padre.GetComponentsInChildren<UIButton>();
//                    foreach(UIButton boton_script in scripts_buttons){
//                        boton_script.defaultColor = Color.white;
//                        boton_script.UpdateColor(true, false);
//                    }
//                }
//                else
//                    tween.pos[0] = 1280.0f;
				
//                //mover a la derecha
//                tween.moveRight[0] = package_penultimo.panelTween;
//            }
//            //Destruyendo los elemento padre
//            Destroy(package_ultimo.padre);
//            Resources.UnloadUnusedAssets();
//        }else{
//            Application.LoadLevel("portada_scene");
//        }
//        tween.OnClick();//Activando animacion
//    }
	
//	public void goTo(string nombre)
//	{
//		int num = hasElement(nombre);
//		if(num != -1)
//		{
//			if(num != historyList.Count-1){
//				for(int i = historyList.Count-1; i > num; i--)
//					removeLastElement();
//				Resources.UnloadUnusedAssets();
//				moveToRight(historyList[num].objsAnimacion,1);
//			}
//		}
//	}
	
//	public void goToMain()
//	{
//		//Checar si que no este en el menu principal
//		int num = nombreList.Count - 1;
//		if(nombreList[num] != "MenuPrincipal")
//		{
//			for(int i = num; i > 1; i--)
//				removeLastElement();
//			Resources.UnloadUnusedAssets();
//			moveToRight(historyList[1].objsAnimacion,1);
//		}
//	}
	
//	public void removeToMain()
//	{
//		//Moviendose a la pantalla principal
//		int num = hasElement("MenuPrincipal");
//		//Checar que este en el historial
//		if(num != -1)
//		{
//			if(num != historyList.Count-1){
//				for(int i = historyList.Count-1; i > num; i--)
//					removeLastElement();
//				Resources.UnloadUnusedAssets();
//			}
//		}
//	}
	
//	public void moveToRight(GameObject[] moveRight, float pos )
//	{
//		if(gameObject.GetComponent<TweenMenus>() != null)
//			Destroy(GetComponent<TweenMenus>());
//		TweenMenus tween = gameObject.AddComponent<TweenMenus>();
//		tween.pos = pos;
//		tween.moveRight = new GameObject[moveRight.Length];
//		for(int i=0; i<moveRight.Length; i++)
//			tween.moveRight[i] = moveRight[i];
//		tween.OnClick();
//	}
//	
//	public void moveToLeft(GameObject[] moveLeft, float pos)
//	{
//		if(gameObject.GetComponent<TweenMenus>() != null)
//			Destroy(GetComponent<TweenMenus>());
//		TweenMenus tween = gameObject.AddComponent<TweenMenus>();
//		tween.pos = pos;
//		tween.moveLeft = new GameObject[moveLeft.Length];
//		for(int i=0; i<moveLeft.Length; i++)
//			tween.moveLeft[i] = moveLeft[i];
//		tween.OnClick();
//	}
}
