import {useContext, useEffect, useState} from 'react';
import {useFetching} from "../../../../hooks/useFetching.js";
import {getByUserIdAsync} from "../../api/getByUserIdAsync.js";
import Loader from "../../../../UI/Loader/Loader.jsx";
import PostCardList from "../PostCardList/PostCardList.jsx";
import {createNewPostAsync} from "../../api/createNewPostAsync.js";
import {deletePostAsync} from "../../api/deletePostAsync.js";
import { UserIdContext } from '../../../../context/Contexts.js';
import { Container } from '@mui/system';
import CreatePostCard from "../CreatePostCard/CreatePostCard.jsx";
import {getUser} from "../../../User/api/getUser.js";
import {editPostAsync} from "../../api/editPostAsync.js";

const Posts = () => {
    const userId = useContext(UserIdContext);
    const [user, setUser] = useState({});
    const [posts, setPosts] = useState([]);
    const [page, setPage] = useState(1);
    const [take, setTake] = useState(2);

    const [initializeUser, _] = useFetching(async () => {
        const user = await getUser(userId);
        console.log(user.email, user.id)

        setUser(user);
    })


    useEffect(() => {
        initializeUser()

    }, []);

    const [initializePosts, isLoading] = useFetching(async () => {
        const response = await getByUserIdAsync(userId, page, take)

        setPosts([...posts, ...response.data.items])

        setPage(response.data.page)
    })

    useEffect(() => {
        initializePosts();
    }, [page]);


    const createNewPost = async (newPost) => {
        const response = await createNewPostAsync(newPost.content, newPost.userId)

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
            <Container maxWidth="xs">
                <CreatePostCard createNewPost={createNewPost} user={user}/>

                <PostCardList posts={posts} deletePost={deletePost} editPost={editPost}/>
                {
                    isLoading &&
                    <div style={{display:"flex", justifyContent: "center", marginTop: "50px"}}><Loader/></div>
                }
            </Container>
        </div>
    );
};

export default Posts;