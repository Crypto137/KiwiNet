# Objects

Path of Exile uses a data-driven game object (entity) system inspired by Dungeon Siege (see [A Data-Driven Game Object System by Scott Bilas](https://www.youtube.com/watch?v=Eb4-0M2a9xE) for reference).

Objects are composed of components, which handle domain-specific functionality (pathfinding, animation, etc.). Objects are defined in object template (`.ot`) files. Additional client-specific components, such as `Render`, are defined in object template client (`.otc`) files. Both file types use a plain text format with the same syntax. Templates can inherit data from other objects to reduce duplication.

There are at least three distinct subtypes of objects: world objects, item objects, and animation objects (names are deduced from behavior and may be inaccurate). They share the same template system, but are used in different contexts.

World objects are objects that appear in the game world. They contain additional functionality needed for replicating data changes happening to previously serialized world objects to clients. World objects always have the `Positioned` component.

Item objects are used to define item specifications. They are not capable of being placed in the world on their own, and are generally used as data for other contexts (e.g. `WorldItem` and `Inventories` components in world objects).

Animated objects are used by the animation system. They are defined in `.ao` and `.aoc` files for common and client-specific components respectively. Details remain to be investigated.

Components are serialized over the network in the order their templates are stored in the object template, which in most cases follow the definition order in the *parent* template. One exception to this rule is the `Positioned` component for world objects, the template for which is allocated before file deserialization happens, resulting in it always being the first one.

## Components

### Common

#### World

| Name                  | Description                                                                        |
| --------------------- | ---------------------------------------------------------------------------------- |
| Actor                 | Allows objects to use actions.                                                     |
| Animated              |                                                                                    |
| AreaTransition        | Interactable objects that allow moving to a different area.                        |
| BaseEvents            |                                                                                    |
| Chest                 | Used for containers that can drop items.                                           |
| Inventories           | A collection of inventories.                                                       |
| Life                  | Contains life, energy shield, mana, and buffs.                                     |
| LimitedLifespan       |                                                                                    |
| NPC                   |                                                                                    |
| ObjectMagicProperties |                                                                                    |
| Pathfinding           |                                                                                    |
| Player                | Player specific data (character name, experience, allocated passive skills, etc.). |
| Positioned            | Defines spatial properties (position, rotation, scale).                            |
| Projectile            |                                                                                    |
| Shrine                |                                                                                    |
| Stats                 |                                                                                    |
| Transitionable        |                                                                                    |
| WorldItem             | Represents an item object in the game world.                                       |

#### Item

| Name                  | Description |
| --------------------- | ----------- |
| Armour                |             |
| AttributeRequirements |             |
| Base                  |             |
| Charges               |             |
| Flask                 |             |
| LocalStats            |             |
| Mods                  |             |
| Quality               |             |
| Quest                 |             |
| SkillGem              |             |
| Sockets               |             |
| Usable                |             |
| Weapon                |             |

#### Animated

| Name                   | Description |
| ---------------------- | ----------- |
| AnimationController    |             |
| AttachedAnimatedObject |             |
| Hull                   |             |

### Client

#### World

| Name             | Description |
| ---------------- | ----------- |
| ClientNPC        |             |
| ClientWorldItem  |             |
| Portal           |             |
| ProximityTrigger |             |
| Render           |             |
| RenderItem       |             |
| Targetable       |             |

#### Animated

| Name                      | Description |
| ------------------------- | ----------- |
| BoneGroups                |             |
| ClientAnimationController |             |
| DecalEvents               |             |
| Lights                    |             |
| ParticleEffects           |             |
| SkinMesh                  |             |
| SoundEvents               |             |
