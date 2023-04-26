---
title: 'F# Advent - Making an F# to C# Compiler with Fable'
excerpt: ''
coverImage: '/assets/blog/hello-world/cover.jpg'
date: '2022-12-03T09:00:00.000Z'
author:
  name: Steve Goguen
ogImage:
  url: '/assets/blog/hello-world/cover.jpg'
---

# F# &harr; C# &mdash; What would it take?

I've been wondering recently what would be involved to make a C# target for the Fable compiler. What would it take to make a tool that can convert C# into F#?  Right of the bat, I know this is a difficult and time consuming project for someone who has experience doing this sort of thing. I've never written a real compiler before and I know I am absolutely over my head with something like this, but I want to try anyway.

## Would it be useful?

I think there is utility converting F# to C# with a transpiler that is capable of producing clean and idiomatic C# code.  I don't 
believe this type of compiler would ever come close as a practical replacement for the official F# compiler, but it could be a useful tool for people who are learning F# and who need a bridge to help them transition from C# to F#.

If such a compiler was good enough, it might even provide a tool to help people migrate away from F# if they decided it was not for them.

Ironically, I think providing that sort of optionality would actually make F# more attractive to people who are on the fence about trying it in the first place.

## My Motivation

* I like compilers and I like languages and I think I want to work with them more on one level or another.
* I want to learn this stuff on a personal level, not on a handwavey abstract level.
* I want to grow from trying something hard and I want to work on something that interests me.
* I used to frequent a hack night meetup where we would go to a Starbucks and work on side-projects. The way it worked was each person would share their goal for the evening and then we would sit there together quietly for the next 3-4 hours and demo what we did at the end.

# Copying the Fable to Dart Compiler

Rather than starting from scratch, I decided to start by copying the Dart compiler from the Fable repository.  I figured it would be a good starting point because it's a statically typed language verify similar to C#.  I'm not sure if that was the best choice, but it's a good learning experience and I may continue with it a little longer so I can get a better understanding of the Fable compiler.

## Transforming F# to C#

The Fable compiler allows you to create targets for different languages. The default target is JavaScript, but in the last few years, we've added support for other languages like TypeScript, Python, PHP and Rust.  Most are alpha or experimental, but the Fable compiler is young and I think there's so much potential for it to be a great tool.

With that, I wanted to understand the actual challenges of creating a C# target rather than just thinking about it. I figured the best way to do that was to start with a simple program and see how far I could get.

# Basics of the Fable Compiler Transformation

The 50,000 foot view of the Fable compiler can be broken down into 3 steps:

1. Parsing the F# AST (Abstract Syntax Tree)
2. Transforming the F# AST to a Target Language AST
3. Printing the Target Language AST to a string (The actual code)

### Step 1 - Define Source and Target ASTs

Fortunately for us, parsing F# has already been handled. We get to focus on the fun part: Transforming the F# AST to a C# AST.

Here's where you can find the ASTs for each language:

<table>
    <thead>
        <tr>
            <th>Language</th>
            <th>AST File</th>
        </tr>
    </thead>
</table>

| Language | AST File |
| --- | --- |
| F# | ./src/Fable.AST/Fable.fs |
| C# | ./src/Fable.Transforms/CSharp/CSharp.fs |
| Dart | ./src/Fable.Transforms/Dart/Dart.fs |
| Python | ./src/Fable.Transforms/Python/Python.fs |
| PHP | ./src/Fable.Transforms/PHP/PHP.fs |
| JavaScript | ./src/Fable.Transforms/Globals/Babel.fs |
| Rust | ./src/Fable.Transforms/Rust/Rust/AST/*.fs (This is broken up into separate files) |

(**Now might be a good time to take a cursory look at the code**)

### Step 2 - Transform AST -> AST

In the same folders as the AST, you'll typically find a file called `Fable2[LangName].ts` that holds the main transformation logic.

That logic is supported by a set of helper functions in the `Replacements.fs` file, which helps the Fable compiler transform .NET and F# specific library calls into their target language equivalents.

Usually, the F# standard library has to be rewritten for each target language. For example, the F# Mailbox Processor is something that doesn't exist in JavaScript/TypeScript, so it was rewritten in TypeScript.  You can check it out here: [./src/fable-library/MailboxProcessor.ts](../../src/fable-library/MailboxProcessor.ts).

It's pretty wild!

### Step 3 - Generate Code

The last part of the Fable compiler is the printer.  The printer takes the target language AST and turns it into a string.  You can think of it as the inverse of the parser.

[Next post](./03--03-making-dart-look-like-csharp.md), I struggle to get a simple "Hello World" program to compile and EXECUTE!

# Making Dart Code Look like C#

In part 1, I was able to get the Fable compiler to output Dart code in .cs files.  This is a good start, but I want to get the code to look more like C# so we can actually run it.

The first thing I wanted to do was not worry about C# Projects for now, so I'm targeting CSX files, which are C# scripts and I'm using the [dotnet-script]() tool to run them.

```bash
dotnet tool install -g dotnet-script
```

I updated the CLI to produce a .csx file instead of a .dart file and worked towards "Hello World"

## Problem 1 - Dart doesn't support top-level statements

```fsharp
printfn "Hello World"
```

This is a top-level statement.  It's not supported in Dart.  What I can do is define a variable and assign it to the result of the top-level statement like so:

```fsharp
let returnCode =
    printfn "Hello World"
    0
```

That generates Dart code that looks like this:

```dart
import 'fable-library/String.dart' as string;

final returnCode = (() {
    string.toConsole<void>(string.printf<void, TextWriter, void, void, void>('Hello World'));
    return 0;
})();
```

There's so much wrong here, so let's simplify:

1. We're going to avoid printfn for now.  We'll write a simple function that uses Fable's Emit attribute to emit the Console.WriteLine method instead.  (We're bootstrapping!)
2. Next, we'll get into the Fable compiler and start changing the output.

First, let's define a function that uses the Emit attribute:

```fsharp
open Fable.Core

[<Emit("Console.WriteLine($0)")>]
let print(msg: string) = jsNative

let returnCode =
    print "Hello World"
    0
```

The Dart code is smaller, but we need it to look like C# code:

```dart
final returnCode = (() {
    Console.WriteLine('Hello World');
    return 0;
})();
```

1. C# doesn't declare variables with the `final` keyword.
2. C# lambdas require the `=>` operator between the parameters and body.
3. We can't define a lambda function and immediately execute it in C# like that.

Fixing the final keyword and adding the `=>` operator is easier than fixing the issue where we immediately execute the lambda function, but let's talk about the Fable compiler before we dive into it:


-----

## Fable.Transforms - Autobots, Roll Out!

[https://media.giphy.com/media/fl9WeVqXsGmHK/giphy.gif](https://media.giphy.com/media/fl9WeVqXsGmHK/giphy.gif)

We're going to focus on the Fable.Transforms project, specifically the CSharp subfolder.  In this folder, there are 4 files:

| File | Description |
| --- | --- |
| CSharp.fs | This module defines the C# AST (Which is actually the Dart AST).  There's no transformations here, just data types representing the C# code. |
| Fable2CSharp.fs | This module defines the transformations that take the F# AST and convert it to the C# AST.  It's the fattest of the modules and most of the work is done here. |
| CSharpPrinter.fs | When the Fable compiler is done transforming the F# AST to the C# AST, we then send that C# AST object to the CSharpPrinter module to generate the C# code. |
| Replacements.fs | This module assists the Fable2CSharp module by assisting with standard library and data type transformations.  This is more about runtime stuff and less about language features. |

-----

## Fixing the CSharpPrinter.fs

Let's fix the first two problems in the CSharpPrinter.fs file so we can familiarize ourselves with it.  Take some time to quickly survey the code.  It's mostly made up of match expressions and calls to printer.Print("...") to output the C# code.

If I search for `final` in the file, I find three places where it's used:

```fsharp
            | ForInStatement(param, iterable, body) ->
                printer.Print("for (final " + param.Name + " in ")
                printer.PrintWithParensIfComplex(iterable)
                printer.Print(") ")
                printer.PrintBlock(body)
```

We can safely change that final keyword to var.

OK!  Next we find this large block of code that prints variable declarations:

```fsharp
        member printer.PrintVariableDeclaration(ident: Ident, kind: VariableDeclarationKind, ?value: Expression, ?isLate) =
            let value =
                match value with
                | None -> None
                // Dart recommends not to explicitly initialize mutable variables to null
                | Some(Literal(NullLiteral _)) when kind = Var -> None
                | Some v -> Some v

            match value with
            | None ->
                match isLate, ident.Type with
                | Some false, _
                | None, Nullable _ -> ()
                | Some true, _
                // Declare as late so Dart compiler doesn't complain var is not assigned
                | None, _ -> printer.Print("late ")  //  DO WE CHANGE THIS TO "var"????
                match kind with
                | Final -> printer.Print("final ")   // <--- CHANGE THIS TO "var"
                | _ -> ()
                printer.PrintType(ident.Type)
                printer.Print(" " + ident.Name)

            | Some value ->
                let printType =
                    // Nullable types and unions usually need to be typed explicitly
                    // Print type also if ident and expression types are different?
                    // (this usually happens when removing unnecessary casts)
                    match ident.Type with
                    | Nullable _ -> true
                    | TypeReference(_, _, info) -> info.IsUnion
                    | _ -> false

                match kind with
                | Const -> printer.Print("const ")   // <--- DO WE CHANGE THIS TO "var"????
                | Final -> printer.Print("final ")   /// <--- CHANGE THIS TO "var"
                | Var when not printType -> printer.Print("var ")
                | Var -> ()

                if printType then
                    printer.PrintType(ident.Type)
                    printer.Print(" ")

                printer.Print(ident.Name + " = ")
                printer.Print(value)
```

There's a lot going on in this block, and I plan on coming back to understand all the scenarios and deal with it more thorougly, but I want to address the issue superficially for so we can familiarize ourselves with the code.

For now, I'm inclined to not touch the `late` and `const` keywords because I don't understand the implications of changing them yet.

I just want to get my program running.

## Fixing the Lambda Function Syntax

While searching for 'function' in the file, I find this block of code for printing AnonymousFunctions

```fsharp
            | AnonymousFunction(args, body, genArgs, _returnType) ->
                printer.PrintList("<", genArgs, ">", skipIfEmpty=true)
                printer.PrintIdentList("(", args, ")", printType=true)
                printer.PrintFunctionBody(body, isExpression=true)
```

I can fix this quickly by adding the fat arrow between the parameters and body like so:

```fsharp
            | AnonymousFunction(args, body, genArgs, _returnType) ->
                printer.PrintList("<", genArgs, ">", skipIfEmpty=true)
                printer.PrintIdentList("(", args, ")", printType=true)
                printer.Print(" => ")  // <--- ADD THIS
                printer.PrintFunctionBody(body, isExpression=true)
```


## Dart Uses Single Quotes for String Literals

We change this:

```fsharp
           | StringLiteral value ->
                let escape str =
                    (Naming.escapeString (fun _ -> false) str).Replace(@"$", @"\$")
                printer.Print("'")
                printer.Print(escape value)
                printer.Print("'")

```

To this:

```fsharp
           | StringLiteral value ->
                let escape str =
                    (Naming.escapeString (fun _ -> false) str).Replace(@"$", @"\$")
                printer.Print("\"")
                printer.Print(escape value)
                printer.Print("\"")
```

## Fixing How We Print Function Types

Dart function types put the return type before the parameter types like: `int Func<string>`


```fsharp
            | Function(argTypes, returnType) ->
                printer.PrintType(returnType)
                printer.Print(" ")
                // Probably this won't work if we have multiple args
                let argTypes = argTypes |> List.filter (function Void -> false | _ -> true)
                printer.PrintList("Func<", ", ", ">", argTypes, printer.PrintType)
```

In C#, we would write this as `Func<string, int>` but only if we're returning a value.  If we're returning void, we would write it as `Action<string>`.  Here the string is the input type.  If function has neither an input or output type, we would write it as `Action`.

This update to the code should be able to accomodate this for now:

```fsharp
            | Function(argTypes, returnType) ->
                let argTypes = argTypes |> List.filter (function Void -> false | _ -> true)
                match returnType, argTypes with
                | Void, [] -> printer.Print("Action")
                | Void, _ -> printer.PrintList("Action<", ", ", ">", argTypes, printer.PrintType)
                | _, _ -> printer.PrintList("Func<", ", ", ">", [ yield! argTypes; yield returnType ], printer.PrintType)
```

Again, we're not making this perfect, we're just getting it to work and making ourselves familiar with the printer.

With these changes, we can now compile this very simple F# program:

```fsharp
open Fable.Core

[<Emit("Console.WriteLine($0)")>]
let print(msg: string) = jsNative

let execute f =
    f()
    0

let returnCode = execute (fun () ->
    print "Hello World!!"
)
```

Into this very simple C# program:

```csharp
int execute(Action f) {
    f();
    return 0;
}

var returnCode = execute(() =>  {
    Console.WriteLine("Hello World!!");
});
```

Try it out by running `dotnet script QuickTest.fs.csx` in your terminal and you should see that magical:

```
Hello World!!
```