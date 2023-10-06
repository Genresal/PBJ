import classes from "./CreatePostCard.module.css"
import AccountBoxOutlinedIcon from "@mui/icons-material/AccountBoxOutlined";
import {Button, TextField, Grid} from "@mui/material";
import {useRef, useState} from "react";

const CreatePostCard = ({createNewPost, user}) => {
    const [post, setPost] = useState({content: "", userEmail: user.email});
    const inputRef = useRef();

    const addPost = () => {
        createNewPost({...post});

        inputRef.current.value = "";

        setPost({content: "", userEmail: user.email});
    }

    const handleContentChange = (event) => {
        setPost({...post, content: event.target.value})
    }


    return (
        <Grid container  direction="row" style={{border: "1px solid lightgray", padding: 10, margin: "10px 0"}}>
            <Grid item style={{paddingRight: 10}}>
                <AccountBoxOutlinedIcon/>
            </Grid>
            <Grid item md={12}>
                <Grid container direction="column" rowSpacing={1}>
                    <Grid item style={{fontSize: 22}}>
                        {user.name}
                    </Grid>
                    <Grid item >
                        <TextField label="What's new?" fullWidth  multiline rows={4}
                        onChange={(event) => handleContentChange(event)} />
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