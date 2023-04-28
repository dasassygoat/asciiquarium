/*#############################################################################
# Asciiquarium - An aquarium animation in ASCII art
#
# This program displays an aquarium/sea animation using ASCII art.
#
# The current version of this program wll be available at:
#
# http://coderbox.co
#
#############################################################################
# Author:
#   Bryan Pritchett <bryan@coderbox.co> 
#   
#
# Contributors:
#   Kirk Baucom <kbaucom@schizoid.com>
#     PERL version of the code
#
#   Joan Stark: http://www.geocities.com/SoHo/7373/
#     most of the ASCII art
#
#   Claudio Matsuoka <cmatsuoka@gmail.com>
#     improved marine biodiversity (backported from the Asciiquarium Live
#     Wallaper for Android)
#     https://market.android.com/details?id=org.helllabs.android.asciiquarium
#
# License:
#
# Copyright (C) 2023 Bryan Pritchett (bryan@coderbox.co)
#
# This program is free software; you can redistribute it and/or
# modify it under the terms of the GNU General Public License
# as published by the Free Software Foundation; either version 2
# of the License, or (at your option) any later version.
#
# This program is distributed in the hope that it will be useful,
# but WITHOUT ANY WARRANTY; without even the implied warranty of
# MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
# GNU General Public License for more details.
#
# You should have received a copy of the GNU General Public License
# along with this program; if not, write to the Free Software
# Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
############################################################################ */

using System;
using System.Drawing;
using asciiquarium_sharp.ascii_art;

namespace asciiquarium_sharp;
class Program
{
    string version = "1.1";

    static void Main(string[] args)
    {
        //Console.Clear();
        DisplayCursorPositionSomewhereElse();

        DisplayZero();

        bool optC = false;
        bool newFish = true;
        bool newMonster = true;

        var startTime = DateTime.Now;
        bool paused = false;

        Screen screen = new Screen();
        if(args.Any())
            optC = Getopts(args, 'C');

        if (optC)
        { //'classic; mode
            newFish = false;
            newMonster = false;
        }

        var randomObjects = InitRandomObjects();

        var depth = new ZDepth(0,1,2,3,20,21,22,new WaterLine(2,3),new WaterLine(4,5), new WaterLine(6,7), new WaterLine(8,9));

        bool run = false;
        while (run)
        {

            AddEnvironment(screen);
            AddCastle(screen);
            AddAllSeaweed(screen);
            AddAllFish(screen, newFish);
            AddRandomOject(randomObjects,screen);
            screen.Redraw();
            
            Console.WriteLine(WaterAscii.WATER_LINE_SEGMENT[0]);
            Console.WriteLine(WaterAscii.WATER_LINE_SEGMENT[1]);
            Console.WriteLine(WaterAscii.WATER_LINE_SEGMENT[2]);
            Console.WriteLine(WaterAscii.WATER_LINE_SEGMENT[3]);
            
            int nexttime = 0;

            while (true)
            {
                var input = Console.ReadKey().KeyChar;

                if (input == 'q')
                {
                    break;
                }
                else if (input == 'r')
                {
                    screen.Redraw();
                }
                else if (input == 'p')
                {
                    paused = !paused;
                }

                //screen.animate() if !paused
            }

            screen.RemoveAllEntities();
            break;
        }

        CleanupScreen();
    }

    private static void CleanupScreen()
    {
        Console.ResetColor();
    }

    private static void DisplayZero()
    {
        var consoleWidth = Console.WindowWidth;
        var consoleHeight = Console.WindowHeight;
        var title = "Welcome";
        Console.SetCursorPosition((consoleWidth/2) - title.Length/2, (consoleHeight /2));
        Console.Write(title);
        var pos = Console.GetCursorPosition();
        Console.WriteLine($" {pos}\n");
    }

    private static void DisplayCursorPositionSomewhereElse()
    {
        Console.SetCursorPosition(5, 5);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("RED ROSE");
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.WriteLine("Purple Petals");
        var pos = Console.GetCursorPosition();
        Console.WriteLine(pos);
        Thread.Sleep(10000);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Orange Orangutan");
        pos = Console.GetCursorPosition();
        Console.WriteLine(pos);
        Thread.Sleep(5000);
        Console.Clear();
        var centerWidth = Console.WindowWidth/2;
        var centerHeight = Console.WindowHeight/2;
        Console.SetCursorPosition(centerWidth, centerHeight);
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("blue bonnets");
        pos = Console.GetCursorPosition();
        Console.WriteLine(pos);
    }

    private static void AddAllFish(Screen screen, bool NewFish)
    {
//figure out how many fish to add by the size of the screen,
//minus the stuff above the water
        var  screenSize = (Console.WindowHeight - 9) * Console.WindowWidth;
        var fishCount = screenSize / 350;
        for (int x = 1; x <=fishCount;x++)
        {
            AddFish(NewFish);
        }
    }

    private static void AddFish(bool new_fish)
    {
        Random rnd = new Random();
        if (new_fish) {
            if (rnd.Next(12) > 8)
            {
                AddNewFish();
            }
            else {
                AddOldFish();
	        }
	    }
        else {
            AddOldFish();
	    }
    }

    private static void AddOldFish()
    {
        
    }

    private static void AddNewFish()
    {
        
    }

    private static void AddAllSeaweed(Screen screen)
    {
        //figure out how many seaweed to add by the width of the screen
        var seaweedCount = Console.WindowWidth / 15;

        for (int x = 1; x < seaweedCount; x++)
        {
            AddSeaweed();
        }
    }

    private static void AddSeaweed()
    {
        Random rnd = new Random(); ;
        string[] seaweedImage = new string[]{"( \n"," )\n"}; //may want to move it to a file under ascii_art folder
        var height = rnd.Next(4) + 3;
        
        for(int x =1; x <= height; x++) {
            bool leftSide, rightSide;
            leftSide = Convert.ToBoolean(x % 2);
            rightSide = !leftSide;
            
            seaweedImage[leftSide?1:0] += "(\n"; //string concat
            seaweedImage[rightSide?1:0] += " )\n"; //string concat

	    }

        int randomX = rnd.Next(Console.WindowWidth - 2) + 1;
        int randomY = Console.WindowHeight - height;
        Random randomSpeed = new Random();
        double animationSpeed = (randomSpeed.Next(5) + .25)/100;
        Entity seaweedEntity = new Entity();
        seaweedEntity.Name = "seaweed" + rnd.Next(1);
        //seaweedEntity.Shape = seaweedImage;
        seaweedEntity.Position = "[x,y,depth('seaweed')]";
        //seaweedEntity.CallbackArgs = [0, 0, 0, animationSpeed];
        //seaweedEntity.DieTime = rnd.Next(4 * 60) + (8 * 60); //seaweed lives for 8 to 12 minutes
        seaweedEntity.DeathCb = "add_seaweed";
        seaweedEntity.DefaultColor = Color.Green;
        Console.WriteLine(seaweedImage[0]);
        Console.WriteLine($"\n{seaweedEntity.Name}\n");
        Console.WriteLine(seaweedImage[1]);

    }


    private static void AddCastle(Screen screen)
    {
        Entity ent = new Entity();
        ent.Name = "catle";
        ent.Shape = CastleAscii.CASTLE_IMAGE;
        ent.Color = CastleAscii.CASTLE_MASK;
        ent.Position = "width -32, height - 13, depth('castle')";
        ent.DefaultColor = Color.Black;
    }

    private static void AddEnvironment(Screen screen)
    {
        var segmentLength = WaterAscii.WATER_LINE_SEGMENT[0].Length;
        var segmentRepeat = (Console.WindowWidth / segmentLength); //in perl version 1 is being padded to the repeat
        Console.WriteLine(Console.WindowWidth);
        for (int x = 0; x < WaterAscii.WATER_LINE_SEGMENT.Length;x++ )
        {
            var unModifiedLine = WaterAscii.WATER_LINE_SEGMENT[x];
            for (int r = segmentRepeat; r > 1; r--)
            {
                WaterAscii.WATER_LINE_SEGMENT[x] += unModifiedLine;
            }
        }

        Entity ent = new Entity();
        int i = 0; //temporary until I figure out what the foreach is cycling through
        ent.Name = "water_seg_" + i;
        ent.Type = AquariumObjectTypes.WaterLine;
        ent.Shape = WaterAscii.WATER_LINE_SEGMENT[i];//this is an array I think
        ent.Position = "[0,i+5,depth(\"water_line\" . i)]"; //something like this is used for setting the position
        ent.DefaultColor = Color.Cyan;
        ent.Depth = 22;
        ent.Physical = 1;

    }

    private static void AddRandomOject(List<AquariumObjectTypes> random_objects, Screen screen)
    {
        //todo: not currently random addition
        random_objects.Add(AquariumObjectTypes.BigFish);
        screen.UpdateObjects(random_objects);
    }

    private static List<AquariumObjectTypes> InitRandomObjects()
    {
        var randomListOfObjects = new List<AquariumObjectTypes>
        {
            AquariumObjectTypes.Ship,
            AquariumObjectTypes.Whale,
            AquariumObjectTypes.Monster,
            AquariumObjectTypes.BigFish,
            AquariumObjectTypes.Shark
        };
        return randomListOfObjects;
    }

    private static bool Getopts(string[] args, char v)
    {
        foreach (var i in args[0])
        {
            if (char.ToUpper(i) == v) { return true; }
        }

        return false;
    }
}
