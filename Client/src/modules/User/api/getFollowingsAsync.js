import axios from "axios"
import { API_USER_URL } from "../../../constants/ApiConstants"

export const getFollowingsAsync = async (followerEmail, page, take) => {
    try {
        const response = await axios.get(API_USER_URL + "/followings", {
            params : {
                email : followerEmail,
                page : page,
                take : take
            }
        })

        return response
    }
    catch (error) {
        console.log(error)
    }
}