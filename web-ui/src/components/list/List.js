import React from "react";
import Film from "./film";

const films = [
    {id: "1", title: "Film1"},
    {id: "2", title: "Film2"}
]

const listItems = films.map(film =>
<li key={film.id}>
    <Film id={film.id} title={film.title}/>
</li>)

function List() {
    return (
        <div>
            <ol>
                {listItems}
            </ol>
        </div>
    )
}

export default List
