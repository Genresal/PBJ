import classes from "./Separator.module.css"

export default function Separator({text = ""}) {
    return (
        <div className={classes.container}>
            <div className={classes.border}/>
                <span className={classes.content}>
                    {text}
                </span>
            <div className={classes.border}/>
      </div>
    )
}
