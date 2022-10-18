# Concavity

Simple solution for a project that required a concave hull and some other point bounding work.

Encloses points with a variable "tightness" as if they were shrink wrapped. A simpler convex hull was not tight enough, and wouldnt work with data containing holes and islands.

Still under developement, really pretty ugly actually, haha

FROM: TestContext Messages:

100 points: 4ms

1,000 points: 10ms

10,000 points: 223ms

100,000 points: 215ms

1,000,000 points: 3554ms


![Flat Demo 1](https://github.com/jared-malooly/Concavity/blob/master/Demos/blank1.PNG?raw=true)
![Flat Demo 1](https://github.com/jared-malooly/Concavity/blob/master/Demos/blank2.PNG?raw=true)
