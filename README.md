
**What problems did the application have before you started refactoring?**

Engine and commands needed refactoring. All the commands were in the engine and were called
by switch case - the open-closed principle was violated. In many of the commands the open-closed priniciple was not followed, as well.
The project needed reducing code complexity. There were too many commands. There were magic strings upon the code.
SOLID principles were not followed. The code was tightly coupled.

**What design patterns have you used and why?**
Abstract factory - to create instances of the classes.
Command design pattern -  to encapsulate a request in an object in the commands' classes.
Inversion Of Control  pattern - used for decoupling components and layers in the system (Autofac IoC Container).



