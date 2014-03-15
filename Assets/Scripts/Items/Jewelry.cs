using UnityEngine;

public class Jewelry : BuffItem {
	private JewelrySlot _slot;				// Keep track of Jewelry in current slot

	public Jewelry() {
		_slot = JewelrySlot.PocketItems;
	}

	public Jewelry(JewelrySlot slot) {
		_slot = slot;
	}

	public JewelrySlot slot{
		get { return _slot; }
		set { _slot = value; }
	}
}

public enum JewelrySlot {					// Categories of existing Jewelry
	Earings,
	Necklaces,
	Bracelets,
	Rings,
	PocketItems
}