import { Avatar, Button, Grid } from "@mui/material"
import classes from "./FollowerCard.module.css"

export default function FollowerCard({key, follower, handleFollowersClick}) {
  return (
    <>
        <Grid key={key} container columnSpacing={3} className={classes.followerCard} justifyContent="space-between"
        style={{border: "1px solid lightGray", borderBottom: "none", padding: 10, width: 500}}>
            <Grid item>
                <Avatar>F</Avatar>
            </Grid>
            <Grid item style={{fontSize: 22}}>
                {follower.email}
            </Grid>
            <Grid item>
            {
                follower.isFollowingRequestUser
                ?
                    <Button onClick={() => handleFollowersClick(follower.email, false)} variant="contained" style={{textTransform: "none", borderRadius: 20, backgroundColor: "lightgray"}}>
                        Unfollow
                    </Button>
                :

                    <Button onClick={() => handleFollowersClick(follower.email, true)} variant="contained" style={{textTransform: "none", borderRadius: 20}}>
                        Follow
                    </Button>
            }
            </Grid>
        </Grid>
    </>
  )
}
