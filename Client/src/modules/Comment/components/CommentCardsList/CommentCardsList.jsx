import { Grid } from '@mui/material'
import CommentCard from '../CommentCard/CommentCard'

export default function CommentCardsList({comments, loggedUser}) {

    if (comments.length === 0) {
        return (
            <h1 style={{textAlign: 'center'}}>No comments yet</h1>
        )
    }

    return (
        comments.slice().reverse().map(comment => 
                <Grid container key={comment.id} xs={12}>
                    <CommentCard comment={comment} loggedUser={loggedUser}/>
                </Grid>
            )
    )
}
