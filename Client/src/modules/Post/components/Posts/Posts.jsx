import {useContext, useEffect, useState} from 'react';
import {useFetching} from "../../../../hooks/useFetching.js";
import {getByUserEmailAsync} from "../../api/getByUserEmailAsync.js";
import PostCardList from "../PostCardList/PostCardList.jsx";
import {createNewPostAsync} from "../../api/createNewPostAsync.js";
import {deletePostAsync} from "../../api/deletePostAsync.js";
import { Container } from '@mui/system';
import CreatePostCard from "../CreatePostCard/CreatePostCard.jsx";
import {editPostAsync} from "../../api/editPostAsync.js";
import { PagesContext } from '../../../Provider/PagesProvider.jsx';
import { CircularProgress, Box } from '@mui/material';

const Posts = () => {
    const {user} = useContext(PagesContext);

    const [posts, setPosts] = useState([]);
    const [page, setPage] = useState(1);
    const [take, setTake] = useState(10);


    const [initializePosts, isLoading] = useFetching(async () => {
        const data = await getByUserEmailAsync(user.email, page, take)

        setPosts([...posts, ...data.items])

        setPage(data.page)
    })

    useEffect(() => {
        initializePosts();
    }, [page]);


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
            ? {...posts, id: id, content: response.content}
           : x

        ));
    }

    return (
        <div>
            <Container maxWidth="lg">
                <CreatePostCard createNewPost={createNewPost} user={user}/>
                {
                    isLoading 
                    ?
                        <Box sx={{ display: 'flex', justifyContent: "center" }}>
                            <CircularProgress />
                        </Box>
                    :
                        <PostCardList posts={posts} deletePost={deletePost} editPost={editPost}/>
                }
            </Container>
        </div>
    );
};

export default Posts;