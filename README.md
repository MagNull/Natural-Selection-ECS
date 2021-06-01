# Natural-Selection-ECS
Simulation of natural selection with ECS 

  The simulation is an environment with food that is periodically created on it and a certain number of creatures in it.  

## Mutation
Creatures can replicate with a certain amount of food.
With replication, there is a chance that a mutation will occur.
At the moment, 2 mutations have been implemented: 
* Speed Mutation - a new creature can have a speed lower or higher than its parent. The faster the creature, the redder it becomes, the slower the blue.
* Poisonous mutation - a new creature can get a poisonous trait with a toxicity parameter, which affects whether the one who eats this creature dies. The toxicity
is noticeable by the shade of green in the creature.
The threshold at which the eater dies is configurable.

Also, each tick (the size is adjustable) takes away a certain amount of food from all creatures. The speed and toxicity are directly proportional to this amount.
  
## Predation
When a creature cannot replicate for a certain period of time (configurable), it acquires the sign of predation. The incomplete predator continues to eat grass, but now it can 
eat other individuals, but the meat will not yet have full nutritional value.
  Predators have such parameters as:
* Rapacity - food consumption increases as you increase. Directly proportional to Predator Experience (configurable)
* Predator Experience - indicator that affects who an individual can eat. A predator can only eat individuals in which
The Predator Experience is smaller than its own.
* Predatoriness - affects the nutritional value of meat and increases as the meat is eaten from 0.1 to 1. To become an absolute predator, you need to eat the required number of
other individuals (configurable). 

As it eats meat, the creature can completely refuse to eat grass. This happens when the ratio of units of food obtained from grass to food obtained from meat is greater than or
equal to a certain number (configurable). 

### Writed by:
* LeoECS
* Job System
