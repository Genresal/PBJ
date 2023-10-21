import { Avatar, Button, Grid } from "@mui/material"
import classes from "./UserCard.module.css"
import { useHistory } from "react-router-dom/cjs/react-router-dom.min"

export default function UserCard({key, user, handleFollowersClick, isButtonEnabled}) {
    const history = useHistory();

    const handleUserClick = () => {
        history.push(`/${user.email}`);
    }

    return (
        <>
            <Grid  onClick={handleUserClick} key={key} container columnSpacing={3} className={classes.userCard} justifyContent="space-between"
            style={{border: "1px solid lightGray", borderBottom: "none", padding: 10, width: 500, margin: 0}}>
                <Grid item>
                    <Avatar>F</Avatar>
                </Grid>
                <Grid item style={{fontSize: 22}}>
                    {user.email}
                </Grid>
                <Grid item>
                    {isButtonEnabled && (
                            user.isFollowingRequestUser
                            ?
                                <Button onClick={() => handleFollowersClick(user.email, false)} variant="contained" 
                                style={{textTransform: "none", borderRadius: 20, backgroundColor: "lightgray"}}>
                                    Unfollow
                                </Button>
                            :

                                <Button onClick={() => handleFollowersClick(user.email, true)} variant="contained" 
                                style={{textTransform: "none", borderRadius: 20}}>
                                    Follow
                                </Button>
                        )
                    }
                </Grid>
            </Grid>
        </>
    )
}
