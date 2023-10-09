import {Button, TextField, Grid, Avatar} from "@mui/material";
import {useState} from "react";

const CreatePostCard = ({createNewPost, user}) => {
    const [post, setPost] = useState({content: "", userEmail: user.email});

    const addPost = () => {
        createNewPost({...post});

        setPost({content: "", userEmail: user.email});
    }

    const handleContentChange = (event) => {
        setPost({...post, content: event.target.value})
    }

    return (
        <Grid container rowSpacing={1}  direction="row" style={{border: "1px solid lightgray", padding: 10, marginTop: "10px"}}>
            <Grid item style={{paddingRight: 10}}>
                <Avatar>A</Avatar>
            </Grid>
            <Grid item style={{fontSize: 22}}>
                {user.userName}
            </Grid>
            <Grid item md={12}>
                <Grid container direction="column">
                    <Grid item>
                        <TextField label="What's new?" fullWidth  multiline rows={4}
                        onChange={(event) => handleContentChange(event)} value={post.content}/>
                    </Grid>          
                </Grid>
            </Grid>
            <Grid container style={{padding: 10}} justifyContent="flex-end">
                <Button variant="contained" onClick={addPost} style={{borderRadius: 30}}>Post</Button>
            </Grid>
        </Grid>
    );
};

export default CreatePostCard;