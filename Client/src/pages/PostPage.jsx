import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom'
import { useFetching } from '../hooks/useFetching';
import { getByIdAsync } from '../modules/Post/api/getByIdAsync';
import PostCard from '../modules/Post/components/PostCard/PostCard';
import { useHistory } from 'react-router-dom/cjs/react-router-dom.min';
import { Loader } from '../UI/Loader/Loader';
import { Grid, IconButton} from '@mui/material';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import { Comments } from "../modules/Comment/components/Comments/Comments"
import NavMenu from '../UI/NavMenu/NavMenu';

export default function PostPage() {
    const params = useParams();
    const history = useHistory()
    const [post, setPost] = useState();

    const [initializePost, isLoading] = useFetching(async () => {
        const response = await getByIdAsync(params.postId)

        if (response) {
            setPost(response.data)
        }
    });

    useEffect(() => {
        initializePost()
    },[])

    const deletePost = async (id) => {
        const response = await deletePostAsync(id);

        history.push("/");
    }

    const editPost = async (id, content) => {
        const response = await editPostAsync(id, content)

        if (response) {
            setPost(response.data)
        }
    }
  
    return (
        <>
            <NavMenu/>

            <Grid container style={{border: "1px solid lightGray", height: 50, fontSize: 25, fontWeight: 600, padding: 5, borderTop: "none"}}>
                <Grid style={{marginRight: 10}}>
                    <IconButton onClick={history.goBack}>
                        <ArrowBackIcon/>
                    </IconButton>
                </Grid>
                <Grid item>
                    Post
                </Grid>
            </Grid>

            {isLoading ? (
                <Loader />
            ) 
            : post ? (
                <Grid container maxWidth="lg" style={{width: 600, fontSize: 20}}>
                    <PostCard post={post} editPost={editPost} deletePost={deletePost} isDisabled={true} userEmail={params.userEmail}/>
                    <Comments postId={post.id} userEmail={params.userEmail}/>
                </Grid>
            ) 
            : null}
        </>
    )
}
