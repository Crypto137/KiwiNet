# Objects

Path of Exile uses a data-driven object system inspired by Dungeon Siege (see [A Data-Driven Game Object System by Scott Bilas](https://www.youtube.com/watch?v=Eb4-0M2a9xE) for reference).

Objects are composed of components, which handle domain-specific functionality (pathfinding, animation, etc.). Objects are defined in object template (`.ot`) files. Additional client-specific components, such as `Render`, are defined in object template client (`.otc`) files. Both file types use a plain text format with the same syntax. Templates can inherit data from other objects to reduce duplication.

There are at least three distinct subtypes of objects: world objects, items, and animated objects (names are deduced from behavior and may be inaccurate). They share the same template system, but are used in different contexts.

World objects (also referred to as "game objects", "entities", or just "objects") are objects that appear in the game world. They contain additional functionality needed for replicating data changes happening to previously serialized world objects to clients. World objects always have the `Positioned` component.

Item objects are used to define item specifications. They are not capable of being placed in the world on their own, and are generally used as data for other contexts (e.g. `WorldItem` and `Inventories` components in world objects).

Animated objects are used by the animation system. They are defined in `.ao` and `.aoc` files for common and client-specific components respectively. Details remain to be investigated.

Components are serialized over the network in the order their templates are stored in the object template, which in most cases follow the definition order in the *parent* template. One exception to this rule is the `Positioned` component for world objects, the template for which is allocated before file deserialization happens, resulting in it always being the first one.

## Components

### World

#### Common

| Name                                                           | Description                                                                        |
| -------------------------------------------------------------- | ---------------------------------------------------------------------------------- |
| [Actor](./Components/Actor.md)                                 | Allows objects to use actions.                                                     |
| [Animated](./Components/Animated.md)                           |                                                                                    |
| [AreaTransition](./Components/AreaTransition.md)               | Interactable objects that allow moving to a different area.                        |
| [BaseEvents](./Components/BaseEvents.md)                       |                                                                                    |
| [Chest](./Components/Chest.md)                                 | Used for containers that can drop items.                                           |
| [Inventories](./Components/Inventories.md)                     | A collection of inventories.                                                       |
| [Life](./Components/Life.md)                                   | Contains life, energy shield, mana, and buffs.                                     |
| [LimitedLifespan](./Components/LimitedLifespan.md)             |                                                                                    |
| [NPC](./Components/NPC.md)                                     |                                                                                    |
| [ObjectMagicProperties](./Components/ObjectMagicProperties.md) |                                                                                    |
| [Pathfinding](./Components/Pathfinding.md)                     |                                                                                    |
| [Player](./Components/Player.md)                               | Player specific data (character name, experience, allocated passive skills, etc.). |
| [Positioned](./Components/Positioned.md)                       | Defines spatial properties (position, rotation, scale).                            |
| Projectile                                                     | Mentioned in game data, but no code to parse it.                                   |
| Shrine                                                         | Mentioned in game data, but no code to parse it.                                   |
| [Stats](./Components/Stats.md)                                 |                                                                                    |
| [Transitionable](./Components/Transitionable.md)               |                                                                                    |
| [WorldItem](./Components/WorldItem.md)                         | Represents an item object in the game world.                                       |

#### Client

| Name             | Description |
| ---------------- | ----------- |
| ClientNPC        |             |
| ClientWorldItem  |             |
| Portal           |             |
| ProximityTrigger |             |
| Render           |             |
| RenderItem       |             |
| Targetable       |             |

### Item

#### Common

| Name                                                           | Description |
| -------------------------------------------------------------- | ----------- |
| [Armour](./Components/Armour.md)                               |             |
| [AttributeRequirements](./Components/AttributeRequirements.md) |             |
| [Base](./Components/Base.md)                                   |             |
| [Charges](./Components/Charges.md)                             |             |
| [Flask](./Components/Flask.md)                                 |             |
| [LocalStats](./Components/LocalStats.md)                       |             |
| [Mods](./Components/Mods.md)                                   |             |
| [Quality](./Components/Quality.md)                             |             |
| [Quest](./Components/Quest.md)                                 |             |
| [Shield](./Components/Shield.md)                               |             |
| [SkillGem](./Components/SkillGem.md)                           |             |
| [Sockets](./Components/Sockets.md)                             |             |
| [Stack](./Components/Stack.md)                                 |             |
| [Usable](./Components/Usable.md)                               |             |
| [Weapon](./Components/Weapon.md)                               |             |

#### Client

| Name       | Description |
| ---------- | ----------- |
| BodyModel  |             |
| RenderItem |             |

### Animated

#### Common

| Name                                                             | Description |
| ---------------------------------------------------------------- | ----------- |
| [AnimationController](./Components/AnimationController.md)       |             |
| [AttachedAnimatedObject](./Components/AttachedAnimatedObject.md) |             |
| [Hull](./Components/Hull.md)                                     |             |

#### Client

| Name                      | Description |
| ------------------------- | ----------- |
| AnimatedRender            |             |
| BoneGroups                |             |
| ClientAnimationController |             |
| DecalEvents               |             |
| EffectPack                |             |
| Lights                    |             |
| ParticleEffects           |             |
| SkinMesh                  |             |
| SoundEvents               |             |
