﻿using UnityEngine;

public class Item {
	private string _name;
	private int _value;
	private RarityType _rarity;
	private int _curDur;
	private int _maxDur;
	
	public Item() {
		_name = "Require Name";
		_value = 0;
		_rarity = RarityType.common;
		_maxDur = 50;
		_curDur = _maxDur;
	}
	
	public Item(string name, int value,  RarityType rare, int maxDur, int curDur){
		_name = name;
		_value = value;
		_rarity = rare;
		_maxDur = maxDur;
		_curDur = curDur;
	}
	
	public string Name {
		get{ return _name; }
		set{ _name = value; }
	}
	
	public int Value {
		get{ return _value; }
		set{ _value = value; }
	}
	
	public RarityType Rarity {
		get { return _rarity; }
		set { _rarity = value; }
	}
	
	public int MaxDurability {
		get { return _maxDur; }
		set { _maxDur = value; }
	}
	
	public int CurDurability {
		get { return _curDur; }
		set { _curDur = value; }
	}
}

public enum RarityType {
	common,
	Uncommon,
	Rare
}