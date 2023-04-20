using System;
namespace asciiquarium_sharp
{
	public class ZDepth
	{
		int GuiText { get; set; }
		int Gui { get; set; }
		int Shark { get; set; }
		int FishStart { get; set; }
		int FishEnd { get; set; }
		int Seaweed { get; set; }
		int Castle { get; set; }

		WaterLine WaterLine3 { get;set; }
		WaterLine WaterLine2 { get; set; }
		WaterLine WaterLine1 { get; set; }
		WaterLine WaterLine0 { get; set; }

		public ZDepth(int GuiText,int Gui, int Shark, int FishStart, int FishEnd, int Seaweed,
			int Castle, WaterLine WaterLine3, WaterLine WaterLine2, WaterLine WaterLine1, 
			WaterLine WaterLine0) 
		{
			this.GuiText = GuiText;
			this.Gui = Gui;
			this.Shark = Shark;
			this.FishStart = FishStart;
			this.FishEnd = FishEnd;
			this.Seaweed = Seaweed;
			this.Castle = Castle;
			this.WaterLine3 = WaterLine3;
			this.WaterLine2 = WaterLine2;
			this.WaterLine1 = WaterLine1;
			this.WaterLine0 = WaterLine0;
		}
	}
}

