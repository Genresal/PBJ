import {
    Button,
    Card,
    CardActions,
    CardContent,
    CardHeader, Collapse,
} from "@mui/material";
import DeleteOutlineIcon from '@mui/icons-material/DeleteOutline';
import EditIcon from '@mui/icons-material/Edit';
import {useEffect, useState} from "react";
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import {useFetching} from "../../../../hooks/useFetching.js";
import {getCommentByPostId} from "../../../Comment/api/getCommentByPostId.js";

const PostCard = ({post, deletePost, editPost}) => {
    const [content, setContent] = useState({content: ""});
    const [isEditing, setIsEditing] = useState(false);
    const [expanded, setExpanded] = useState(false);
    const [page, setPage] = useState(1);
    const [take, setTake] = useState(2);
    const [comments, setComments] = useState([])

    const [initializeComments, isLoading] = useFetching(async () => {
        const data = await getCommentByPostId(post.id, page, take);

        setComments([...comments, ...data.items]);

        setPage(data.page);
    })

    useEffect(() => {
        initializeComments();
    }, [page]);

    const handleExpandClick = () => {
        setExpanded(!expanded);
    };

    const handleEditButtonClick = () => {
        setIsEditing(!isEditing);
    };

    const handleTextChange = (event) => {
        setContent(event.target.value)
        console.log(content)
    };

    const handleEditPost = (id, content) =>{
        editPost(id, content);

        setIsEditing(false);
    }

    return (
        <Card>
                <CardHeader>
                    {post.createdAt}
                </CardHeader>
                <CardContent>
                    {isEditing
                        ?<textarea
                            onChange={(event) => handleTextChange(event)}
                        />
                        :
                        <p>{post.id}{post.content}</p>
                    }
                </CardContent>
                <CardActions>
                    {
                        isEditing
                        ?
                            <Button variant="outlined" startIcon={<EditIcon />}
                                    onClick={() => handleEditPost(post.id, content)}>
                                Save
                            </Button>
                        :
                            <Button variant="outlined" startIcon={<EditIcon />} onClick={handleEditButtonClick}>
                                Edit
                            </Button>
                    }

                    <Button variant="outlined" startIcon={<DeleteOutlineIcon />}
                            onClick={() => deletePost(post.id)}>
                        Delete
                    </Button>
                    <Button variant="outlined" onClick={handleExpandClick} endIcon={<ExpandMoreIcon/>}>
                        Show comments
                    </Button>
            </CardActions>
            <Collapse in={expanded} timeout="auto" unmountOnExit>
                <CardContent>
                    {
                        comments.map(comment =>
                            <Card key={comment.id}>
                                <CardHeader>
                                    {comment.id}
                                </CardHeader>
                                <CardContent>
                                    {comment.content}
                                </CardContent>
                            </Card>
                        )
                    }
                </CardContent>
            </Collapse>
        </Card>
    );
};

export default PostCard;