import axios from "axios";
import { API_COMMENT_URL } from "../../../constants/ApiConstants";

export const getByPostIdAsync = async (postId, page, take) => {
    try {
        const response = await axios.get(API_COMMENT_URL + "/post", {
            params: {
                postId: postId,
                page: page,
                take: take
            }
        })
    
        return response;
    }
    catch (error) {
        console.log(error)
    }
}