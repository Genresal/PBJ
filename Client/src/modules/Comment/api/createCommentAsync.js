import axios from "axios"
import { API_COMMENT_URL } from "../../../constants/ApiConstants"

export const createCommentAsync = async (commentContent, userEmail, postId) => {
    try {
        const response = await axios.post(API_COMMENT_URL, {
            content: commentContent,
            userEmail: userEmail,
            postId: postId
        })

        return response
    }
    catch (error) {
        console.log(error)
    }
}