import { useEffect, useState } from "react";
import { useFetching } from "../../../../hooks/useFetching";
import { getByPostIdAsync } from "../../api/getByPostIdAsync";
import { Grid } from "@mui/material";
import { Loader } from "../../../../UI/Loader/Loader";
import CommentCardsList from "../CommentCardsList/CommentCardsList";

export const Comments = ({postId, loggedUser}) => {
    const [comments, setComments] = useState([]);
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

    return (
        <Grid container maxWidth="md" style={{width: 600}}>
            {isLoading ? (
                <Loader/>
            ) : comments ? (
                <CommentCardsList comments={comments} loggedUser={loggedUser}/>
            )
            :null}

        </Grid>
    )
}
