# IronScheme Unity

IronSchemeUnity is a collection of c# scripts and IronScheme libraries that make it possible to use IronScheme as a scripting language in Unity3d.

# Setup
IronScheme Unity is meant to be used in the context of a Unity3d project. To "Install" Clone this repository and copy the files and folders to your project's Assets directory. The folder structure is important because of the way unity works and the special meaning folders like Editor and StreamingAssets have.

```Shell
$ git clone https://github.com/saolsen/IronSchemeUnity.git
$ cd IronSchemeUnity
$ cp ./* /my/unity/project/Assets/
```

Then open the unity project (or if it's already open wait for it to import the new assets). In unity go to Edit -> Project Settings -> Player and under Other Settings -> Optimization set "Api compatibility level" to ".NET 2.0." One of the things included is a repl window so you should now be able to go to the Window menu, select Repl and have an ironscheme repl that you can evaluate expressions from in the editor.

# What's Included
* Simple editor repl with the ability to load scheme libraries.
* Beginnings of a scheme library to wrap common Unity functionality.
* Example monobehavior for running code from a scheme library.

# Editor Repl
## Evaluation in the repl.
The editor repl is accessed through the window menu. It loads the ironscheme clr library and also adds the reference to the UnityEngine dll so you should be able to execute expressions that use the unity engine strait away in the repl. For example evaluating this should log in the unity console.

```Scheme
>>> (clr-static-call Debug Log "Test Logging From The Repl")
```

## Loading Scheme Libraries
Scheme source files are stored in the StreamingAssets folder. This is a special folder name used by unity that will recreate this directory in a build with these files when it's deployed. This is necessary so that we can load the files into ironscheme without having to compile them to dll's or do some other trickery. Libraries can be loaded from the repl (which has already set up the library loading path to point at the StreamingAssets folder).

For example create a small test library in StreamingAssets called mylib.sls
```Scheme
#!r6rs
(library (mylib)
(export test-logging)
(import (rnrs)
        (ironscheme clr))
(clr-using UnityEngine)

(define (test-logging)
  (clr-static-call Debug Log "logging!"))
)
```

Then from the repl you should be able to load and call the library.
```Scheme
>>> (import (mylib))
>>> (test-logging)
```

# Common Scheme Libraries
IronSchemeUnity ships with a (very small) library that wraps some common unity functionality. It is far from extensive and it's my hope that if you write some code that you think is useful to anyone you would submit it back to this library so we can gradually build it up. You can load it from the repl or from another scheme file with import.

```Scheme
(import (unity))
```

# Calling Scheme from csharp.
One limitation of IronScheme currently is you can't implement a class that extends Monobehavior directly. That doesn't mean you can't consume an IronScheme library to the same effect though. References to functions in a scheme library can be captured in c# and then executed. A very simple example of a monobehavior that consumes a scheme file are testlib.sls and TestLibrary.cs. This example shows capturing an "update" function from scheme and calling it every Update() of the monobehavior.

I'm currently working on a much nicer system for creating gameobject behaviors in scheme which will be modeled after the techniques [here](https://docs.google.com/document/d/13kocjneV_tprPBXm6q63QQnCqmofRMBCJ8qjzHmxvgk/pub) but they are not ready yet.

# Future Work (and call for submissions)
* Better Tooling (Nicer editor repl, In game repl)
* More functions for the standard library.
* Ways to declare game behavior and attach it to game objects. (first attempts coming soon)

Huge thanks to [leppie](https://twitter.com/leppie) for helping get this working.
