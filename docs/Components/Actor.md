# Actor

`Actor` allows world objects to execute actions, such as moving and using skills.

## Template Variables

| Name                     | Type     | Note                                                       |
| ------------------------ | -------- | ---------------------------------------------------------- |
| `actor`                  | `string` | Resource file path.                                        |
| `actor_size`             | `string` | Allowed values: `Small`, `Medium`, `Large`, `Epic`.        |
| `basic_action`           | `string` | Can be used multiple times to assign more than one action. |
| `main_hand_unarmed_type` | `string` | See the `ItemClass` enum for allowed values.               |
| `off_hand_unarmed_type`  | `string` | See the `ItemClass` enum for allowed values.               |
| `team`                   | `int`    |                                                            |

## Dependencies

- Pathfinding

- Stats

- Life

- Animated
