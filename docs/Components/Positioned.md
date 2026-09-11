# Positioned

`Positioned` is used to define a world object's spatial properties (position, rotation, scale). All world objects have this component.

|              |                                |
| ------------ | ------------------------------ |
| Dependencies | None                           |
| Data Table   | `Data/ComponentPositioned.dat` |

## Template Variables

| Name          | Type   | Note                                    |
| ------------- | ------ | --------------------------------------- |
| `blocking`    | `bool` |                                         |
| `object_size` | `int`  | 1 is subtracted when parsed.            |
| `scale`       | `int`  | Percentage, divided by 100 when parsed. |
| `static`      | `bool` |                                         |
