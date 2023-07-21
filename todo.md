#### next
- sometimes need to rotate to regain control?
- fix jumping
  - move it to Hoverboard? or access latched state from CharController?
  - make jump detach from surface
- jittery rotation on curves?
  - a large hoverboard collider improves smoothness of normal calculation, but collides with sharp curves
  - a small collider leads to jittery normal calculation
  - i think the normal calculation collider needs to be different from the board's physical collider

#### later
- try velocity lookahead for camera?
- how to do procedural tail animation without ruining physics?
- logarithmic lava rising speed?
- resolution bug
- end screen
- keep lava active when player dies
- screenshake on rough landing
  - bump sound
- divide board functions into separate gameobjects (gyroscope, thruster, ground repeller, ground attractor)