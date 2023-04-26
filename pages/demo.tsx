import React from "react";
import {Dump} from "../components/dump";

const person = {
    name: 'Steve',
    age: 30,
    projects: [
        { name: 'Build this', done: false },
        { name: 'Build that', done: true }
    ],
}

export default function Index(o: unknown) {
    const item = <div>Hello</div>;
    console.log(item);

    return <div>
        <h1>Title</h1>
        <p>Paragraph</p>
        <Dump value={person}></Dump>
    </div>
};

