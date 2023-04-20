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

using System.Drawing;
using asciiquarium_sharp.ascii_art;

namespace asciiquarium_sharp;
class Program
{
    string version = "1.1";

    static void Main(string[] args)
    {
        bool opt_c = false;
        int new_fish = 1;
        int new_monster = 1;

        var start_time = DateTime.Now;
        bool paused = false;

        Screen screen = new Screen();
        if(args.Any())
            opt_c = getopts(args, 'C');

        if (opt_c)
        { //'classic; mode
            new_fish = 0;
            new_monster = 0;
        }

        var random_objects = initRandomObjects();

        var depth = new ZDepth(0,1,2,3,20,21,22,new WaterLine(2,3),new WaterLine(4,5), new WaterLine(6,7), new WaterLine(8,9));
        

        while (true)
        {

            addEnvironment(screen);
            //add_castle(screen);
            //add_all_seaweed(screen);
            //add_all_fish(screen);
            addRandomOject(random_objects,screen);
            screen.redraw();
            
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
                    screen.redraw();
                }
                else if (input == 'p')
                {
                    paused = !paused;
                }

                //screen.animate() if !paused
            }

            screen.removeAllEntities();
        }
        Console.WriteLine(WaterAscii.WATER_LINE_SEGMENT[0]);
    }

    private static void addEnvironment(Screen screen)
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

    private static void addRandomOject(List<AquariumObjectTypes> random_objects, Screen screen)
    {
        //todo: not currently random addition
        random_objects.Add(AquariumObjectTypes.BigFish);
        screen.updateObjects(random_objects);
    }

    private static List<AquariumObjectTypes> initRandomObjects()
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

    private static bool getopts(string[] args, char v)
    {
        foreach (var i in args[0])
        {
            if (char.ToUpper(i) == v) { return true; }
        }

        return false;
    }
}
