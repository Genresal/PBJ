import axios from "axios";
import { API_USER_FOLLOWER_URL } from "../../../constants/ApiConstants";

export const createUserFollowerAsync = async (userEmail, followerEmail) => {
    try {
        const response = await axios.post(API_USER_FOLLOWER_URL, {
            userEmail : userEmail,
            followerEmail: followerEmail
        })

        return response
    }
    catch (error) {
        console.log(error);
    }
}