import { useEffect, useState } from "react";
import { useFetching } from "../../../../hooks/useFetching";
import { getByPostIdAsync } from "../../api/getByPostIdAsync";
import { Button, Grid, TextField } from "@mui/material";
import { Loader } from "../../../../UI/Loader/Loader";
import CommentCardsList from "../CommentCardsList/CommentCardsList";
import { createCommentAsync } from "../../api/createCommentAsync";
import {editCommentAsync} from "../../api/editCommentAsync"
import {deleteCommentAsync} from "../../api/deleteCommentAsync"
import { useContext } from "react";
import { PagesContext } from "../../../Provider/PagesProvider";

export const Comments = ({postId, userEmail}) => {
    const {loggedUser} = useContext(PagesContext)
    const [commentContent, setCommentContent] = useState("")
    const [comments, setComments] = useState([]);
    const [isCommenting, setIsCommenting] = useState(false)
    const [page, setPage] = useState(1);
    const [take, setTake] = useState(10);

    const [initializeComments, isLoading] = useFetching(async () => {
        const response = await getByPostIdAsync(postId, page, take);

        if (response) {
            setComments([...comments, ...response.data.items]);
        }
    });

    useEffect(() => {
        initializeComments();
    }, []);

    const handleCommentClick = () => {
        setIsCommenting(prev => !prev);
    }

    const handleChange = (event) => {
        setCommentContent(event.target.value);
    }

    const handleCreateComment = async () => {
        const response = await createCommentAsync(commentContent, loggedUser.email, postId);

        if (response) {
            setComments([...comments, response.data]);

            setCommentContent("");

            setIsCommenting(prev => !prev);
        }
    }

    const handleEditComment = async (id, content) => {
        const response = await editCommentAsync(id, content);

        if (response) {
            setComments(comments.map(x =>
                x.id === id
                 ? {...comments, id: id, content: response.data.content, userEmail: response.data.userEmail}
                : x
            ));
        }
    }

    const handleDeleteComment = async (id) => {
        const response = await deleteCommentAsync(id);

        if (response) {
            setComments(comments.filter(x => x.id !== id));
        }
    }

    return (
        <Grid container direction="column" maxWidth="md" style={{width: 600}}>
            {loggedUser.email !== userEmail &&
                <Grid item alignSelf="flex-end" style={{position: "relative", bottom: 50, right: 10}}>
                    <Button onClick={handleCommentClick} variant="outlined" style={{}}>Comment</Button>
                </Grid>
            }
            {isLoading ? 
                    <Loader/>
                :
                <>
                    {isCommenting &&
                            <Grid container columnSpacing={1} direction="row" style={{marginBottom: 10}}>
                                <Grid item md={9}>
                                    <TextField fullWidth
                                    name="content"
                                    onChange={handleChange}/>
                                </Grid>
                                <Grid item alignSelf="center">
                                    <Button onClick={handleCreateComment} variant="contained" style={{borderRadius: 30}}>Save</Button>
                                </Grid>
                                <Grid item alignSelf="center" md={1}>
                                    <Button onClick={handleCommentClick} variant="contained" style={{borderRadius: 30}}>Undo</Button>
                                </Grid>
                            </Grid>
                    }
                    <Grid item>
                        <CommentCardsList isOwner={userEmail === loggedUser.email} comments={comments} 
                        userEmail={loggedUser.email} editComment={handleEditComment} deleteComment={handleDeleteComment}/>
                    </Grid>
                </>
            }
        </Grid>
    )
}
