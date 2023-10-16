import {useEffect, useState} from 'react';
import {useFetching} from "../../../../hooks/useFetching.js";
import {getByUserEmailAsync} from "../../api/getByUserEmailAsync.js";
import PostCardList from "../PostCardList/PostCardList.jsx";
import {createNewPostAsync} from "../../api/createNewPostAsync.js";
import {deletePostAsync} from "../../api/deletePostAsync.js";
import CreatePostCard from "../CreatePostCard/CreatePostCard.jsx";
import {editPostAsync} from "../../api/editPostAsync.js";
import { Loader } from "../../../../UI/Loader/Loader.jsx"
import { Grid } from '@mui/material';

const Posts = ({user, isLoggedUser}) => {
    const [posts, setPosts] = useState([]);
    const [page, setPage] = useState(1);
    const [take, setTake] = useState(10);


    const [initializePosts, isLoading] = useFetching(async () => {
        const response = await getByUserEmailAsync(user.email, page, take)

        setPosts([...posts, ...response.data.items])

        setPage(response.data.page)
    })

    useEffect(() => {
        initializePosts();
    }, []);


    const createNewPost = async (newPost) => {
        const response = await createNewPostAsync(newPost.content, newPost.userEmail)

        setPosts([...posts, response.data]);
    }

    const deletePost = async (id) => {
        const response = await deletePostAsync(id);

        setPosts(posts.filter(x => x.id !== id))
    }

    const editPost = async (id, content) => {
        const response = await editPostAsync(id, content)

        setPosts(posts.map(x =>
           x.id === id
            ? {...posts, id: id, content: response.data.content, userEmail: response.data.userEmail}
           : x

        ));
    }

    return (
        <Grid container maxWidth="md" style={{width: 500}}>
            {isLoggedUser &&
                <CreatePostCard createNewPost={createNewPost} user={user}/>
            }
            {
                isLoading 
                ?
                    <Loader/>
                :
                    <PostCardList posts={posts} deletePost={deletePost} editPost={editPost} 
                    userEmail={user.email} isLoggedUser={isLoggedUser}/>
            }
        </Grid>
    );
};

export default Posts;