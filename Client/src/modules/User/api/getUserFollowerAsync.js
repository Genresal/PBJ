import axios from "axios";
import { API_USER_FOLLOWER_URL } from "../../../constants/ApiConstants";

export const getUserFollowerAsync = async (userEmail, followerEmail) => {
    try {
        const response = await axios.get(API_USER_FOLLOWER_URL, {
            params : {
                userEmail: userEmail,
                followerEmail: followerEmail
            }
        });

        return response;
    }
    catch (error) {
        console.log(error);
    }
}