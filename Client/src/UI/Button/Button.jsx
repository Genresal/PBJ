import classes from "./Button.module.css"

const Button = ({children, ...props}) => {
    return (
        <div {...props} className={ classes.button }>
            {children}
        </div>
    );
};

export default Button;