---
title: 'Object - Show thyself!'
excerpt: 'In 2007, I became obsessed with LINQPad and the object dumper.'
coverImage: '/assets/white-and-grey-fractal.png'
date: '2020-09-24T20:51:00.000Z'
author:
  name: Steve Goguen
  picture: '/assets/blog/authors/jj.jpeg'
ogImage:
  url: '/assets/white-and-grey-fractal.png'
---

## Object - Show thyself!

In 2007, I became obsessed with LINQPad.  If you've never used it, LINQPad is a tool that allows you to write C# code and execute it.  It's a great tool for learning C# and for quickly prototyping code.  It's also a great tool for quickly prototyping LINQ queries.  I used it to quickly prototype LINQ queries for a project I was working on at the time.  I was so impressed with the object dumper that I wrote a blog post about it.  I've been using the object dumper ever since.

## Creating an Object Dumper with React and TypeScript

I've been working on a project that uses React and TypeScript.  I wanted to create a component that would allow me to quickly dump an object to the screen.  I started by creating a new React project using `create-react-app`.  I then added TypeScript support to the project.  I used the following command to add TypeScript support:

```bash
npx create-react-app my-app --template typescript
```

I then created a new component called `ObjectDumper`.  I added the following code to the component:

```tsx
import React from 'react';

export interface ObjectDumperProps {
    object: any;
}
```