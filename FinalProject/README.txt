****problems****

-null reference thing at the start that automatically pause the game,
the game still run fine if you unpause the game

-enemy goes weird if you let him follow you down the slope
 

------------------------------------------------
Dynamic NavMesh 

-press o to open/close door, if door open while game running, 
player and enemy can go through the path

-----------------------------------------
Anim  

-Player - blendtree (idle,walk,run),enter h to wave, enter 
space to shoot bullets)

-Enemy - simple animations like walk, can crouch to pass object in its way 
when walking toward a waypoint

-enemy will dodge bullet (will crouch down)
(try shoot bullets behind him while he is patrolling)

-------------------------------------------
Network 

-same as last CA, movement and bullet shots sync in network screen 

--------------------------------------------
AI

-enemy walks toward one waypoint to the next, when see player,
enemy will go towards player

-enemy goes back to patrol state if player is away from him(using onTriggerExit)
(try close the door and block the enemy while he follows you, 
enemy will go back to patrol state)




---------------------------------------------
Other things

-mountain - baked navmesh so that player can't walk up to the higher part of
mountain
(click on navigation tap you can see the navmesh,
part covered in blue means can walk on, white means cant walk on)

-bullet shots - will disappear if collide to gameobjects (Using OnCollisionEnter)

-Health Bar