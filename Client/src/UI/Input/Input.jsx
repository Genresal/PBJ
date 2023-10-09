import React from "react";
import classes from "./Input.module.css"

const Input = React.forwardRef((props, ref) => {
    return (
        <div>
            return(
            <input ref={ref} className={classes.input} {...props}/>
            );
        </div>
    );
});

export default Input;