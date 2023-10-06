import axios from "axios";
import {BASE_POST_URL} from "../constants/PostApiConstants.js";


export const createNewPostAsync = async (content, userEmail) => {
    const response = await axios.post(BASE_POST_URL, {
        content: content,
        userEmail: userEmail
    })

    return response;
}