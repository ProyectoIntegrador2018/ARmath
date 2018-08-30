using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Package {
	
	public Package(GameObject padre, TweenPosition tween){
		this.padre = padre;
		this.tween = tween;
	}
	
	public Package(GameObject padre, List<TweenPosition> tweens){
		this.padre = padre;
		this.tweens = tweens;
	}
	
	public GameObject padre {get; set;}
    public TweenPosition tween { get; set; }
    public List<TweenPosition> tweens { get; set; }
}
