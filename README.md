# River Raid, written in Monogame

This is mainly a learning experience for me, to improve my understanding of the Monogame framework.
I am loosely following the [20 game challenge](https://20_games_challenge.gitlab.io/challenge/).
The page for [River Raid](https://20_games_challenge.gitlab.io/games/river_raid/) has a short list of requirements as a guide.
There is also a [wikipedia](https://en.wikipedia.org/wiki/River_Raid) page for the game.


# Hacks for M1 Mac

A couple of issues encountered on Apple Silicon.

First, starting the content editor via `dotnet mgcb-editor` would do nothing.
To get this working, I installed the x64 version of the SDK, as recommended [here](https://community.monogame.net/t/mgcb-not-launching-on-macos-m1/17948/2).
This is in the [monogame documentation](https://docs.monogame.net/articles/whats_new.html#apple-m1-silicon-support), too.

Second, once the font was added, the build would fail with the following exception:
```
System.DllNotFoundException: Unable to load shared library 'freetype6' or one of its dependencies.
```
To resolve this, I followed the suggestion by `sezdev` in [this thread](https://community.monogame.net/t/textureimporter-error-mac-os-monterey-12-6/18049/25).

In Rider, go to `Rider -> Preferences -> Build, Execution, Deployment -> Toolset and Build` and change the `.NET CLI executable path`
to `/usr/local/share/dotnet/x64/dotnet` (it is a drop-down).
This is saved in the `Pong.sln.DotSettings.user` file, which is NOT checked into git.

The game will now run from within Rider.
It does not run from the command-line, unless you do a `dotnet clean` first.

For a little more info, see [this issue](https://github.com/MonoGame/MonoGame/issues/3556#issuecomment-1762816496).


# Art Credits

* [Allied Fighter Sprites](https://www.deviantart.com/prinzeugn/art/Allied-Fighter-Sprites-66532109) by [PrinzEugn](https://www.deviantart.com/prinzeugn)

