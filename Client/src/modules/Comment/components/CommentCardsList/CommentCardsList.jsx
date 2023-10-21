import { Grid } from '@mui/material'
import CommentCard from '../CommentCard/CommentCard'

export default function CommentCardsList({isOwner, comments, userEmail, editComment, deleteComment}) {

    if (comments.length === 0) {
        return (
            <h2 style={{textAlign: 'center'}}>No comments yet</h2>
        )
    }

    return (
        comments.slice().reverse().map(comment => 
            <Grid container key={comment.id} xs={12}>
                <CommentCard isOwner={isOwner} comment={comment} userEmail={userEmail} editComment={editComment} deleteComment={deleteComment}/>
            </Grid>
        )
    )
}
