import axios from "axios"
import { API_COMMENT_URL } from "../../../constants/ApiConstants"

export const editCommentAsync = async (id, content) => {
    try {
        const response = await axios.put(API_COMMENT_URL, {
            content: content
        }, {
            params : {
                id: id
            }
        });

        return response;
    }
    catch (error) {
        console.log(error);
    }
} 