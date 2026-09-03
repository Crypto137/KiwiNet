# Objects

Path of Exile uses a data-driven game object (entity) system inspired by Dungeon Siege (see [A Data-Driven Game Object System by Scott Bilas](https://www.youtube.com/watch?v=Eb4-0M2a9xE) for reference).

Objects are composed of components, which handle domain-specific functionality (pathfinding, animation, etc.). Objects are defined in object template (`.ot`) files. Additional client-specific components, such as `Render`, are defined in object template client (`.otc`) files. Both file types use a plain text format with the same syntax. Templates can inherit data from other objects to reduce duplication.

There are at least two distinct subtypes of objects: world objects and item objects (names are deduced from behavior and may be inaccurate). They share the same template system, but are used in different contexts.

World objects are used for game objects that appear in the game world. They contain additional functionality needed for replicating data changes happening to previously serialized world objects to clients. World objects are expected to have the `Positioned` component.

Item objects are used to define item specifications. They are not capable of being placed in the world on their own, and are generally used as data for other contexts (e.g. `WorldItem` and `Inventories` components in world objects).

## Components

Components have a strict serialization order, which indicates some kind of static type id system. This is still being investigated.

### Common

#### World

- Positioned

- Stats

- Pathfinding

- Life

- Animated

- Player

- Inventories

- Actor

- BaseEvents

- Chest

- ObjectMagicProperties

- LimitedLifespan

- AreaTransition

- Projectile

- Transitionable

- WorldItem

- NPC

- Shrine

#### Item

- Base

- Mods

- LocalStats

- AttributeRequirements

- Quality

- Armour

- Sockets

- Usable

- Charges

- Flask

- SkillGem

- Quest

- Weapon

### Client

- Render

- Targetable

- ProximityTrigger

- RenderItem

- Portal

- ClientWorldItem

- ClientNPC
