import React from "react";
// import { Table } from "react-bootstrap";
import * as z from 'zod';

const customType = z.object({
    '@type': z.string()
});

type RenderType = "simple-value" | "object" | "array";
type RenderFunction = (value: unknown) => JSX.Element | null;

const customerRenderers = new Map<string, RenderFunction>();

export function setRenderFunction(typeName: string, render: RenderFunction) {
    customerRenderers.set(typeName, render);
}

export function Dump(props: { value: unknown }): JSX.Element {
    const o = props.value;
    const info = getRenderType(o);
    if (info.type === 'simple-value') {
        return <div>{info.value}</div>;
    } else if (info.type === 'object') {
        const { value } = info;
        const obj = customType.safeParse(value);
        if (obj.success && obj.data["@type"]) {
            const type = obj.data["@type"];
            const renderer = customerRenderers.get(type);
            if (renderer) {
                const result = renderer(value)
                if (result) {
                    return result;
                }
            }
        }
        const keyValues = getKeyValues(value);
        return <table className="table-auto border-collapse border">
            {keyValues.map(kv => {
                const { key, value } = kv;
                return <tr>
                    <th className="border border-slate-600">{key}</th>
                    <td className="border border-slate-600">
                        <Dump value={value}></Dump>
                    </td>
                </tr>
            })}
        </table>
    } else {
        const { value, keys } = info;
        const rows = value as Record<string, unknown>[];
        if (keys) {
            // Tailwind CSS table
            return <table className="table-auto border-collapse border">
                <thead>
                    <tr >
                        {keys.map(key => <th className="border border-slate-600">{key}</th>)}
                    </tr>
                </thead>
                <tbody>
                    {rows.map(row => <tr className="border border-slate-600">
                        {keys.map(key => <td>
                            <Dump value={row[key]}></Dump>
                        </td>)}
                    </tr>)}
                </tbody>
            </table>
        }
        return <table className="table-auto border-collapse border">
            <tbody>
                {rows.map(row => <tr>
                    <Dump value={row}></Dump>
                </tr>)}
            </tbody>
        </table>
    }
}

type RenderInfo = { type: 'simple-value'; value: string } |
{ type: 'object'; value: object } |
{ type: 'array'; value: object; keys: string[] | undefined };

function getRenderType(
    value: any
): RenderInfo {
    const type = typeof value;
    switch (type) {
        case "string":
        case "number":
        case "bigint":
        case "boolean":
        case "symbol":
        case "undefined":
            return { type: "simple-value", value: `${value}` };
        case "object":
            if (value === null) return { type: "simple-value", value: "null" };
            if (Array.isArray(value)) {
                const keys = getCommonKeys(value);
                return { type: "array", value, keys };
            }
            return { type: "object", value: (value as object) };
        default:
            throw new Error("Unsupported type");
    }
}

function getKeyValues(value: object) {
    if (!value) {
        return [];
    }
    return Object.entries(value).map(([key, value]) => ({ key, value }));
}

function getCommonKeys(value: object[]): string[] {
    if (!value) {
        return [];
    }
    const keys = new Set<string>([]);
    for (const row of value) {
        for (const key of Object.keys(row)) {
            keys.add(key);
        }
    }
    return Array.from(keys);
}