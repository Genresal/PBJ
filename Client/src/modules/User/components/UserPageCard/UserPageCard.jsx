import { Grid, Avatar, Button } from "@mui/material"

export default function UserPageCard({user, isFollower, handleFollowUnfollowClick}) {

    return (
        <Grid container rowSpacing={1}  direction="row" style={{border: "1px solid lightgray", padding: 10, marginTop: "10px"}}>
            <Grid item style={{paddingRight: 10}}>
                <Avatar>A</Avatar>
            </Grid>
            <Grid item style={{fontSize: 22}}>
                {user.userName} {user.surname}
            </Grid>
            <Grid item md={12}>
                {user.email}
            </Grid>
            <Grid item>
                Birth Date: {new Date(user.birthDate).toLocaleDateString()}
            </Grid>
            <Grid container style={{padding: 10}} justifyContent="flex-end">
                {isFollower 
                    ?
                        <Button variant="contained" onClick={() => handleFollowUnfollowClick(user.email)}
                        style={{borderRadius: 30, textTransform: "none",  backgroundColor: "lightgray"}}>Unfollow</Button>
                    :   
                        <Button variant="contained" onClick={() => handleFollowUnfollowClick(user.email)}
                        style={{borderRadius: 30, textTransform: "none"}}>Follow</Button>
                }
            </Grid>
        </Grid>
    )
}
