# RTS
Real time strategy game created in unity, with main focus on stearing, responsive units and group behaviours. I tried to replicate Starcraft II mechanics

# code
## RtsController
is responsive for selecting and controlling player's units
## Unit Movement
controlls movement of unit, can be called from PlayerInput script or EnemyAI. when unit is ordered to move to a position NavMesh agent is used. If multiple units try to get to one point there is a problem because units can't overlap. Script make unit stop if it get to the other unit at destination position.
## Fraction
Every entity needs a fraction for finding targets for attack and group steering. Also assign color to each unit
## Unit Health
Component for units that could be attacked and destroyed. Update health bar and destoys Entity if health value goes below zero.
## Unit Attack
Checks if there are units in range and create projectile if so. Ignores units from own fraction
## Unit Spawning
If building is selected, fraction has enough resources (gold) add unit to a queue. Spawns unit if there is one on the queue with set delay.
## Blueprint Script
Move blueprint object to mouse position. Checks if you can place building and set green or red color to object. If mouse is pressed and object doesn't colide with anythigh swap blueprint to actual object
## Selectable
Allows units to be selected by the player and highlight model.