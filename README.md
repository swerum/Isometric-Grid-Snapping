# Isometric-Grid-Snapping
This project contains a system for some Simple Grid Snapping for grid's with non-orthogonal axes e.g. isometric grids.
As input it takes
- Vector3 origin: where is the (0,0) position of your grid on the screen
- Vector2 horizontalAxis: what direction does your x-axis go and how large is the tile in the x direction
- Vector2 verticalAxis: what direction does your y-axis go and how large is the tile in the y direction
- Vector2Int gridSize: how many tiles are there in each direction
![Image of Editor Input](Readme%20Images/Editor.png)

## Examples
Normal grids have an x-axis that goes right and a y axis that goes up, so horizontalAxis = (1,0) and verticalAxis = (0,1)
![Image of Generated Regular Grid](Readme%20Images/Normal%20Grid.png)

However, you may want your grid to be angled, for example when displaying it with perspective. The most common example of this is an isometric grid - often used in indie games. Here the axes might be: horizontalAxis = (1, 0.6f) and verticalAxis = (1, -0.6f)
![Image of Generated Isometric Grid](Readme%20Images/isometric.png)

Finally, you might have a wall that you want to snap to instead of just the floor. here you might have the axes: horizontalAxis = (0.7f, -0.4f) and verticalAxis = (0,1)
![Image of Generated Slanted Grid](Readme%20Images/Wall%20Perspective.png)

## The Math
The main complicated math is in GridUtil.FindGridCoordinates(). 
We Linear Algebra to split up the Vector from the origin to the given position into some combination of the x and y axis vectors. You can imagine the screen to be a plane defined by the two vectors: v for the vertical Axis and h for the horizontal axis. Every point on this plane must be some combination of those two vectors, such that 
$`P = s * v + t * h `$ where s and t are some (as yet unknown) rational numbers (floats). In the below drawing, for example, $`s = 1, t = 3`$, so $`P = 1 * v + 3 * h`$

![Image of hand-drawn Example Grid](Readme%20Images/Vectors.png)

To find s and t, we have to solve the equation $`P = s * v + t * h`$  for s and t. Luckily, this equation is actually two equations, since it has to be true for both x and y. Thus, we actually have

$`P.x = s * v.x + t * h.x`$    

AND

$`P.y = s * v.y + t * h.y`$

We can solve both equations for s and then set them equal

$` s = (P.x - t * v.x) / h.x`$

$`s = (P.y - t * v.y) / h.y`$

&rarr; $`(P.x - t * v.x) / h.x = (P.y - t * v.y) / h.y `$

We then have an equation without s that we can solve for t

$`t = (h.x * P.y - h.y * P.x) / (v.y * h.x - v.x * h.y) `$

So now we have both s and t. We can then round them to the nearest integer and plug them back into $`P = s * v + t * h `$ in order to find the closest grid coordinate to our original position P.
