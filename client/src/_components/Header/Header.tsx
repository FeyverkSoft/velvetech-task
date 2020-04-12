import * as React from 'react';
import style from "./header.module.scss";

export const Header = ({ ...props }) => {
    return (
        <header className={style["header"]}>
            {props.children}
        </header>
    );
}

export const Logo = ({ ...props }) => {
    return <div className={style["logo"]}>
        {<div className={style["title"]}><b>Students</b>Task</div>}
        <div className={style["small-title"]}><b>S</b><b>T</b></div>
    </div>
}
