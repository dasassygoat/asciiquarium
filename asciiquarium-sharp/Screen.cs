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

namespace asciiquarium_sharp
{
    public class Screen
    {
        private List<AquariumObjectTypes> aquariumObjectTypes;

        public Screen() {
            this.aquariumObjectTypes = new List<AquariumObjectTypes>();
	    }
         public Screen(List<AquariumObjectTypes> aquariumObjectTypes){
            this.aquariumObjectTypes = aquariumObjectTypes;
         }

        public void redraw()
        {
            Console.WriteLine("Redraw," + aquariumObjectTypes.Any());
        }

        public void updateObjects(List<AquariumObjectTypes> aquariumObjectTypes) {
            this.aquariumObjectTypes = aquariumObjectTypes;
	    }

        public void removeAllEntities()
        {
            aquariumObjectTypes = null;
        }
    }
}