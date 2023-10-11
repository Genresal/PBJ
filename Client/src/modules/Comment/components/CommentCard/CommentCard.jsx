import { Avatar, Grid, IconButton } from '@mui/material'
import React from 'react'
import { ConvertDate } from '../../../../shared/DateConverter'
import MoreHorizIcon from '@mui/icons-material/MoreHoriz';


export default function CommentCard({comment, loggedUser}) {
    return (
        <>
            <Grid container direction="row" style={{
                border: "1px solid lightGray", 
                borderTop: "none", 
                padding: 15, 
                flexWrap: "nowrap",
                fontWeight: "normal",
                fontSize: 16
            }}>
                <Grid item style={{padding: 10}}>
                    <Avatar>C</Avatar>
                </Grid>
                <Grid item md={12}>
                    <Grid container direction="row" columnSpacing={2} fullWidth>
                        <Grid item>
                            {comment.userEmail}
                        </Grid>
                        <Grid item>
                            {ConvertDate(comment.createdAt)}
                        </Grid>
                        {loggedUser.email === comment.userEmail &&
                            <Grid container justifyContent="flex-end" md={4}>
                                <IconButton>
                                    <MoreHorizIcon/>
                                </IconButton>
                            </Grid>     
                        }
                    </Grid>
                    <Grid item>
                        <Grid item style={{padding: 15}}>
                            {(comment.content).trim()}
                        </Grid>
                    </Grid>
                </Grid>
            </Grid>
        </>
  )
}
