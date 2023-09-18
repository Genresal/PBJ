import axios from "axios";
import {BASE_POST_URL} from "../constants/PostApiConstants.js";


export const createNewPostAsync = async (content, userId) => {
    const response = await axios.post(BASE_POST_URL, {
        content: content,
        userId: userId
    })

    return response;
}