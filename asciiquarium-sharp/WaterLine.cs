using System;
namespace asciiquarium_sharp
{
	public class WaterLine
	{
		int WaterLineZ { get; set; }
		int WaterLineGap { get; set; }

		public WaterLine(int WaterLineZ, int WaterLineGap) {
			this.WaterLineZ = WaterLineZ;
			this.WaterLineGap = WaterLineGap;
	
		}
		
	}
}

