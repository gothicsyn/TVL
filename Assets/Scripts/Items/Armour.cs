using UnityEngine;

public class Armour : Clothing {
	private int _armourlevel; 				// The armour level of the equipped piece of Armour

	public Armour() {
		_armourlevel = 0;
	}

	public Armour(int al){
		_armourlevel = al;
	}

	public int ArmourLevel {
		get { return _armourlevel; }
		set { _armourlevel = value; }
	}
}
