using UnityEngine;

public class Clothing : BuffItem {
		private ArmourSlot _slot;				// Keep track of Armour in current slot
		
		public Clothing() {
			_slot = ArmourSlot.Torso;
		}
		
		public Clothing(ArmourSlot slot) {
			_slot = slot;
		}
		
		public ArmourSlot slot{
			get { return _slot; }
			set { _slot = value; }
		}
}

public enum ArmourSlot {
	Head,
	Shoulders,
	Chest,
	Arms, 
	Torso,
	Hands,
	Legs,
	Feet,
	Back
}