https://www.youtube.com/watch?v=LFOXge7Ak3E

# Singleton types

## Generic

Generic Singleton is what it is. The thing I don't like is that he just adds a new object whenever there aren't any on any scene. Isn't it better to just throw an error? So we always keep the best control

## Persistent

Destroy new ones

Don't destroy on load works only on object at the root lvl

## Regulator

Destroy old ones

Use-case - replace a system

Is self-regulating
Destroys if any exists

`FindObjectsByType` is a new Unity Method (a more optimized one?). And it has sort mode