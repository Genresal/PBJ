import { Avatar, Grid, IconButton, TextField, Popover, Button } from '@mui/material'
import {useState} from 'react'
import { ConvertDate } from '../../../../shared/DateConverter'
import MoreHorizIcon from '@mui/icons-material/MoreHoriz';
import EditIcon from '@mui/icons-material/Edit';
import DeleteOutlineIcon from '@mui/icons-material/DeleteOutline';
import { useContext } from 'react';
import { PagesContext } from '../../../Provider/PagesProvider';


export default function CommentCard({isOwner, comment, userEmail, editComment, deleteComment}) {

    console.log(isOwner)
    const {loggedUser} = useContext(PagesContext)
    const [content, setContent] = useState({content: ""});
    const [isEditing, setIsEditing] = useState(false);
    const [anchorEl, setAnchorEl] = useState(null);

    const open = Boolean(anchorEl);
    const id = open ? 'simple-popover' : undefined;

    const handlePopoverClick = (event) => {
        event.stopPropagation();

        setAnchorEl(event.currentTarget);
    };
    
    const handlePopoverClose = () => {
        setAnchorEl(null);
    };

    const handleEditButtonClick = () => {
        setContent(comment.content)
        setIsEditing(!isEditing);
        setAnchorEl(null);
    };

    const handleTextChange = (event) => {
        setContent(event.target.value)
    };

    const handleSaveComment = (content) =>{
        editComment(comment.id, content);

        setIsEditing(false);
    }

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
                        {isOwner ? 
                                    <Grid container justifyContent="flex-end" md={4}>
                                    <IconButton onClick={handlePopoverClick} aria-describedby={id}>
                                        <MoreHorizIcon/>
                                    </IconButton>
                                    </Grid> 
                                :

                                userEmail === comment.userEmail &&
                                    <Grid container justifyContent="flex-end" md={4}>
                                    <IconButton onClick={handlePopoverClick} aria-describedby={id}>
                                        <MoreHorizIcon/>
                                    </IconButton>
                                    </Grid> 
                        }
                    </Grid>
                    <Grid item>
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
                                            <Button onClick={() => handleSaveComment(content)} variant="contained" style={{borderRadius: 30, marginTop: 10}}>
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
                                    {(comment.content).trim()}
                                </Grid>    
                        }
                    </Grid>
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
                        <Button onClick={() => deleteComment(comment.id)} fullWidth startIcon={<DeleteOutlineIcon/>} style={{color: "black", textTransform: "none"}}>
                            Delete
                        </Button>
                    </Grid>
                </Grid>
            </Popover>
        </>
  )
}
