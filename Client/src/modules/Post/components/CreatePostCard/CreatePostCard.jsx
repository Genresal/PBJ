import classes from "./CreatePostCard.module.css"
import AccountBoxOutlinedIcon from "@mui/icons-material/AccountBoxOutlined";
import {Button, TextField} from "@mui/material";
import {useContext, useRef, useState} from "react";
import {UserIdContext} from "../../../../context/Contexts.js";

const CreatePostCard = ({createNewPost, user}) => {
    const userId = useContext(UserIdContext);
    const [post, setPost] = useState({content: "", userId: userId});
    const inputRef = useRef(null);

    const addPost = () => {
        createNewPost({...post});

        inputRef.current.value = "";

        setPost({content: "", userId: userId});
    }


    return (
        <div className={classes.postCard}>
            <div className={classes.userInfo}>
                <div className={classes.userDetails}>
                    <h2 style={{marginBottom:10}}><AccountBoxOutlinedIcon/>{user.name}</h2>
                    <p style={{marginTop:10}}>{user.email}</p>
                </div>
            </div>
            <div className={classes.postContent}>
                <TextField ref={inputRef} label="What's new?" variant="filled" fullWidth={4} rows={4} maxRows={4} multiline
                onChange={(e) => setPost({...post, content: e.target.value})}/>
            </div>
            <div className={classes.postActions} style={{marginTop: "30px"}}>
                <Button variant="contained" onClick={addPost}>Post</Button>
            </div>
        </div>
    );
};

export default CreatePostCard;