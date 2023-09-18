import PostCard from "../PostCard/PostCard.jsx";
import {Container} from "@mui/system";
import Grid from "@mui/system/Unstable_Grid";

const PostCardList = ({posts, deletePost, editPost}) => {

    const gridStyles = {
        backgroundColor: "lightGray",
        padding: "10px",
        borderRadius: "5px",
        width: "500px"
    }


    if(posts.lenght){
        return(
            <h1 style={{textAlign: 'center'}}>No posts yet</h1>
        );
    }

    return (
        <Container>
            <Grid container spacing={2} style={gridStyles}>
                {posts.map(post =>
                    <Grid item="true" key={post.id} xs={12} >
                        <PostCard  post={post} deletePost={deletePost} editPost={editPost}/>
                    </Grid>
                )}
            </Grid>
        </Container>
    );
};

export default PostCardList;