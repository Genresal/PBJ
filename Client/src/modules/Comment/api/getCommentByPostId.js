import axios from "axios";
import {GET_COMMENT_BY_POST_ID} from "../constants/CommentApiConstants.js";

export const getCommentByPostId = async (postId, page, take) => {
    const response = await axios.get(GET_COMMENT_BY_POST_ID, {
        params: {
            postId: postId,
            page: page,
            take: take
        }
    })

    return response.data;
}