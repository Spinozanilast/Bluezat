![Bluezat](https://raw.githubusercontent.com/Spinozanilast/Bluezat/main/.github/poster.jpg)

# Bluezat

> A successor of the [DotNet-BlueZ](https://github.com/hashtagchris/DotNet-BlueZ) library, boosted by using `Tmds.DBus.Protocol` instead of `Tmds.DBus` to get all the benefits of NativeAOT and trimming in modern .NET.

Bluezat gives you a strongly typed, event-driven way to talk to the [BlueZ](https://github.com/bluez/bluez) D-Bus API on Linux. It uses **Tmds.DBus.Protocol** to access D-Bus and the object interfaces are generated with **Tmds.DBus.Tool**.

## Installation

```bash
dotnet add package Spinozanilast.Bluezat
```
Requires **.NET 7 or later** and a Linux system with BlueZ running.

## License

[MIT](https://github.com/Spinozanilast/Bluezat/blob/main/LICENSE)