# Objects

Path of Exile uses a data-driven game object (entity) system inspired by Dungeon Siege (see [A Data-Driven Game Object System by Scott Bilas](https://www.youtube.com/watch?v=Eb4-0M2a9xE) for reference).

Objects are composed of components, which handle domain-specific functionality (pathfinding, animation, etc.). Objects are defined in object template (`.ot`) files. Additional client-specific components, such as `Render`, are defined in object template client (`.otc`) files. Both file types use a plain text format with the same syntax. Templates can inherit data from other objects to reduce duplication.

## Components

### Common

#### General

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
