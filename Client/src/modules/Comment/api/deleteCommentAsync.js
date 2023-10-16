import axios from "axios";
import { API_COMMENT_URL } from "../../../constants/ApiConstants";

export const deleteCommentAsync = async (id) => {
    try {
        const response = await axios.delete(API_COMMENT_URL, {
            params: {
                id: id
            }
        });

        return response;
    }
    catch (error) {
        console.log(error);
    }
}