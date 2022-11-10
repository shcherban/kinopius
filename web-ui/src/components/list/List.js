import React from "react";
import Film from "./film";

function List() {
    return (
        <div>
            <ol>
                <li>
                    <Film />
                </li>
                <li>
                    <Film />
                </li>
                <li>
                    <Film />
                </li>
            </ol>
        </div>
    )
}

export default List
