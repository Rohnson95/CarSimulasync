# Car Simulasync
Made By <a href="https://www.linkedin.com/in/robert-johnson-4466101aa/"  target="_blank">Robert Johnson</a>

A Tiny project made to get used to using different threads. 
The premise is that there is a car race where each car represented is a thread. They drive a distance of 10km with a constant velocity that is 120 km/h.
There are events present that can and will occur by chance during the race.
Examples of this are as follows:
* Run out of gas - This forces the thread to stop for 30 seconds
* Flat tire - Same as above but 20 seconds
* Bird caught on windshield - Same but 10 seconds
* Mechanical problems - This causes the velocity to decrease by 1 km/h each time it occurs.

## How Does it work?
The Program is very plain and simple. Starting out you'll be welcomed and asked to press <b>enter</b> to start the race
As soon as the race starts there will be messages popping up whenever an issue occurs. And if you press <b>enter</b> again, you'll get the distance remaining, how fast you're going, and the problem currently affecting the car(s).

