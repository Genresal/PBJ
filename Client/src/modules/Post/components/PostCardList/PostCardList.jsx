import PostCard from "../PostCard/PostCard.jsx";
import Grid from "@mui/system/Unstable_Grid";

const PostCardList = ({posts, deletePost, editPost, loggedUser}) => {

    if(posts.length === 0){
        return(
            <h1 style={{textAlign: 'center'}}>No posts yet</h1>
        );
    }

    return (
        posts.slice().reverse().map(post =>
            <Grid container key={post.id} xs={12} >
                <PostCard  post={post} deletePost={deletePost} editPost={editPost} isDisabled={false} loggedUser={loggedUser}/>
            </Grid>
        )
    );
};

export default PostCardList;