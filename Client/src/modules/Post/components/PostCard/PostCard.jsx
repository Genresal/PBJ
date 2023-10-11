import {Avatar, Button, Grid, IconButton, TextField, Popover} from "@mui/material";
import DeleteOutlineIcon from '@mui/icons-material/DeleteOutline';
import EditIcon from '@mui/icons-material/Edit';
import {useState} from "react";
import { ConvertDate } from "../../../../shared/DateConverter.js";
import MoreHorizIcon from '@mui/icons-material/MoreHoriz';
import classes from "./PostCard.module.css"

const PostCard = ({post, deletePost, editPost}) => {
    const [content, setContent] = useState({content: ""});
    const [isEditing, setIsEditing] = useState(false);
    const [anchorEl, setAnchorEl] = useState(null);

    const open = Boolean(anchorEl);
    const id = open ? 'simple-popover' : undefined;

    const handlePopoverClick = (event) => {
        setAnchorEl(event.currentTarget);
      };
    
    const handlePopoverClose = () => {
        setAnchorEl(null);
    };

    const handleEditButtonClick = () => {
        setContent(post.content)
        setIsEditing(!isEditing);
        setAnchorEl(null);
    };

    const handleTextChange = (event) => {
        setContent(event.target.value)
    };

    const handleSavePost = (content) =>{
        editPost(post.id, content);

        setIsEditing(false);
    }

    return (
        <>
            <Grid container direction="row" style={{
                border: "1px solid lightGray", 
                borderTop: "none", 
                padding: 15, 
                flexWrap: "nowrap",
                fontWeight: "normal"
                }}
                className={classes.postCard}>
                <Grid item style={{paddingRight: 10}}>
                    <Avatar>A</Avatar>
                </Grid>
                <Grid item md={12}>
                    <Grid container direction="row" columnSpacing={2} fullWidth>
                        <Grid item>
                            {post.userEmail} 
                        </Grid>
                        <Grid item>
                            {ConvertDate(post.createdAt)}
                        </Grid>
                        <Grid container justifyContent="flex-end" md={4}>
                            <IconButton onClick={handlePopoverClick} aria-describedby={id}>
                                <MoreHorizIcon/>
                            </IconButton>
                        </Grid>
                    </Grid>
                    <Grid item>
                        {isEditing
                            ?
                                <>
                                    <TextField
                                    fullWidth
                                    multiline
                                    value={content}
                                    onChange={handleTextChange}
                                    />
                                    <Grid columnSpacing={1} container fullWidth justifyContent="flex-end">
                                        <Grid item>
                                            <Button onClick={() => handleSavePost(content)} variant="contained" style={{borderRadius: 30, marginTop: 10}}>
                                                Save
                                            </Button>
                                        </Grid>
                                        <Grid item>
                                            <Button onClick={handleEditButtonClick} variant="contained" style={{borderRadius: 30, marginTop: 10}}>
                                                Undo
                                            </Button>
                                        </Grid>
                                    </Grid>
                                </>
                                
                            :
                                <Grid item style={{ whiteSpace: "pre-wrap"}}>
                                    {(post.content).trim()}
                                </Grid>    
                        }
                    </Grid>
                </Grid>
            </Grid>
            
            <Popover
                id={id}
                open={open}
                anchorEl={anchorEl}
                onClose={handlePopoverClose}
                anchorOrigin={{
                vertical: 'bottom',
                horizontal: 'left',
                }}
            >
                <Grid container rowSpacing={1} direction="column" 
                    style={{width: 100, padding: 5}}>
                    <Grid item>
                        <Button onClick={handleEditButtonClick} fullWidth startIcon={<EditIcon/>} style={{color: "black", textTransform: "none"}}>
                            Edit
                        </Button>
                    </Grid>
                    <Grid item>
                        <Button onClick={() => deletePost(post.id)} fullWidth startIcon={<DeleteOutlineIcon/>} style={{color: "black", textTransform: "none"}}>
                            Delete
                        </Button>
                    </Grid>
                </Grid>
            </Popover>
        </>
        
    );
};

export default PostCard;